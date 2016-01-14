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

    

    partial class ApplicationChecksScreen
    {

        DataGridView applicationsGrid;
        DataGridView applicationChecksGrid;

        private int applicationsCode; // Set this if this is form has objects with parents

        private void LoadApplicationsGrid(int results) {
            this.applicationsGrid = new DataGridView();
            this.applicationsGrid.ReadOnly = true;
            DataTable dataTable = new DataTable();
            OracleDataReader dataCursor = SQLExecutor.GetRows("B_APPLICATIONS", "APL_CODE", this.applicationsCode);
            dataTable.Load(dataCursor);
            dataTable = Utils.TransposeDataTable(dataTable); //Transpose for gridView
            applicationsGrid.DataSource = dataTable;
        }

        private void LoadApplicationsGrid()
        {
            LoadApplicationsGrid(999999999);
        }

        private void LoadApplicationChecksGrid(int results)
        {
            this.applicationChecksGrid = new DataGridView();
            this.applicationChecksGrid.ReadOnly = true;
            DataTable dataTable = new DataTable();
            OracleDataReader dataCursor = SQLExecutor.GetRows("V_APPLICATION_CHECKS", "APL_CODE", this.applicationsCode);
            dataTable.Load(dataCursor);
            applicationChecksGrid.DataSource = dataTable;
        }
        
        private void LoadApplicationChecksGrid()
        {
            LoadApplicationChecksGrid(999999999);
        }

        public void SetApplicationCode(int currentApplication)
        {
            this.applicationsCode = currentApplication;
        }


        public void ApplicationsGridPrettyNames()
        {
            for (int i = 0; i < this.applicationsGrid.Rows.Count; i++)
            {
                string colName = this.applicationsGrid.Rows[i].Cells[1].Value.ToString();
                string prettyName = SQLExecutor.GetPrettyName("V_APPLICATION_CHECKS", colName);
                this.applicationsGrid.Rows[i].Cells[0].Value = prettyName;

            }
        }

        private void ApplicationChecksGridPrettyNames()
        {
            for (int i = 0; i < this.applicationChecksGrid.Columns.Count; i++)
            {

                string colName = this.applicationChecksGrid.Columns[i].HeaderText;
                string prettyName = SQLExecutor.GetPrettyName("V_APPLICATION_CHECKS", colName);
                this.applicationChecksGrid.Columns[i].HeaderText = prettyName;

            }
        }



    }
}
