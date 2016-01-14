using ApplicationAnalyzer.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationAnalyzer.CustomControls
{
    class ButtonToolBar : TableLayoutPanel
    {
        ToolBarCommitButton commitButton;
        ToolBarDeleteButton deleteButton;

        public ButtonToolBar()
        {
            this.commitButton = new ToolBarCommitButton();
            this.commitButton.SetBounds(0, 0, 50, 50);
            //
            // Create DeleteButton
            //
            this.deleteButton = new ToolBarDeleteButton();
            deleteButton.SetBounds(0, 0, 50, 50);
            //
            // Create ToolBar and add Buttons to it
            //
            this.ColumnCount = 6;
            this.RowCount = 1;
            this.Height = 60;
            this.Dock = DockStyle.Fill;
            this.Controls.Add(commitButton, 0, 0);
            this.Controls.Add(deleteButton, 1, 0);
        }

        public ToolBarDeleteButton DeleteButton {
            get
            {
                return deleteButton;
            }
        }

        public ToolBarCommitButton CommitButton
        {
            get
            {
                return commitButton;
            }
        }

    }
}
