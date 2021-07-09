using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel.Attributes;
using Grasshopper.Kernel;
using Grasshopper.GUI.Canvas;
using Grasshopper.GUI;
using System.Drawing;

namespace BH.UI.Grasshopper.Templates
{
    public class CallerAttribute : GH_ComponentAttributes
    {

        public CallerAttribute(IGH_Component component) : base(component)
        {

        }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            base.Render(canvas, graphics, channel);

            //Draws an orange textbox on top of the black textbox
            if (channel == GH_CanvasChannel.Objects)
            {
                GH_Capsule capsuleName = GH_Capsule.CreateTextCapsule(this.m_innerBounds, this.m_innerBounds, GH_Palette.Warning, this.Owner.NickName, GH_FontServer.Large, GH_Orientation.vertical_center, 3, 6);
                capsuleName.Render(graphics, this.Selected, this.Owner.Locked, false);
                capsuleName.Dispose();

                //PointF topRight = new PointF(this.Bounds.Right - 24, this.Bounds.Top);
                //graphics.DrawImage(BH.UI.Grasshopper.Properties.Resources.alpha, topRight);

                PointF topRight = new PointF(this.Bounds.Right, this.Bounds.Top + GH_FontServer.Standard.Size / 2);
                System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(Color.OrangeRed);
                // new StringFormat(StringFormatFlags.DirectionRightToLeft)
                graphics.DrawString("alpha", GH_FontServer.Standard, brush, topRight, GH_TextRenderingConstants.FarCenter);
            }


        }
    }
}
