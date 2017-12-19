﻿using System;
using Grasshopper.Kernel;
using System.Collections.Generic;
using System.Linq;
using GHE = Grasshopper_Engine;
using BHI = BHoM.Structural.Interface;
using BHL = BHoM.Structural.Loads;
using GH_IO.Serialization;
using BHoM.Base;

namespace Alligator.Structural.Commands
{
    public class CloseFile : GH_Component
    {

        public CloseFile() : base("Close file", "Close", "Close file", "Structure", "Commands")
        {

        }

        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("69741e45-db75-49e9-8cf3-4ad0fc17e5db");
            }
        }

        /// <summary> Icon (24x24 pixels)</summary>
        protected override System.Drawing.Bitmap Internal_Icon_24x24
        {
            get { return Alligator.Properties.Resources.BHoM_App_Close; }
        }

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Application", "Application", "Application to run", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Activate", "Go", "Activate", GH_ParamAccess.item);

            pManager[1].Optional = true;
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddBooleanParameter("Success", "Success", "Returns wether the analysis was successfully run or not", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool success = false;
            if (GHE.DataUtils.Run(DA, 1))
            {
                BHI.ICommandAdapter adapter = GHE.DataUtils.GetGenericData<BHI.ICommandAdapter>(DA, 0);

                success = adapter.Close();
            }

            DA.SetData(0, success);
        }
    }
}