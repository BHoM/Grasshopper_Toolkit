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
using Grasshopper.Kernel;
using System;
using System.Drawing;
using System.Linq;
using BH.Engine.Base;

namespace BH.UI.Grasshopper
{
    public static partial class Render
    {
        /***************************************************/
        /**** Public Methods  - Interfaces              ****/
        /***************************************************/

        public static void IRenderWires(this BHG.IGeometry geometry, GH_PreviewWireArgs args)
        {
            if (geometry == null)
            {
                return;
            }
            Color bhColour = RenderColour(args.Color, Color.FromArgb(80, 255, 41, 105));//BHoM pink!
            try
            {
                RenderWires(geometry as dynamic, args.Pipeline, bhColour);
            }
            catch (Exception) { }
        }


        /***************************************************/
        /**** Public Methods  - Vectors                 ****/
        /***************************************************/

        public static void RenderWires(BHG.Point point, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawPoint(point.ToRhino(), Rhino.Display.PointStyle.Simple, 4, bhColour);
        }

        /***************************************************/

        public static void RenderWires(BHG.Vector vector, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            //args.Pipeline.DrawLineArrow(vector.ToRhino(), args.Color, args.Thickness, args.Thickness);
        }

        /***************************************************/

        public static void RenderWires(BHG.Plane plane, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawConstructionPlane(new Rhino.DocObjects.ConstructionPlane() { Plane = (plane.ToRhino()), ThickLineColor = bhColour, ThinLineColor = Color.Black, GridLineCount = 10 });
        }

        /***************************************************/

        public static void RenderWires(BHG.CoordinateSystem.Cartesian coordinateSystem, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawConstructionPlane(new Rhino.DocObjects.ConstructionPlane() { Plane = (coordinateSystem.ToRhino()), ThickLineColor = bhColour, ThinLineColor = Color.Black, GridLineCount = 10 });
        }


        /***************************************************/
        /**** Public Methods  - Curves                  ****/
        /***************************************************/

        public static void RenderWires(BHG.Arc arc, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawArc(arc.ToRhino(), bhColour, 2);
        }

        /***************************************************/

        public static void RenderWires(BHG.Circle circle, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawCircle(circle.ToRhino(), bhColour, 2);
        }

        /***************************************************/

        public static void RenderWires(BHG.Ellipse ellipse, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawCurve(ellipse.ToRhino().ToNurbsCurve(), bhColour, 2);
        }

        /***************************************************/

        public static void RenderWires(BHG.Line line, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawLine(line.ToRhino(), bhColour, 2);
        }
        /***************************************************/

        public static void RenderWires(BHG.NurbsCurve curve, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawCurve(curve.ToRhino(), bhColour, 2);
        }

        /***************************************************/

        public static void RenderWires(BHG.PolyCurve polycurve, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawCurve(polycurve.ToRhino(), bhColour, 2);
        }

        /***************************************************/

        public static void RenderWires(BHG.Polyline polyline, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawPolyline(polyline.ControlPoints.Select(x => x.ToRhino()), bhColour, 2);
        }


        /***************************************************/
        /**** Public Methods  - Surfaces                ****/
        /***************************************************/

        public static void RenderWires(BHG.Extrusion surface, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            RHG.Surface rSurface = surface.ToRhino();
            pipeline.DrawSurface(rSurface, bhColour, 2);
        }

        /***************************************************/

        public static void RenderWires(BHG.Loft surface, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            RHG.Surface rSurface = surface.ToRhino();
            pipeline.DrawSurface(rSurface, bhColour, 2);
        }

        /***************************************************/

        public static void RenderWires(BHG.NurbsSurface surface, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            RHG.GeometryBase geometry = surface.ToRhino();
            if (geometry is RHG.Surface)
                geometry = RHG.Brep.CreateFromSurface((RHG.Surface)geometry);

            pipeline.DrawBrepWires((RHG.Brep)geometry, bhColour, 2);
        }

        /***************************************************/

        public static void RenderWires(BHG.Pipe surface, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawBrepWires(surface.ToRhino(), bhColour, 0);
        }

        /***************************************************/

        public static void RenderWires(BHG.PlanarSurface surface, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawBrepWires(surface.ToRhino(), bhColour, 0);
        }

        /***************************************************/

        public static void RenderWires(BHG.PolySurface polySurface, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            polySurface.Surfaces.ForEach(srf => pipeline.DrawSurface((RHG.Surface)srf.IToRhino(), bhColour, 2));
        }


        /***************************************************/
        /**** Public Methods  - Mesh                    ****/
        /***************************************************/

        public static void RenderWires(BHG.Mesh mesh, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            RHG.Mesh rMesh = mesh.ToRhino();
            pipeline.DrawMeshWires(rMesh, bhColour);
        }

        /***************************************************/
        /**** Public Methods  - Miscellanea             ****/
        /***************************************************/

        public static void RenderWires(BHG.BoundingBox bbBox, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawLines(bbBox.ToRhino().GetEdges(), bhColour, 2);
        }

        /***************************************************/

        public static void RenderWires(BHG.CompositeGeometry composite, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            if (composite.Elements.Count == 0) { return; }
            foreach (BHG.IGeometry geom in composite.Elements)
            {
                try
                {
                    RenderWires(geom as dynamic, pipeline, bhColour);
                }
                catch (Exception) { }
            }
        }

        /***************************************************/
    }
}


