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
using System.Drawing;
using BH.oM.Reflection.Attributes;
using System.Collections.Generic;
using GH = Grasshopper;
using BH.oM.Base;
using BH.oM.Geometry;
using BH.Engine.Rhinoceros;

namespace BH.UI.Grasshopper
{ 
    public static partial class Render
    {
        /***************************************************/
        /**** Public Methods  - Interfaces              ****/
        /***************************************************/

        public static void IRenderRhinoWires(this object geometry, GH_PreviewWireArgs args, Color custom)
        {
            if (geometry == null)
            {
                return;
            }
            Color bhColour = RenderColour(args.Color, custom);
            try
            {
                RenderRhinoWires(geometry as dynamic, args.Pipeline, bhColour);
            }
            catch (Exception) { }
        }

        /***************************************************/

        public static void RenderRhinoWires(this List<object> geometry, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            foreach(object geo in geometry)
            {
                RenderRhinoWires(geo as dynamic, pipeline, bhColour);
            }
        }


        /***************************************************/
        /**** Private Methods  - Fallback               ****/
        /***************************************************/

        private static void RenderRhinoWires(this object fallback, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            // fallback in case no method is found for the provided runtime type
            return;
        }


        /***************************************************/
        /**** Public Methods  - Vectors                 ****/
        /***************************************************/

        public static void RenderRhinoWires(RHG.Point3d point, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawPoint(point, bhColour);
        }

        /***************************************************/

        public static void RenderRhinoWires(RHG.Plane plane, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawConstructionPlane(new Rhino.DocObjects.ConstructionPlane() { Plane = plane, ThickLineColor = bhColour, ThinLineColor = Color.Black, GridLineCount = 10 });
        }


        /***************************************************/
        /**** Public Methods  - Curves                  ****/
        /***************************************************/

        public static void RenderRhinoWires(RHG.Arc arc, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawArc(arc, bhColour, 2);
        }

        /***************************************************/

        public static void RenderRhinoWires(RHG.ArcCurve arc, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawArc(arc.Arc, bhColour, 2);
        }

        /***************************************************/

        public static void RenderRhinoWires(RHG.Circle circle, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawCircle(circle, bhColour, 2);
        }

        /***************************************************/

        public static void RenderRhinoWires(RHG.Curve curve, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawCurve(curve, bhColour, 2);
        }

        /***************************************************/

        public static void RenderRhinoWires(RHG.Ellipse ellipse, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawCurve(ellipse.ToNurbsCurve(), bhColour, 2);
        }

        /***************************************************/

        public static void RenderRhinoWires(RHG.Line line, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawLine(line, bhColour, 2);
        }

        /***************************************************/

        public static void RenderRhinoWires(RHG.LineCurve line, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawLine(line.Line, bhColour, 2);
        }

        /***************************************************/

        public static void RenderRhinoWires(RHG.NurbsCurve curve, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawCurve(curve, bhColour, 2);
        }

        /***************************************************/

        public static void RenderRhinoWires(RHG.PolyCurve polycurve, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawCurve(polycurve, bhColour, 2);
        }

        /***************************************************/

        public static void RenderRhinoWires(RHG.Polyline polyline, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawPolyline(polyline, bhColour, 2);
        }

        /***************************************************/

        public static void RenderRhinoWires(RHG.PolylineCurve polyline, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            RHG.Polyline poly;
            if (polyline.TryGetPolyline(out poly))
                pipeline.DrawPolyline(poly, bhColour, 2);
        }

        /***************************************************/
        /**** Public Methods  - Surfaces                ****/
        /***************************************************/

        public static void RenderRhinoWires(RHG.Extrusion surface, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawSurface(surface, bhColour, 2);
        }

        /***************************************************/

        public static void RenderRhinoWires(RHG.NurbsSurface surface, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawSurface(surface, bhColour, 2);
        }

        /***************************************************/

        public static void RenderRhinoWires(RHG.Brep brep, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawBrepWires(brep, bhColour, 0);
        }


        /***************************************************/
        /**** Public Methods - Solids                   ****/
        /***************************************************/

        public static void RenderRhinoWires(RHG.Cone cone, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawCone(cone, bhColour);
        }

        /***************************************************/

        public static void RenderRhinoWires(RHG.Cylinder cylinder, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawCylinder(cylinder, bhColour);
        }

        /***************************************************/

        public static void RenderRhinoWires(RHG.Sphere sphere, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawSphere(sphere, bhColour);
        }

        /***************************************************/

        public static void RenderRhinoWires(RHG.Torus torus, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawTorus(torus, bhColour);
        }


        /***************************************************/
        /**** Public Methods  - Mesh                    ****/
        /***************************************************/

        public static void RenderRhinoWires(RHG.Mesh mesh, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            if (mesh.VertexColors.Count == 0)
                pipeline.DrawMeshWires(mesh, bhColour);
            else if (GH.CentralSettings.PreviewMeshEdges)
                pipeline.DrawMeshWires(mesh, bhColour);
        }


        /***************************************************/
        /**** Public Methods  - Miscellanea             ****/
        /***************************************************/

        public static void RenderRhinoWires(RHG.BoundingBox bbBox, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {
            pipeline.DrawBox(bbBox, bhColour, 2);
        }


        /***************************************************/
        /**** Public Methods  - Representations         ****/
        /***************************************************/

        public static void RenderRhinoWires(Text3d text3D, Rhino.Display.DisplayPipeline pipeline, Color bhColour)
        {

            pipeline.Draw3dText(text3D, bhColour, text3D.TextPlane); 
        }

        /***************************************************/
    }
}


