﻿using System;
using Rhino;
using Rhino.Commands;
using Grasshopper.Kernel;
using BH.oM.Base;
using BH.oM.UI;
using BH.Engine.Grasshopper;
using BH.Engine.Alligator.Objects;
using System.Collections.Generic;
using System.Linq;
using BH.UI.Templates;
using System.Windows.Forms;
using Grasshopper.Kernel.Special;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;

namespace BH.UI.Alligator.Templates
{
    public abstract class CallerValueList : GH_ValueList
    {
        /*******************************************/
        /**** Properties                        ****/
        /*******************************************/

        protected abstract MultiChoiceCaller Caller { get; }

        protected override System.Drawing.Bitmap Icon { get { return Caller.Icon_24x24; } }

        public override Guid ComponentGuid { get { return Caller.Id; } }

        public override GH_Exposure Exposure { get { return (GH_Exposure)Math.Pow(2, Caller.GroupIndex); } }


        /*******************************************/
        /**** Constructors                      ****/
        /*******************************************/

        public CallerValueList() : base()
        { 
            Name = Caller.Name;
            NickName = Caller.Name;
            Description = Caller.Description;
            Category = "Alligator";
            SubCategory = Caller.Category;
            ListItems.Clear();

            m_Accessor = new DataAccessor_GH();
            Caller.SetDataAccessor(m_Accessor);

            Caller.ItemSelected += DynamicCaller_ItemSelected;
        }


        /*******************************************/
        /**** Override Methods                  ****/
        /*******************************************/

        protected override void CollectVolatileData_Custom()
        {
            this.m_data.Clear();
            List<GH_ValueListItem>.Enumerator enumerator = this.SelectedItems.GetEnumerator();
            while (enumerator.MoveNext())
            {
                GH_ValueListItem item = enumerator.Current;
                int index = 0;
                item.Value.CastTo<int>(out index);
                object result = Caller.Run(new object[] { index });
                this.m_data.Append(result.IToGoo(), new GH_Path(0));
            }
        }

        /*******************************************/

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            GH_DocumentObject.Menu_AppendSeparator(menu);

            if (Caller.Selector != null)
                Caller.Selector.AddToMenu(menu);
        }

        /*******************************************/

        public override bool Write(GH_IO.Serialization.GH_IWriter writer)
        {
            if (Caller.Selector != null)
                writer.SetString("Component", Caller.Selector.Write());

            int index = ListItems.IndexOf(FirstSelectedItem);
            if (index >= 0)
                writer.SetInt32("Selection", index);

            return base.Write(writer);
        }

        /*************************************/

        public override bool Read(GH_IO.Serialization.GH_IReader reader)
        {
            if (!base.Read(reader))
                return false;

            if (Caller.Selector != null)
            {
                string callerString = ""; reader.TryGetString("Component", ref callerString);
                if (Caller.Selector.Read(callerString))
                {
                    int selection = -1;
                    reader.TryGetInt32("Selection", ref selection);
                    if (selection >= 0 && selection < ListItems.Count)
                        this.SelectItem(selection);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }


        /*******************************************/
        /**** Private Methods                   ****/
        /*******************************************/

        protected void DynamicCaller_ItemSelected(object sender, object e)
        {
            this.NickName = Caller.Name;
            this.Name = Caller.Name;

            ListItems.Clear();
            List<string> names = Caller.GetChoiceNames();
            for (int i = 0; i < names.Count; i++)
                ListItems.Add(new GH_ValueListItem(names[i], i.ToString()));

            this.ExpireSolution(true);
        }


        /*******************************************/
        /**** Private Fields                    ****/
        /*******************************************/

        private DataAccessor_GH m_Accessor = null;


        /*******************************************/
    }
}