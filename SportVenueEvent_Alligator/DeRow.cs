﻿using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using RHG = Rhino.Geometry;

using BHG = BHoM.Geometry;
using SportVenueEvent.oM;

namespace SportVenueEvent_Alligator
{
    public class DeRow : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the DeRake class.
        /// </summary>
        public DeRow()
          : base("Deconstruct Row", "DeRow",
              "",
              "SportVenueEvent", "Stadium")
        {
        }

        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.secondary;
            }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Row", "Row", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Seats", "Seats", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Row row = new Row();
            List<Seat> seats = new List<Seat>();

            DA.GetData(0, ref row);

            seats.AddRange(row.Seats);

            DA.SetDataList(0, seats);
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
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("b51e4cbd-073c-416c-8d18-b4f8d9283f03"); }
        }
    }
}