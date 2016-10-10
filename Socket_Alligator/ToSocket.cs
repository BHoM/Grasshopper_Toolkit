﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using GHE = Grasshopper_Engine;

namespace Alligator.Socket
{
    public class ToSocket : GH_Component
    {
        public ToSocket() : base("ToSocket", "ToSocket", "Send string to a socket", "Alligator", "Socket") { }

        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("0EEFD0B8-CD8E-44FF-9144-2DF685A93EE7");
            }
        }

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("IP address", "address", "IP address of the socket to send data to", GH_ParamAccess.item);
            pManager.AddIntegerParameter("port", "port", "port used by the socket", GH_ParamAccess.item);
            pManager.AddTextParameter("data", "data", "data t osend", GH_ParamAccess.item);
            pManager.AddBooleanParameter("active", "active", "check if the component currently allows data transfer", GH_ParamAccess.item, false);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddBooleanParameter("success", "success", "data transfer succesful", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string address = ""; DA.GetData<string>(0, ref address);
            int port = 8888; DA.GetData<int>(1, ref port);
            string data = ""; DA.GetData<string>(2, ref data);
            bool active = false; DA.GetData<bool>(3, ref active);

            if (!active) return;

            bool success = Socket_Engine.SocketLink.SendData(address, port, data);
            DA.SetData(0, success);
        }
    }
}