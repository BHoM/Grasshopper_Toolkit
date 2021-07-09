/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2021, the respective contributors. All rights reserved.
 *
 * Each contributor holds copyright over their respective contributions.
 * The project versioning (Git) records all such contribution source information.
 *                                           
 *                                                                              
 * The BHoM is free software: you can redistribute it and/or modify         
 * it under the terms of the GNU Lesser General Public License as published by  
 * the Free Software Foundation, either version 3.0 of the License, or          
 * (at your option) any later version.                                          
 *                                                                              
 * The BHoM is distributed in the hope that it will be useful,              
 * but WITHOUT ANY WARRANTY; without even the implied warranty of               
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                 
 * GNU Lesser General Public License for more details.                          
 *                                                                            
 * You should have received a copy of the GNU Lesser General Public License     
 * along with this code. If not, see <https://www.gnu.org/licenses/lgpl-3.0.html>.      
 */

using System;
using System.Linq;
using GH = Grasshopper;
using Grasshopper.Kernel;
using BH.oM.Base;
using BH.oM.UI;
using System.Collections.Generic;
using System.Windows.Forms;
using BH.UI.Grasshopper.Global;
using BH.oM.Reflection;
using Grasshopper.Kernel.Parameters;
using BH.UI.Grasshopper.Parameters;
using BH.Engine.Reflection;
using BH.oM.Geometry;
using BH.Engine.Grasshopper;
using BH.UI.Grasshopper.Components;
using System.Collections;
using BH.Adapter;
using BH.oM.Reflection.Debugging;
using BH.UI.Base;
using System.IO;
using BH.UI.Base.Global;

namespace BH.UI.Grasshopper.Templates
{
    public abstract partial class CallerComponent : GH_Component, IGH_VariableParameterComponent, IGH_InitCodeAware
    {
        /*******************************************/
        /**** Properties                        ****/
        /*******************************************/

        public abstract Caller Caller { get; }

        public DataAccessor_GH Accessor = null;

        protected override System.Drawing.Bitmap Internal_Icon_24x24 { get { return Caller.Icon_24x24; } }

        public override Guid ComponentGuid { get { return Caller.Id; } }

        public override GH_Exposure Exposure { get { return (GH_Exposure)Math.Pow(2, Caller.GroupIndex); } }

        public override bool Obsolete { get { return Caller.IsObsolete(); } }


        /*******************************************/
        /**** Constructors                      ****/
        /*******************************************/

        public CallerComponent() : base()
        {
            NewInstanceGuid(System.Guid.Empty);
            PostConstructor();  // Explicitly calling PostConstructor() since it is not called in the parameterless constructor overload base()
            NewInstanceGuid();

            Name = Caller.Name;
            NickName = Caller.Name;
            Description = Caller.Description;
            Category = "BHoM";
            SubCategory = Caller.Category;

            

            Accessor = new DataAccessor_GH(Params.Input, IsValidPrincipalParameterIndex ? PrincipalParameterIndex : -1);
            Caller.SetDataAccessor(Accessor);

            // Listening to events from the Caller
            Caller.Modified += OnCallerModified;
            Caller.SolutionExpired += (sender, e) => ExpireSolution(true);

            // Listening to events from GH component
            Params.ParameterChanged += OnGHParamChanged;
        }

        /*******************************************/

        static CallerComponent()
        {
            GlobalSearchMenu.Activate();

            GH.Instances.CanvasCreated += (canvas) =>
            {
                canvas.DocumentChanged += (sender, e) =>
                {
                    if (e.OldDocument != null)
                        DocumentListener.OnDocumentClosing(e.OldDocument.FilePath);

                    if (e.NewDocument != null)
                        DocumentListener.OnDocumentEndOpening(e.NewDocument.FilePath);
                    m_CurrentLoadingDocument = null;
                };
            };
        }


        /*******************************************/
        /**** GH_Component Override Methods     ****/
        /*******************************************/

        public override void CreateAttributes()
        {
            this.m_attributes = new CallerAttribute(this);
        }

        /*******************************************/

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Accessor.GH_Accessor = DA;
            Caller.Run();

            List<Event> events = Engine.Reflection.Query.CurrentEvents();
            Helpers.ShowEvents(this, events);

            if (DA.Iteration == 0)
            {
                GH_Document doc = GH.Instances.ActiveCanvas.Document;
                Engine.UI.Compute.LogUsage("Grasshopper", GH.Versioning.VersionString, InstanceGuid, Caller.GetType().Name, Caller.SelectedItem, events, doc.DocumentID.ToString(), doc.FilePath);
            }
                
        }

        /*******************************************/

        protected override void AppendAdditionalComponentMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalComponentMenuItems(menu);
            Caller.AddToMenu(menu);
        }

        /*******************************************/

        public override bool Write(GH_IO.Serialization.GH_IWriter writer)
        {
            writer.SetString("Component", Caller.Write());
            return base.Write(writer);
        }

        /*************************************/

        public override bool Read(GH_IO.Serialization.GH_IReader reader)
        {
            if (!base.Read(reader) || !Params.Read(reader))
                return false;

            if (reader.ArchiveLocation != null && reader.ArchiveLocation != m_CurrentLoadingDocument)
            {
                BH.UI.Base.Global.DocumentListener.OnDocumentBeginOpening(reader.ArchiveLocation);
                m_CurrentLoadingDocument = reader.ArchiveLocation;
            }

            string callerString = ""; reader.TryGetString("Component", ref callerString);
            Caller.Read(callerString);

            return true;
        }

        /*******************************************/

        public override IList<string> RuntimeMessages(GH_RuntimeMessageLevel level)
        {
            //Make sure to extract both 'Remark' as well as 'Blank' messages when remarks are extracted.
            //This is because we are adding 'Blank' messages instead of 'Remarks' when there are any warnings/errors present
            //to make sure the component colouring is correct.
            //See more comments in the "Logging.cs" file.
            if (level == GH_RuntimeMessageLevel.Remark)
            {
                List<string> remarks = base.RuntimeMessages(level).ToList();
                remarks.AddRange(base.RuntimeMessages(GH_RuntimeMessageLevel.Blank));
                return remarks;
            }
            return base.RuntimeMessages(level);
        }

        /*******************************************/

        protected override void RegisterInputParams(GH_InputParamManager pManager = null)
        {
            foreach (ParamInfo info in Caller.InputParams.Where(x => x.IsSelected))
                Params.RegisterInputParam(info.ToGH_Param());
        }

        /*******************************************/

        protected override void RegisterOutputParams(GH_OutputParamManager pManager = null)
        {
            foreach (ParamInfo info in Caller.OutputParams.Where(x => x.IsSelected))
                Params.RegisterOutputParam(info.ToGH_Param());
        }

        /*******************************************/

        private static string m_CurrentLoadingDocument = "";
    }
}


