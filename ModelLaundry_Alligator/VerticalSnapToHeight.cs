﻿using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using MLE = ModelLaundry_Engine;
using GHE = Grasshopper_Engine;


namespace Alligator.ModelLaundry
{
    public class VerticalSnapToHeight : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the VerticalPointSnaping class.
        /// </summary>
        public VerticalSnapToHeight() : base("VerticalSnapToHeight", "VSnap2H", "Description", "Alligator", "ModelLaundry") { }
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("BHoM Elements", "bhElements", "BHoM object or geometry to snap", GH_ParamAccess.item);
            pManager.AddNumberParameter("Reference Heights", "refHeights", "reference heights to snap to", GH_ParamAccess.list);
            pManager.AddNumberParameter("Tolerance", "tolerance", "Set a tolerance for the snapping", GH_ParamAccess.item, 0.2);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("SnappedElement", "snapped", "resulting BHoM object or geometry", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            object element = GHE.DataUtils.GetGenericData<object>(DA, 0);
            List<double> refHeights = GHE.DataUtils.GetDataList<double>(DA, 1);
            double tol = GHE.DataUtils.GetData<double>(DA, 2);

            object result = MLE.Snapping.VerticalSnapToHeight(element, refHeights, tol);
            DA.SetData(0, result);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return base.Icon;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("d1634fe1-9e9a-47c0-9728-6566e76e524d"); }
        }
    }
}