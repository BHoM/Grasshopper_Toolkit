﻿using Alligator.Components;
using BHoM.Structural.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHE = Grasshopper_Engine;
using BHE = BHoM.Structural.Elements;
using BHI = BHoM.Structural.Interface;
using BHR = BHoM.Base.Results;
using Grasshopper.Kernel;
using Grasshopper_Engine.Components;

namespace Alligator.Structural.Results
{
    public class GetPanelForce : ResultBaseComponent<PanelForce>
    {
        public GetPanelForce() : base("GetPanelForce", "PanelForce", "Gets the panel forces from the selected result server", "Structure", "Results") { }

        /// <summary> Icon (24x24 pixels)</summary>
        protected override System.Drawing.Bitmap Internal_Icon_24x24
        {
            get { return Alligator.Properties.Resources.BHoM_GetPanelForce; }
        }

        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{F5EA6887-0BA4-4661-A736-8A7D1948BEFA}");
            }
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            if (GHE.DataUtils.Run(DA, 3))
            {
                BHI.IResultAdapter app = GHE.DataUtils.GetGenericData<BHI.IResultAdapter>(DA, 0);
                if (app != null)
                {
                    List<string> ids = GHE.DataUtils.GetDataList<string>(DA, 1);
                    List<string> cases = GHE.DataUtils.GetDataList<string>(DA, 2);
                    Dictionary<string, BHR.IResultSet> results = new Dictionary<string, BHR.IResultSet>();
                    app.GetPanelForces(ids, cases, m_ResultOrder, out results);

                    SetResults<PanelForce>(DA, results);
                }
            }
        }
    }

    public class GetPanelStress : ResultBaseComponent<PanelStress>
    {
        public GetPanelStress() : base("GetPanelStress", "PanelStress", "Gets the panel stresses from the selected result server", "Structure", "Results") { }

        /// <summary> Icon (24x24 pixels)</summary>
        protected override System.Drawing.Bitmap Internal_Icon_24x24
        {
            get { return Alligator.Properties.Resources.BHoM_GetPanelStress; }
        }

        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{ABEE6887-0BA4-4661-A736-8A7D1948BEFA}");
            }
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            if (GHE.DataUtils.Run(DA, 3))
            {
                BHI.IResultAdapter app = GHE.DataUtils.GetGenericData<BHI.IResultAdapter>(DA, 0);
                if (app != null)
                {
                    List<string> ids = GHE.DataUtils.GetDataList<string>(DA, 1);
                    List<string> cases = GHE.DataUtils.GetDataList<string>(DA, 2);
                    Dictionary<string, BHR.IResultSet> results = new Dictionary<string, BHR.IResultSet>();
                    app.GetPanelStress(ids, cases, m_ResultOrder, out results);

                    SetResults<PanelStress>(DA, results);
                }
            }
        }
    }
}