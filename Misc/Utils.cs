using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationAnalyzer.Misc
{
    class Utils
    {
        public static string replaceFirst(string input, string findString, string newString)
        {
            int pos = input.IndexOf(findString);
            if (pos < 0)
            {
                return input;
            }
            return input.Substring(0, pos) + newString + input.Substring(pos + findString.Length);

        }

        //Returns transposed Datatable - ColumnHeaders are set to Column 0 values,
        // this is meant for single row source tables (key value kind of thingies).
        public static DataTable TransposeDataTable(DataTable sourceTable)
        {
            DataTable transposedTable = new DataTable();

            transposedTable.Columns.Add("Field Name");
            transposedTable.Columns.Add("Value");

            for (int rowCount = 1; rowCount < sourceTable.Columns.Count; rowCount++)
            {
                DataRow newRow = transposedTable.NewRow();
                newRow["Field Name"] = sourceTable.Columns[rowCount].ColumnName;
                newRow["Value"] = sourceTable.Rows[0][rowCount];

                transposedTable.Rows.Add(newRow);

            }

            return transposedTable;
        }

    }
}
