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

using BH.Engine.Rhinoceros;
using BHG = BH.oM.Geometry;
using RHG = Rhino.Geometry;
using Rhino.Display;
using Grasshopper.Kernel;
using System;
using System.Drawing;
using System.Linq;
using BH.Engine.Base;
using System.Collections.Generic;

namespace BH.UI.Grasshopper
{
    public static partial class Render
    {
        /***************************************************/
        /**** Public Methods  - Interfaces              ****/
        /***************************************************/

        public static void IRenderMeshes(this BHG.IGeometry geometry, GH_PreviewMeshArgs args)
        {
            if (geometry == null)
            {
                return;
            }
            else if (!(geometry is BHG.ISurface) & !(geometry is BHG.Mesh))
            {
                return;
            }
            DisplayMaterial bhMaterial = RenderMaterial(args.Material, Color.FromArgb(80, 255, 41, 105));//BHoM pink!
            try
            {
                RenderMeshes(geometry as dynamic, args.Pipeline, bhMaterial);
            }
            catch (Exception) { }
        }


        /***************************************************/
        /**** Public Methods  - Vectors                 ****/
        /***************************************************/

        public static void RenderMeshes(BHG.Point point, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            return;
        }

        /***************************************************/

        public static void RenderMeshes(BHG.Vector vector, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            return;
        }

        /***************************************************/

        public static void RenderMeshes(BHG.Plane plane, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            return;
        }

        /***************************************************/

        public static void RenderMeshes(BHG.CoordinateSystem.Cartesian coordinateSystem, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            return;
        }


        /***************************************************/
        /**** Public Methods  - Curves                  ****/
        /***************************************************/

        public static void RenderMeshes(BHG.Arc arc, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            return;
        }

        /***************************************************/

        public static void RenderMeshes(BHG.Circle circle, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            return;
        }

        /***************************************************/

        public static void RenderMeshes(BHG.Ellipse ellipse, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            return;
        }

        /***************************************************/

        public static void RenderMeshes(BHG.Line line, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            return;
        }
        /***************************************************/

        public static void RenderMeshes(BHG.NurbsCurve curve, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            return;
        }

        /***************************************************/

        public static void RenderMeshes(BHG.PolyCurve polycurve, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            return;
        }

        /***************************************************/

        public static void RenderMeshes(BHG.Polyline polyline, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            return;
        }


        /***************************************************/
        /**** Public Methods  - Surfaces                ****/
        /***************************************************/

        public static void RenderMeshes(BHG.Extrusion surface, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            pipeline.DrawBrepShaded(RHG.Brep.CreateFromSurface(surface.ToRhino()), material);
        }

        /***************************************************/

        public static void RenderMeshes(BHG.Loft surface, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            pipeline.DrawBrepShaded(RHG.Brep.CreateFromSurface(surface.ToRhino()), material);
        }

        /***************************************************/

        public static void RenderMeshes(BHG.NurbsSurface surface, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            RHG.GeometryBase geometry = surface.ToRhino();
            if (geometry is RHG.Surface)
                geometry = RHG.Brep.CreateFromSurface((RHG.Surface)geometry);

            pipeline.DrawBrepShaded((RHG.Brep)geometry, material);
        }

        /***************************************************/

        public static void RenderMeshes(BHG.Pipe surface, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            RHG.Brep rSurface = surface.ToRhino();
            pipeline.DrawBrepShaded(rSurface, material);
        }

        /***************************************************/

        public static void RenderMeshes(BHG.PlanarSurface surface, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            pipeline.DrawBrepShaded(surface.ToRhino(), material);
        }

        /***************************************************/

        public static void RenderMeshes(BHG.PolySurface polySurface, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            List<BHG.ISurface> surfaces = polySurface.Surfaces;
            for (int i = 0; i < surfaces.Count; i++)
            {
                pipeline.DrawBrepShaded(RHG.Brep.CreateFromSurface((RHG.Surface)surfaces[i].IToRhino()), material);
            }
        }


        /***************************************************/
        /**** Public Methods  - Mesh                    ****/
        /***************************************************/

        public static void RenderMeshes(BHG.Mesh mesh, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            RHG.Mesh rMesh = mesh.ToRhino();
            pipeline.DrawMeshShaded(rMesh, material);
        }

        /***************************************************/
        /**** Public Methods  - Miscellanea             ****/
        /***************************************************/

        public static void RenderMeshes(BHG.BoundingBox bbBox, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            pipeline.DrawBrepShaded(RHG.Brep.CreateFromBox(bbBox.ToRhino()), material);
        }

        /***************************************************/

        public static void RenderMeshes(BHG.CompositeGeometry composite, Rhino.Display.DisplayPipeline pipeline, DisplayMaterial material)
        {
            if (composite.Elements.Count == 0) { return; }
            foreach (BHG.IGeometry geom in composite.Elements)
            {
                try
                {
                    RenderMeshes(geom as dynamic, pipeline, material);
                }
                catch (Exception) { }
            }
        }

        /***************************************************/
    }
}


