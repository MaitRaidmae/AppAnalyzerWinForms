using ApplicationAnalyzer.Misc;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationAnalyzer.Forms
{
    partial class ApplicationsScreen
    {
        private DataGridView applicationsGrid;
        
        private void LoadApplicationsGrid(int results) {
            
            this.applicationsGrid = new DataGridView();
            this.applicationsGrid.ReadOnly = true;
            DataTable table = new DataTable();
            OracleDataReader dataCursor = SQLExecutor.GetResults("B_APPLICATIONS", results);
            table.Load(dataCursor);
            applicationsGrid.DataSource = table;
        }

        private void LoadApplicationsGrid()
        {
            LoadApplicationsGrid(500);
        }

        private void DeleteRow(int code)
        {
            SQLExecutor.DeleteRow("B_APPLICATIONS",code);
        }

        private void UpdateRow()
        {
            int thing = this.applicationsGrid.SelectedRows[0].Index;

        }

        private void InsertRow() { }

        private void PrettyColumnNames()
        {
            for (int i = 0; i < this.applicationsGrid.Columns.Count; i++)
            {

                string colName = applicationsGrid.Columns[i].HeaderText;
                string prettyName = SQLExecutor.GetPrettyName("B_APPLICATIONS",colName);
                applicationsGrid.Columns[i].HeaderText = prettyName;

            }
        }
    }
}
