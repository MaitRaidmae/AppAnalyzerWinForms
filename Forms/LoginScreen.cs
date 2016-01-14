using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using ApplicationAnalyzer.Misc;

namespace ApplicationAnalyzer
{
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void LoginButtonClick(object sender, EventArgs e)
        {
            ConnectionManager connMan = ConnectionManager.Connection;
            
            OracleConnection conn = connMan.NewConnection("Hundisilm","dummyPassword"); // C#

            OracleCommand cmd = new OracleCommand();

            cmd.Connection = conn;

            cmd.CommandText = "select * from B_SUITE_SCENARIOS"; cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();

            dr.Read();

            label1.Text = dr.GetInt64(0).ToString();

            conn.Dispose();
        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
