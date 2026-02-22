using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department.Ather_File
{


    public class ThemedToolStripRenderer : ToolStripProfessionalRenderer
    {
        private readonly bool _dark;

        public ThemedToolStripRenderer(bool dark)
            : base(new ProfessionalColorTable())
        {
            _dark = dark;
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            var bg = _dark ? Color.FromArgb(28, 28, 28) : SystemColors.Control;
            using (var b = new SolidBrush(bg))
            {
                e.Graphics.FillRectangle(b, e.AffectedBounds);
            }
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            var item = e.Item;

            Color bg;
            if (item.Selected)
                bg = _dark ? Color.FromArgb(45, 90, 140) : SystemColors.Highlight;
            else
                bg = _dark ? Color.FromArgb(28, 28, 28) : SystemColors.Control;

            using (var b = new SolidBrush(bg))
            {
                e.Graphics.FillRectangle(b, new Rectangle(Point.Empty, item.Size)); 
            }
                
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = _dark ? Color.FromArgb(230, 230, 230) : Color.Black;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            var line = _dark ? Color.FromArgb(55, 55, 55) : SystemColors.ControlDark;
            var r = new Rectangle(0, 3, e.Item.Width, 1);
            using (var p = new Pen(line))
            {
                e.Graphics.DrawLine(p, r.Left + 2, r.Top, r.Right - 2, r.Top);

            }
               
        }
    }
}
