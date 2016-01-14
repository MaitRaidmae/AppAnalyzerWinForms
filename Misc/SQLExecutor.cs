using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Data;
using ApplicationAnalyzer.Misc;

namespace ApplicationAnalyzer.Misc
{
    class SQLExecutor
    {

        public static OracleDataReader GetResults(string tableName, int nrOfResults)
        {
            return GetResults(tableName, nrOfResults, null, null);
        }

        public static OracleDataReader GetResults(string tableName, int nrOfResults, string findByField, int? findByKey)
        {
            ConnectionManager connMan = ConnectionManager.Connection;
            //TODO remove the debug portion of getting new connection
            OracleConnection connection = connMan.NewConnection("Hundisilm", "dummyPassword");
            //OracleConnection connection = connMan.GetConnection();

            OracleCommand objCmd = new OracleCommand();
            String packageName = GetPackageName(tableName);
            objCmd.Connection = connection;
            objCmd.CommandText = packageName + ".GET_RESULTS_PAGE";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("par_page_nr", OracleDbType.Int32).Value = 1;
            objCmd.Parameters.Add("par_results_per_page", OracleDbType.Int32).Value = nrOfResults;
            objCmd.Parameters.Add("result_set", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            if (findByField != null)
            {
                objCmd.Parameters.Add("par_find_by_field", OracleDbType.Varchar2).Value = findByField;
                objCmd.Parameters.Add("par_find_by_value", OracleDbType.Int32).Value = findByKey;
            }

            try
            {
                OracleDataReader objReader = objCmd.ExecuteReader();
                return objReader;
            }
            catch (Exception sqle)
            {
                Console.WriteLine("Exception: {0}", sqle.ToString());
            }
            return null;
        }

        // Gets
        public static OracleDataReader GetRows(string tableName, string fieldName, int key)
        {
            String packageName = GetPackageName(tableName);
            OracleCommand command = CallOracleProcedure(packageName + ".GET_ROWS");
            command.Parameters.Add("par_field_name", OracleDbType.Varchar2).Value = fieldName;
            command.Parameters.Add("par_key", OracleDbType.Int32).Value = key;
            command.Parameters.Add("par_results", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            OracleDataReader dataReader = command.ExecuteReader();
            return dataReader;
        }

        public static void DeleteRow(string tableName, Int32 rowCode)
        {
            try
            {
                if (!(rowCode > 0))
                {
                    throw new Exception("rowCode: " + rowCode + ". Bad value - rowCode must be greater than zero.");
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            string packageName = GetPackageName(tableName);
            OracleCommand command = CallOracleProcedure(packageName + ".DELETE_ROW");
            command.Parameters.Add("par_code", OracleDbType.Varchar2).Value = rowCode;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
            }

        }

        //Gets pretty names from database
        public static string GetPrettyName(string tableName, string columnName)
        {
            OracleCommand command = CallOracleProcedure("MISC_UTILS.PRETTY_NAME");
            //Return value *has* to be added first (goddamnit to hell)
            command.Parameters.Add("pretty_name", OracleDbType.Varchar2, 32767);
            command.Parameters["pretty_name"].Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add("PAR_TABLE_NAME", OracleDbType.Varchar2, 32767).Value = tableName;
            command.Parameters.Add("PAR_COLUMN_NAME", OracleDbType.Varchar2, 32767).Value = columnName;
            command.ExecuteNonQuery();
            return command.Parameters["pretty_name"].Value.ToString();
        }

        public static void CommitChanges(DataTable changesTable)
        {

            string packageName = GetPackageName(changesTable.TableName);
            OracleCommand insertCommand = CallOracleProcedure(packageName + ".INSERT_ROW");
            OracleCommand updateCommand = CallOracleProcedure(packageName + ".UPDATE_ROW");
            OracleCommand deleteCommand = CallOracleProcedure(packageName + ".DELETE_ROW");

            foreach (DataRow row in changesTable.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {

                    foreach (DataColumn column in changesTable.Columns)
                    {
                        if (column.Ordinal > 0) //Skip first code column for Insert Procedure;
                        {
                            AddParameters(ref insertCommand, column, row);
                        }
                    }
                    insertCommand.ExecuteNonQuery();
                }
                else if (row.RowState == DataRowState.Modified)
                {
                    foreach (DataColumn column in changesTable.Columns)
                    {
                        AddParameters(ref updateCommand, column, row);
                    }
                    updateCommand.ExecuteNonQuery();
                } else if (row.RowState == DataRowState.Deleted)
                {
                    DataColumn column = changesTable.Columns[0];
                    Decimal code = (Decimal)row[column, DataRowVersion.Original];
                    deleteCommand.Parameters.Add("code",OracleDbType.Int32).Value = code;
                    deleteCommand.ExecuteNonQuery();
                }
                
            }
        }

        public static void DeleteRow()
        {

        }


        // Returns OracleCommand with type and connection set 
        public static OracleCommand CallOracleProcedure(string procedure)
        {

            ConnectionManager connMan = ConnectionManager.Connection;
            //TODO remove the debug portion of getting new connection
            OracleConnection connection = connMan.NewConnection("Hundisilm", "dummyPassword");
            //OracleConnection connection = connMan.GetConnection();

            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedure;
            return command;
        }


        private static String GetPackageName(String tableName)
        {
            String packageName = tableName.ToUpper();
            switch (packageName.Substring(0, 2))
            {
                case "B_":
                    packageName = Utils.replaceFirst(packageName, "B_", "P_");
                    break;
                case "V_":
                    packageName = Utils.replaceFirst(packageName, "V_", "P_");
                    break;
            }
            return packageName;
        }


        private static void AddParameters(ref OracleCommand command, DataColumn column, DataRow row)
        {
            Object itemValue;

            if (row.ItemArray[column.Ordinal] == null)
            {
                itemValue = DBNull.Value;
            } else
            {
                itemValue = row.ItemArray[column.Ordinal];
            }

            if (column.DataType == Type.GetType("System.String"))
            {
               command.Parameters.Add(column.ColumnName, OracleDbType.Varchar2).Value = itemValue;
            }
            else if (column.DataType == Type.GetType("System.Int32"))
            {
                command.Parameters.Add(column.ColumnName, OracleDbType.Int32).Value = itemValue;
            }
            else if (column.DataType == Type.GetType("System.Decimal"))
            {
                command.Parameters.Add(column.ColumnName, OracleDbType.Decimal).Value = itemValue;
            }
            else if (column.DataType == Type.GetType("System.DateTime"))
            {
                command.Parameters.Add(column.ColumnName, OracleDbType.TimeStamp).Value = itemValue;
            }
        }

    }

}

