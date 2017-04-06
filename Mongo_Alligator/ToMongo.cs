﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using BHB = BHoM.Base;
using MA = Mongo_Adapter;
using GHE = Grasshopper_Engine;

namespace Alligator.Mongo
{
    public class ToMongo : GH_Component
    {
        public ToMongo() : base("ToMongo", "ToMongo", "Send BHoM objects to a Mongo database", "Alligator", "Mongo") { }

        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("30CA2D65-C265-43A5-A7FA-E183C2B916EB");
            }
        }

        /// <summary> Icon (24x24 pixels)</summary>
        protected override System.Drawing.Bitmap Internal_Icon_24x24
        {
            get { return Mongo_Alligator.Properties.Resources.BHoM_Mongo_To; }
        }
        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.secondary;
            }
        }

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Mongo link", "link", "collection to send the data to", GH_ParamAccess.item);
            pManager.AddGenericParameter("objects", "objects", "objects to send", GH_ParamAccess.tree);
            pManager.AddTextParameter("key", "key", "key unique to that package of data", GH_ParamAccess.item);
            pManager.AddTextParameter("tags", "tags", "tags attached to the saved data", GH_ParamAccess.list);
            pManager.AddBooleanParameter("active", "active", "check if the component currently allows data transfer", GH_ParamAccess.item, false);
            Params.Input[3].Optional = true;
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddBooleanParameter("Done", "Done", "return true when the task is finished", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MA.MongoLink link = GHE.DataUtils.GetGenericData<MA.MongoLink>(DA, 0);
            List<List<object>> listOfList = GHE.DataUtils.GetGenericDataListOfListFromDataTree<object>(DA, 1);
            string key = GHE.DataUtils.GetData<string>(DA, 2);
            List<string> tags = new List<string>(); DA.GetDataList<string>(3, tags);
            bool active = false; DA.GetData<bool>(4, ref active);

            List<object> objects;

            if (listOfList.Count == 1)
                objects = listOfList[0];
            else
                objects = listOfList.ToList<object>();

            if (!active || objects.Count == 0)
            {
                DA.SetData(0, false);
                return;
            }

            bool done = link.Push(objects, key, tags);
            DA.SetData(0, done);
        }

    }
}
