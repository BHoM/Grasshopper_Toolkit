/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2018, the respective contributors. All rights reserved.
 *
 * Each contributor holds copyright over their respective contributions.
 * The project versioning (Git) records all such contribution source information.
 *                                           
 *                                                                              
 * The BHoM is free software: you can redistribute it and/or modify         
 * it under the terms of the GNU Lesser General Public License as published by  
 * the Free Software Foundation, either version 3.0 of the License, or          
 * (at your option) any later version.                                          
 *                                                                              
 * The BHoM is distributed in the hope that it will be useful,              
 * but WITHOUT ANY WARRANTY; without even the implied warranty of               
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                 
 * GNU Lesser General Public License for more details.                          
 *                                                                            
 * You should have received a copy of the GNU Lesser General Public License     
 * along with this code. If not, see <https://www.gnu.org/licenses/lgpl-3.0.html>.      
 */

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using BH.oM.Base;
using BH.oM.Geometry;
using BH.Engine.Geometry;
using System;
using BH.Engine.Base;
using BH.Engine.Rhinoceros;
using Rhino;
using Rhino.DocObjects;
using GH_IO;
using GH_IO.Serialization;
using BH.Engine.Serialiser;

namespace BH.Engine.Grasshopper.Objects
{
    public class GH_BHoMAdapter : GH_BHoMGoo<object>  // Cannot reference BHoMAdapter for now as we are in the Engine
    {
        /*******************************************/
        /**** Properties                        ****/
        /*******************************************/

        public override string TypeName { get; } = "BHoMAdapter";

        public override string TypeDescription { get; } = "Contains a BHoM Adapter";

        public override bool IsValid { get { return Value != null; } }


        /*******************************************/
        /**** Constructors                      ****/
        /*******************************************/

        public GH_BHoMAdapter() : base() { }

        /***************************************************/

        public GH_BHoMAdapter(object val) : base(val) { }


        /*******************************************/
        /**** Override Methods                  ****/
        /*******************************************/

        public override IGH_Goo Duplicate()
        {
            return new GH_BHoMAdapter { Value = Value };
        }

        /***************************************************/

        public override string ToString()
        {
            object val = Value;
            if (val == null)
                return "null";
            else
                return val.ToString();
        }

        /***************************************************/
    }
}
