using ApplicationAnalyzer.Buttons;
using ApplicationAnalyzer.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationAnalyzer.Forms
{
    public partial class CheckSuitsScreen : Form
    {

        public CheckSuitsScreen()
        {
            InitializeComponent();

            LoadToolBar();
        }

        private void CheckSuitsScreen_Load(object sender, EventArgs e)
        {
            LoadCheckSuitsGrid();
            
            this.tableLayoutPanel1.Controls.Add(this.checkSuitsGrid, 1, 1);
            checkSuitsGrid.AutoSize = true;
            checkSuitsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.checkSuitsGrid.Dock = DockStyle.Fill;
            checkSuitsGrid.MouseClick += CheckSuitsGrid_MouseClick;
            checkSuitsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            checkSuitsGrid.RowTemplate.DefaultCellStyle.Padding = new Padding(2,2,2,2);
            checkSuitsGrid.RowTemplate.ReadOnly = true;
            checkSuitsGrid.Columns["CHS_DATETIME"].ReadOnly = true;
            checkSuitsGrid.Columns["CHS_CODE"].Visible = false;
            checkSuitsGrid.RowLeave += CheckSuitsGrid_RowLeave;
        }

        private void CheckSuitsGrid_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            checkSuitsGrid.Rows[e.RowIndex].ReadOnly = true;
        }


        private void CheckSuitsGrid_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    ContextMenu contextMenu = new ContextMenu();
                    MenuItem menuItemEdit = new MenuItem("&Edit", MenuItemEdit_Click);
                    MenuItem menuItemDelete = new MenuItem("&Delete", MenuItemDelete_Click);

                    var hti = checkSuitsGrid.HitTest(e.X, e.Y);
                    if (hti.RowIndex >= 0)
                    {
                        checkSuitsGrid.ClearSelection();
                        checkSuitsGrid.Rows[hti.RowIndex].Selected = true;
                    }

                    contextMenu.MenuItems.Add(menuItemEdit);
                    contextMenu.MenuItems.Add(menuItemDelete);
                    checkSuitsGrid.ContextMenu = contextMenu;
                    contextMenu.Show(checkSuitsGrid, new Point(e.X, e.Y));
                    break;
            }
        }

        private void MenuItemEdit_Click(object sender, EventArgs e)
        {
            checkSuitsGrid.ClearSelection();
            checkSuitsGrid.SelectedRows[0].ReadOnly = false;
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

    }
}
