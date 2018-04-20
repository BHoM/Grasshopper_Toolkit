﻿using System;
using Grasshopper.Kernel;
using System.Collections.Generic;
using System.Linq;
using BH.oM.Base;
using BH.UI.Alligator.Templates;
using System.IO;
using System.Reflection;

namespace BH.UI.Alligator.Base
{
    public class CreateBHoM : MethodCallTemplate
    {
        /*******************************************/
        /**** Properties                        ****/
        /*******************************************/

        public override Guid ComponentGuid { get; } = new Guid("0E1C95EB-1546-47D4-89BB-776F7920622D"); 

        protected override System.Drawing.Bitmap Internal_Icon_24x24 { get; } = Properties.Resources.CreateBHoM; 

        public override GH_Exposure Exposure { get; } = GH_Exposure.primary;

        public override string MethodGroup { get; set; } = "Create";


        /*******************************************/
        /**** Constructors                      ****/
        /*******************************************/

        public CreateBHoM() : base("Create BHoM Object", "CreateBHoM", "Creates a specific class of BHoMObject", "Alligator", " oM") {}


        /*******************************************/
    }
}