﻿using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace SVG_Alligator
{
    public class SVGStyleComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the StyleComponent class.
        /// </summary>
        public SVGStyleComponent()
          : base("SVG Style", "SVG Style",
              "Description",
              "Alligator", "SVG")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "N", "Style Name", GH_ParamAccess.item);
            pManager.AddNumberParameter("Thickness", "T", "Thickness", GH_ParamAccess.item);
            pManager.AddColourParameter("Stroke", "S", "Stroke", GH_ParamAccess.item);
            pManager.AddColourParameter("Fill", "F", "Fill", GH_ParamAccess.item);

            pManager[0].Optional = true;
            pManager[1].Optional = true;
            pManager[2].Optional = true;
            pManager[3].Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Style", "S", "Style", GH_ParamAccess.item);
            pManager.AddTextParameter("SVG String", "SVG", "SVG String", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            System.Drawing.Color sCol = new System.Drawing.Color();
            System.Drawing.Color fCol = new System.Drawing.Color();
            string name = null;
            double thickness = 0;

            Dictionary<string, object> StyleData = new Dictionary<string, object>();
            string styleString = "._name {" + System.Environment.NewLine 
                + "fill: rgb(_rFVal, _gFVal, _bFVal);" + System.Environment.NewLine 
                + "stroke: rgb(_rSVal, _gSVal, _bSVal);" + System.Environment.NewLine 
                + "stroke - width: _width;" + System.Environment.NewLine 
                + "}";

            StyleData.Add("Name", null);
            StyleData.Add("Thickness", null);
            StyleData.Add("Stroke", null);
            StyleData.Add("Fill", null);
            StyleData.Add("SVG", null);

            if (DA.GetData(0, ref name)) { StyleData["Name"] = name; };
            if (DA.GetData(1, ref thickness)) { StyleData["Thickness"] = thickness; } ;
            if (DA.GetData(2, ref sCol)) { StyleData["Stroke"] = sCol; };
            if (DA.GetData(3, ref fCol)) { StyleData["Fill"] = fCol; };
            
            styleString = styleString.Replace("_name", name);

            styleString = styleString.Replace("_width", thickness.ToString());
            styleString = styleString.Replace("_rSVal", sCol.R.ToString());
            styleString = styleString.Replace("_gSVal", sCol.G.ToString());
            styleString = styleString.Replace("_bSVal", sCol.B.ToString());
            styleString = styleString.Replace("_rFVal", fCol.R.ToString());
            styleString = styleString.Replace("_gFVal", fCol.G.ToString());
            styleString = styleString.Replace("_bFVal", fCol.B.ToString());

            StyleData["SVG"] = styleString;

            DA.SetData(0, StyleData);
            DA.SetData(1, styleString);
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
            get { return new Guid("de39c7dd-dec4-4568-af37-6454764412c0"); }
        }
    }
}