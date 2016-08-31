﻿using Grasshopper.Kernel;
using System;
using RSI = Robot_Adapter.Structural.Interface;

namespace Alligator.Robot
{
    public class RobotApp : GH_Component
    {
        public RobotApp() : base("Robot Application", "RobotApp", "Creates a robot Application", "Structure", "Application") { }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Filename", "F", "Robot Filename", GH_ParamAccess.item);
            pManager.AddGenericParameter("Settings", "S", "Application Settings", GH_ParamAccess.item);
            pManager[0].Optional = true;
            pManager[1].Optional = true;
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Application", "A", "Robot Application", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            RSI.RobotAdapter app = new RSI.RobotAdapter();
            DA.SetData(0, app);        
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("97b9ded4-41a2-4ded-b835-432332fb760a"); }
        }

        /// <summary> Icon (24x24 pixels)</summary>
        protected override System.Drawing.Bitmap Internal_Icon_24x24
        {
            get { return Robot_Alligator.Properties.Resources.robot_app; }
        }

    }
}