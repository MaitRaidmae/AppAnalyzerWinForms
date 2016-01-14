using ApplicationAnalyzer.Buttons;
using ApplicationAnalyzer.Misc;
using Oracle.DataAccess.Client;
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
    public partial class ApplicationsScreen : Form
    {
        public ApplicationsScreen()
        {
            InitializeComponent();

            LoadApplicationsGrid();
            this.tableLayoutPanel1.Controls.Add(this.applicationsGrid, 1,1);
            this.applicationsGrid.Dock = DockStyle.Fill;
            this.applicationsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.applicationsGrid.CellDoubleClick += new DataGridViewCellEventHandler(this.CellDoubleClick);
        }

        private void ApplicationsScreen_Load(object sender, EventArgs e)
        {
            this.applicationsGrid.Columns[0].Visible = false;
            PrettyColumnNames();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ApplicationChecksScreen applicationChecksScreen = new ApplicationChecksScreen();
            // Set the Parent Form of the Child window.
            applicationChecksScreen.MdiParent = this.MdiParent;
            int application = Convert.ToInt32(applicationsGrid.SelectedRows[0].Cells[0].Value);
            applicationChecksScreen.SetApplicationCode(application);
            // Display the new form.
            applicationChecksScreen.Show();
        }

        private void TickButton_Click(object sender, EventArgs e)
        {
  
        }
        
    }
}
