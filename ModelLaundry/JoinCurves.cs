﻿using System;
using System.Linq;
using System.Collections.Generic;
using Grasshopper.Kernel;
using GHE = Grasshopper_Engine;
using BHG = BHoM.Geometry;


namespace Alligator.ModelLaundry
{
    public class JoinCurves : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the JoinCurves class.
        /// </summary>
        public JoinCurves()
          : base("JoinCurves", "JoinCrvs",
              "Joining BHoM curves",
              "Alligator", "ModelLaundry")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Curves", "crvs", "Set of curves to join", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("JoinedCurves", "joinedCrvs", "Joined curves", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<object> crvs = (GHE.DataUtils.GetGenericDataList<object>(DA, 0));
            List<BHG.Curve> JoinCurves = new List<BHG.Curve>();
            List<BHG.Curve> output = new List<BHG.Curve>();
            List<BHG.Curve> singleCrvs = new List<BHG.Curve>();

            for (int i = 0; i < crvs.Count; i++)
            {
                if (crvs[i] is BHoM.Geometry.Group<BHG.Curve>)
                {
                    BHG.Group<BHG.Curve> newCrv = (BHG.Group<BHG.Curve>)crvs[i];
                    JoinCurves = BHG.Curve.Join(newCrv.ToList());

                    for (int j=0; j < JoinCurves.Count; j++)
                    {
                        output.Add(JoinCurves[j]);
                    } 
                }

                else
                {
                    BHG.Curve newSingleCrv = (BHG.Curve)crvs[i];
                    singleCrvs.Add(newSingleCrv);
                }
            }

            JoinCurves = BHG.Curve.Join(singleCrvs);

            for (int i = 0; i < JoinCurves.Count; i++)
            {
                output.Add(JoinCurves[i]);
            }


            DA.SetDataList(0, output);
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
            get { return new Guid("{16779af4-cd6d-450f-8754-802648af4be1}"); }
        }
    }
}