/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2021, the respective contributors. All rights reserved.
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

using RHG = Rhino.Geometry;
using Rhino.Display;
using Grasshopper.Kernel;
using System;
using BH.oM.Reflection.Attributes;
using System.Collections.Generic;
using BH.oM.Geometry;
using System.Drawing;
using BH.Engine.Rhinoceros;

namespace BH.UI.Grasshopper
{
    public static partial class Render
    {
        /***************************************************/
        /**** Public Methods  - Interfaces              ****/
        /***************************************************/

        public static void IRenderRhinoMeshes(this object obj, GH_PreviewMeshArgs args, Color custom)
        {
            if (obj == null)
            {
                return;
            }
            DisplayMaterial material = RenderMaterial(args.Material, custom);

            try
            {
                RenderRhinoMeshes(obj as dynamic, args.Pipeline, material);
            }
            catch (Exception) { }
        }

        /***************************************************/

        public static void RenderRhinoMeshes(this List<object> geometry, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            foreach(object geo in geometry)
            {
                RenderRhinoMeshes(geo as dynamic, pipeline, material);
            }
        }


        /***************************************************/
        /**** Private Methods  - Fallback               ****/
        /***************************************************/

        private static void RenderRhinoMeshes(this object fallback, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            // fallback in case no method is found for the provided runtime type
            return;
        }


        /***************************************************/
        /**** Public Methods  - Surfaces                ****/
        /***************************************************/

        public static void RenderRhinoMeshes(RHG.Extrusion surface, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            pipeline.DrawBrepShaded(surface.ToBrep(), material);
        }

        /***************************************************/

        public static void RenderRhinoMeshes(RHG.NurbsSurface surface, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            pipeline.DrawBrepShaded(surface.ToBrep(), material);
        }

        /***************************************************/

        public static void RenderRhinoMeshes(RHG.Brep brep, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            pipeline.DrawBrepShaded(brep, material);
        }

        /***************************************************/

        public static void RenderRhinoMeshes(RHG.Surface surface, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            pipeline.DrawBrepShaded(surface.ToBrep(), material);
        }


        /***************************************************/
        /**** Public Methods - Solids                   ****/
        /***************************************************/

        public static void RenderRhinoMeshes(RHG.Cone cone, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            pipeline.DrawBrepShaded(cone.ToBrep(true), material);
        }

        /***************************************************/

        public static void RenderRhinoMeshes(RHG.Cylinder cylinder, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            pipeline.DrawBrepShaded(cylinder.ToBrep(cylinder.IsFinite, cylinder.IsFinite), material);
        }

        /***************************************************/

        public static void RenderRhinoMeshes(RHG.Sphere sphere, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            pipeline.DrawBrepShaded(sphere.ToBrep(), material);
        }

        /***************************************************/

        public static void RenderRhinoMeshes(RHG.Torus torus, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            pipeline.DrawBrepShaded(torus.ToRevSurface().ToBrep(), material);
        }


        /***************************************************/
        /**** Public Methods  - Mesh                    ****/
        /***************************************************/

        public static void RenderRhinoMeshes(RHG.Mesh mesh, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            if (mesh.VertexColors.Count > 0)
                pipeline.DrawMeshFalseColors(mesh);
            else
                pipeline.DrawMeshShaded(mesh, material);
        }


        /***************************************************/
        /**** Public Methods  - Miscellanea             ****/
        /***************************************************/

        public static void RenderRhinoMeshes(RHG.BoundingBox bbBox, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            pipeline.DrawBrepShaded(bbBox.ToBrep(), material);
        }
    }
}


