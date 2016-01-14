using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAnalyzer.Misc
{
    class ConnectionManager : IDisposable
    {
        public static readonly ConnectionManager connMan = new ConnectionManager();

        public OracleConnection connection;

        private ConnectionManager() {}

        public static ConnectionManager Connection
        {
            get
            {
                return connMan;
            }
        }


        public OracleConnection NewConnection(string username, string password)
        {
            string oradb = "Data Source=HUNDIPLUG12; User Id="+ username + "; Password="+ password + ";";
            connection = new OracleConnection(oradb); // C#
            connection.Open();
            return connection;
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public OracleConnection GetConnection()
        {
            return connection;
        }

        }
    }
