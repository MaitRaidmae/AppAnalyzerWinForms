using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationAnalyzer.Buttons
{
    class ToolStripTickButton : ToolStripButton
    {
        public ToolStripTickButton()
        {
            this.Image = Properties.Resources.GreenTickButton;
            this.AutoSize = false;
            this.Width = 50;
            this.Height = 50;
          
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Image = Properties.Resources.GreenTickButton;
            this.ImageScaling = ToolStripItemImageScaling.SizeToFit;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.Image = Properties.Resources.GreenTickButton;
            this.ImageScaling = ToolStripItemImageScaling.SizeToFit;
        }


    }
}
