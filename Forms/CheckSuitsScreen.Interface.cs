using ApplicationAnalyzer.CustomControls;
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
    partial class CheckSuitsScreen
    {
        DataGridView checkSuitsGrid;
        DataTable checkSuitsDataTable;
        ButtonToolBar buttonToolBar = new ButtonToolBar();

        private void LoadCheckSuitsGrid(int results)
        {
            this.checkSuitsGrid = new DataGridView();
            this.checkSuitsDataTable = new DataTable();
            this.checkSuitsGrid.EditMode = DataGridViewEditMode.EditOnEnter;
            OracleDataReader dataCursor = SQLExecutor.GetResults("B_CHECK_SUITS", results);
            this.checkSuitsDataTable.Load(dataCursor);
            this.checkSuitsDataTable.TableName = "B_CHECK_SUITS";
            this.checkSuitsDataTable.Columns["CHS_CODE"].AllowDBNull = true;
            this.checkSuitsDataTable.Columns["CHS_DATETIME"].AllowDBNull = true;
            this.checkSuitsGrid.DataSource = this.checkSuitsDataTable;
            this.checkSuitsGrid.AllowUserToDeleteRows = true;
        }

        private void CheckSuitsGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            checkSuitsGrid.AllowUserToAddRows = false;
        }

        private void LoadCheckSuitsGrid()
        {
            LoadCheckSuitsGrid(9999999);
        }

        private void DeleteItem()
        {
            checkSuitsGrid.Rows.RemoveAt(checkSuitsGrid.SelectedRows[0].Index);
        }

        private void CommitChanges()
        {
            DataTable changesTable = this.checkSuitsDataTable.GetChanges();
            if (changesTable != null) { 
                changesTable.TableName = this.checkSuitsDataTable.TableName;
                SQLExecutor.CommitChanges(changesTable);
            }

        }

        private void CommitButton_Click(object sender, EventArgs e)
        {
            CommitChanges();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void LoadToolBar()
        {
            tableLayoutPanel1.Controls.Add(buttonToolBar, 1, 0);
            buttonToolBar.DeleteButton.Click += DeleteButton_Click;
            buttonToolBar.CommitButton.Click += CommitButton_Click;
        }


    }
}
