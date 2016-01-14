using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAnalyzer.Buttons
{
    class ToolBarDeleteButton : PictureButton
    {
        public ToolBarDeleteButton()
        {
            Bitmap button = new Bitmap(Properties.Resources.DeleteButton, new Size(50, 50));
            Bitmap buttonPressed = new Bitmap(Properties.Resources.DeleteButtonPressed, new Size(50, 50));
            this.BackgroundImage = button;
            this.PressedImage = buttonPressed;
        }
    }
}
