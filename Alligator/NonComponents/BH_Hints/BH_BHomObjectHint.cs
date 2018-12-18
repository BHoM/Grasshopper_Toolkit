﻿using System;
using Grasshopper.Kernel.Parameters;

namespace BH.UI.Grasshopper.GeometryHints
{
    public class BH_BHoMObjectHint : IGH_TypeHint
    {
        /*******************************************/
        /**** Properties                        ****/
        /*******************************************/

        public Guid HintID { get; } = new Guid("0977C35E-92DD-4933-8835-8B2C8A37C8CF"); 

        public string TypeName { get; } = typeof(BH.oM.Base.BHoMObject).ToString(); 


        /*******************************************/
        /**** Constructors                      ****/
        /*******************************************/

        public bool Cast(object data, out object target)
        {
            target = data;
            return true;
        }

        /*******************************************/
    }
}
