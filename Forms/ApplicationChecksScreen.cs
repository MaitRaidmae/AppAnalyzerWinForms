using ApplicationAnalyzer.Misc;
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
    public partial class ApplicationChecksScreen : Form
    {

        public ApplicationChecksScreen()
        {

            InitializeComponent();
            Console.Write("LoadGrid"); //TODO Remove this;
            
        }

        private void ApplicationChecksScreen_Load(object sender, EventArgs e)
        {
            LoadApplicationsGrid();

            this.tableLayoutPanel1.Controls.Add(this.applicationsGrid, 1, 1);
            this.applicationsGrid.Dock = DockStyle.Fill;
            this.applicationsGrid.AutoSize = true;
            this.applicationsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.applicationsGrid.ColumnHeadersVisible = false;
            this.applicationsGrid.RowHeadersVisible = false;
            this.applicationsGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;
            this.applicationsGrid.AllowUserToAddRows = false;
            //ApplicationsGridPrettyNames();
            
            LoadApplicationChecksGrid();
            this.tableLayoutPanel1.Controls.Add(this.applicationChecksGrid, 2, 1);
            this.applicationChecksGrid.Dock = DockStyle.Fill;
            this.applicationChecksGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.applicationChecksGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.applicationChecksGrid.ColumnHeadersVisible = true;
            this.applicationChecksGrid.RowHeadersVisible = false;
            this.applicationsGrid.Columns[0].Width = 150;
            this.applicationChecksGrid.Columns["APL_CODE"].Visible = false;
            this.applicationChecksGrid.AllowUserToAddRows = false;

            ApplicationChecksGridPrettyNames();

        }
    }
}
