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
    public partial class ApplicationAnalyzerMain : Form
    {
        public ApplicationAnalyzerMain()
        {
            InitializeComponent();
        }

        private void MDIChildeNew_Click(object sender, EventArgs e)
        {
            ApplicationsScreen newMDIChild = new ApplicationsScreen();
            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void checkSuitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSuitsScreen newMDIChild = new CheckSuitsScreen();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }
    }
}
