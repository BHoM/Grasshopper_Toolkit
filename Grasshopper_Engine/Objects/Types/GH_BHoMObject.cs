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
using BH.oM.Base;
using BH.Engine.Geometry;
using BH.Engine.Base;
using BH.Engine.Rhinoceros;

namespace BH.Engine.Grasshopper.Objects
{
    public class BHoMObjectGoo : BHGoo<IBHoMObject>, IGH_PreviewData
    {
        /*******************************************/
        /**** Properties                        ****/
        /*******************************************/

        public Rhino.Geometry.BoundingBox ClippingBox { get { return Bounds(); } }

        public Rhino.Geometry.BoundingBox Boundingbox { get { return Bounds(); } }


        /*******************************************/
        /**** Constructors                      ****/
        /*******************************************/

        public BHoMObjectGoo()
        {
            this.Value = null;
        }

        /***************************************************/

        public BHoMObjectGoo(BHoMObject val)
        {
            this.Value = val;
        }


        /*******************************************/
        /**** Override Methods                  ****/
        /*******************************************/

        protected override bool SetGeometry()
        {
            if (Value == null)
                return true;

            m_Geometry = Value.IGeometry();
            if (m_Geometry == null)
                return true;

            m_RhinoGeometry = m_Geometry.IToRhino();

            return true;
        }

        /***************************************************/
    }
}
