﻿using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using BH.UI.Alligator.Base;
using BH.oM.Base;
using BH.UI.Alligator;
using BH.Adapter.Queries;
using BH.Adapter;

namespace BH.UI.Alligator.Adapter
{
    public class Push : GH_Component
    {
        public Push() : base("Push", "Push", "Push objects to the external software", "Alligator", "Adapter") { }
        protected override System.Drawing.Bitmap Internal_Icon_24x24 { get { return null; } }
        public override Guid ComponentGuid { get { return new Guid("040CEC18-C6E1-443B-B816-72B100304536"); } }

        public override GH_Exposure Exposure { get { return GH_Exposure.primary; } }

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Adapter", "Adapter", "Adapter to the external software", GH_ParamAccess.item);
            pManager.AddParameter(new BHoMObjectParameter(), "Objects", "Objects", "Objects to push", GH_ParamAccess.list);
            pManager.AddTextParameter("Tag", "Tag", "Tag to apply to the objects being pushed", GH_ParamAccess.item);
            pManager.AddGenericParameter("Config", "Config", "Delete config", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Active", "Active", "Execute the push", GH_ParamAccess.item);
            Params.Input[3].Optional = true;
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddBooleanParameter("Success", "Success", "Success", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            BHoMAdapter adapter = null; DA.GetData(0, ref adapter);
            List<BHoMObject> objects = new List<BHoMObject>(); DA.GetDataList(1, objects);
            string tag = ""; DA.GetData(2, ref tag);
            Dictionary<string, string> config = null; DA.GetData(3, ref config);
            bool active = false; DA.GetData(4, ref active);

            if (!active) return;

            bool success = adapter.Push(objects, tag, config);
            DA.BH_SetData(0, success);
        }
    }
}
