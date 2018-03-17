﻿using System;
using Grasshopper.Kernel;
using System.Collections.Generic;
using BH.UI.Alligator.Templates;
using BH.oM.DataStructure;
using System.Linq;
using System.Windows.Forms;
using Grasshopper.GUI;
using BH.Engine.DataStructure;
using BH.Engine.Reflection.Convert;
using BH.UI.Alligator.Base.NonComponents.Menus;

namespace BH.UI.Alligator.Base
{
    public class CreateBHoMType : GH_Component
    {
        /*******************************************/
        /**** Properties                        ****/
        /*******************************************/

        public override Guid ComponentGuid { get; } = new Guid("FC00CD7C-AAC6-43FC-A6B7-BBE35BF0E4FD");

        protected override System.Drawing.Bitmap Internal_Icon_24x24 { get; } = Properties.Resources.Type;

        public override GH_Exposure Exposure { get; } = GH_Exposure.primary; 


        /*******************************************/
        /**** Constructors                      ****/
        /*******************************************/

        public CreateBHoMType() : base("Create BHoM Type", "BHoMType", "Creates a specific type definition", "Alligator", " oM")
        {
            if (m_TypeTree == null || m_TypeList == null)
            {
                IEnumerable<Type> types = Engine.Reflection.Query.BHoMTypeList();
                IEnumerable<string> paths = types.Select(x => x.ToText(true));

                List<string> ignore = new List<string> { "BH", "oM", "Engine" };
                m_TypeTree = Create.Tree(types, paths.Select(x => x.Split('.').Where(y => !ignore.Contains(y))), "select a type").ShortenBranches();
                m_TypeList = paths.Zip(types, (k, v) => new Tuple<string, Type>(k, v)).ToList();
            }
        }


        /*******************************************/
        /**** Override Methods                  ****/
        /*******************************************/

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
        }

        /*******************************************/

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Type", "Type", "Type definition", GH_ParamAccess.item);
        }

        /*******************************************/

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            DA.SetData(0, m_Type);
        }

        /*************************************/

        public override bool Write(GH_IO.Serialization.GH_IWriter writer)
        {
            if (m_Type != null)
            {
                writer.SetString("TypeName", m_Type.AssemblyQualifiedName);
            }
            return base.Write(writer);
        }

        /*************************************/

        public override bool Read(GH_IO.Serialization.GH_IReader reader)
        {
            string typeString = ""; reader.TryGetString("TypeName", ref typeString);

            if (typeString.Length > 0)
                m_Type = Type.GetType(typeString);

            if (m_Type != null)
                Message = m_Type.ToText();

            return base.Read(reader);
        }


        /*******************************************/
        /**** Protected Methods                 ****/
        /*******************************************/

        protected override void AppendAdditionalComponentMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalComponentMenuItems(menu);

            SelectorMenu<Type> selector = new SelectorMenu<Type>(menu, Item_Click);
            selector.AppendTree(m_TypeTree);
            selector.AppendSearchBox(m_TypeList);
        }

        /*******************************************/

        protected void Item_Click(object sender, Type type)
        {
            m_Type = type;
            if (m_Type == null)
                return;
        
            Message = m_Type.ToText();
            ExpireSolution(true);
        }


        /*******************************************/
        /**** Protected Fields                  ****/
        /*******************************************/

        Type m_Type = null;
        protected static Tree<Type> m_TypeTree = null;
        protected static List<Tuple<string, Type>> m_TypeList = null;

        /*******************************************/
    }
}