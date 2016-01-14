using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAnalyzer.Buttons
{
    class ToolBarCommitButton : PictureButton
    {
        public ToolBarCommitButton()
        {
            Bitmap button = new Bitmap(Properties.Resources.GreenTickButton, new Size(50, 50));
            Bitmap buttonPressed = new Bitmap(Properties.Resources.GreenTickButtonPressed, new Size(50, 50));
            this.BackgroundImage = button;
            this.PressedImage = buttonPressed;
        }

    }
}
