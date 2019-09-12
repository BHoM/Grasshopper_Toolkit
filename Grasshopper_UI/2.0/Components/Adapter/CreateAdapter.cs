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

using System;
using Grasshopper.Kernel;
using System.Collections.Generic;
using System.Linq;
using BH.UI.Grasshopper.Templates;
using BH.Adapter;
using System.IO;
using System.Reflection;

namespace BH.UI.Grasshopper.Adapter
{
    public class CreateAdapter : MethodCallTemplate
    {
        /*******************************************/
        /**** Properties                        ****/
        /*******************************************/

        public override Guid ComponentGuid { get; } = new Guid("A2D956AE-98A8-486D-A2AB-371B45F8B3AE"); 

        protected override System.Drawing.Bitmap Internal_Icon_24x24 { get; } = Properties.Resources.Adapter; 

        public override GH_Exposure Exposure { get; } = GH_Exposure.hidden;

        public override bool ShortenBranches { get; set; } = true;

        public override bool Obsolete { get; } = true;


        /*******************************************/
        /**** Constructors                      ****/
        /*******************************************/

        public CreateAdapter() : base("Create Adapter", "Adapter", "Creates a specific class of Adapter", "Grasshopper", " Adapter") { }


        /*******************************************/
        /**** Override Methods                  ****/
        /*******************************************/

        protected override IEnumerable<MethodBase> GetRelevantMethods()
        {
            Type adapterType = typeof(BHoMAdapter);
            return BH.Engine.Reflection.Query.AdapterTypeList().Where(x => x.IsSubclassOf(adapterType)).OrderBy(x => x.Name).SelectMany(x => x.GetConstructors());
        }

        /*******************************************/
    }
}