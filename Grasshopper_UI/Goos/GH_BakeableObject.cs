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
using BH.Engine.Reflection;
using System.Drawing;

namespace BH.UI.Grasshopper.Goos
{
    public class GH_BakeableObject<T> : GH_BHoMGoo<T>, IGH_PreviewData, IGH_BakeAwareData, GH_ISerializable
    {
        /*******************************************/
        /**** Properties                        ****/
        /*******************************************/

        public override string TypeName { get; } = "ViewableObject";

        public override string TypeDescription { get; } = "Contains a generic object that can be viewed in Rhino";

        public override bool IsValid { get { return Value != null; } }

        public virtual Rhino.Geometry.BoundingBox ClippingBox { get { return Bounds(); } }

        public virtual Rhino.Geometry.BoundingBox Boundingbox { get { return Bounds(); } }

        public override T Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value;
                try { SetGeometry(); } catch { }
            }
        }


        /*******************************************/
        /**** Constructors                      ****/
        /*******************************************/

        public GH_BakeableObject() : base() { }

        /***************************************************/

        public GH_BakeableObject(T val) : base(val) { }


        /*******************************************/
        /**** Override Methods                  ****/
        /*******************************************/

        public override IGH_Goo Duplicate()
        {
            return new GH_BakeableObject<T> { Value = Value };
        }

        /***************************************************/

        public virtual Rhino.Geometry.BoundingBox GetBoundingBox(Rhino.Geometry.Transform xform)
        {
            Rhino.Geometry.BoundingBox box = Bounds();
            box.Transform(xform);
            return box;
        }

        /***************************************************/

        public override bool CastFrom(object source)
        {
            if (source == null)
                return false;

            if (source is IGH_Goo)
                return CastFrom(Helpers.IFromGoo<T>((IGH_Goo)source));

            if (source.GetType().Namespace.StartsWith("Rhino.Geometry"))
                source = BH.Engine.Rhinoceros.Convert.IFromRhino(source);

            return base.CastFrom(source);
        }

        /***************************************************/

        public override bool CastTo<Q>(ref Q target)
        {
            try
            {
                if (Value == null)
                    target = default(Q);
                if (target is IGH_GeometricGoo || target is GH_Transform || target is GH_Matrix || target is GH_Vector)
                    return Helpers.CastToGoo(m_RhinoGeometry as dynamic, ref target);
                else
                    return Helpers.CastToGoo(Value as dynamic, ref target);
            }
            catch (Exception)
            {
                string message = string.Format("Impossible to convert {0} into {1}. Check the description of each input for more details on the type of object that need to be provided", Value.GetType().FullName, typeof(Q).FullName);
                BH.Engine.Reflection.Compute.RecordError(message);
                return false;
            }
        }

        /***************************************************/

        public override bool Read(GH_IReader reader)
        {
            string json = "";
            reader.TryGetString("Json", ref json);

            if (json != null && json.Length > 0)
            {
                object fromJson = BH.Engine.Serialiser.Convert.FromJson(json);
                try
                {
                    Value = (T)fromJson;
                }
                catch
                {
                    string message = string.Format("Impossible to convert {0} into {1}. Check the description of each input for more details on the type of object that need to be provided", fromJson.GetType().FullName, typeof(T).IToText());
                    BH.Engine.Reflection.Compute.RecordError(message);
                    return false;
                }
            }


            return true;
        }

        /***************************************************/

        public override bool Write(GH_IWriter writer)
        {
            if (Value != null)
                writer.SetString("Json", Value.ToJson());
            return true;
        }

        /***************************************************/

        public virtual void DrawViewportMeshes(GH_PreviewMeshArgs args)
        {
            if (m_RhinoGeometry != null)
                Render.IRenderRhinoMeshes(m_RhinoGeometry, args, m_Color);
        }

        /***************************************************/

        public virtual void DrawViewportWires(GH_PreviewWireArgs args)
        {
            if (m_RhinoGeometry != null)
                Render.IRenderRhinoWires(m_RhinoGeometry, args, m_Color);
        }


        /***************************************************/
        /**** Private Method                            ****/
        /***************************************************/

        private bool SetGeometry()
        {
            if (Value == null)
            {
                return true;
            }
            else if (Value is IRepresentation)
            {
                m_RhinoGeometry = (Value as IRepresentation).IToRhino();
                m_Color = (Value as IRepresentation).Colour;
                if(Value is GeometricalRepresentation)
                    m_Geometry = (Value as GeometricalRepresentation).Geometry;
                return true;
            }
            else if (Value is BHoMObject)
            {
                m_Geometry = (Value as BHoMObject).IGeometry();
                m_RhinoGeometry = m_Geometry.IToRhino();
                return true;
            }
            else if (Value is IGeometry)
            {
                m_Geometry = Value as IGeometry;
                m_RhinoGeometry = m_Geometry.IToRhino();
                m_Color = Color.FromArgb(80, 255, 41, 105);//BHoM pink!
                return true;
            }
            else
            {
                return false;
            }
        }

        /***************************************************/

        private Rhino.Geometry.BoundingBox Bounds()
        {
            try
            {
                if (Value == null)
                    return Rhino.Geometry.BoundingBox.Empty;

                BH.oM.Geometry.BoundingBox bhBox = m_Geometry.IBounds();
                if (bhBox == null)
                    return Rhino.Geometry.BoundingBox.Empty;

                return bhBox.ToRhino();
            }
            catch
            {
                return Rhino.Geometry.BoundingBox.Empty;
            }
        }

        /***************************************************/
        /**** IGH_BakeAwareData methods                 ****/
        /***************************************************/

        public bool BakeGeometry(RhinoDoc doc, ObjectAttributes att, out Guid obj_guid)
        {
            if (m_RhinoGeometry != null)
            {
                if (att == null)
                    att = new ObjectAttributes();

                BHoMObject bhObj = Value as BHoMObject;

                if (string.IsNullOrEmpty(att.Name) && bhObj != null && !string.IsNullOrWhiteSpace(bhObj.Name))
                    att.Name = bhObj.Name;

                obj_guid = doc.Objects.Add(GH_Convert.ToGeometryBase(m_RhinoGeometry), att);
                return true;
            }

            obj_guid = Guid.Empty;
            return false;
        }

        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        protected IGeometry m_Geometry = null;

        protected object m_RhinoGeometry = null;

        protected Color m_Color = new Color();

        /***************************************************/
    }
}