using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibrarySys
{
    public partial class Reports : Form
    {
        private OleDbConnection conn;
        OleDbCommand com;
        OleDbDataReader dr;

        
        private int numberOfItemsPerPage = 0;
        private int numberOfItemsPrintedSoFar = 0;

        public Reports()
        {
            InitializeComponent();
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Queennie\\OneDrive\\Documents\\libsys.mdb");
        }

        private void Reports_Load(object sender, EventArgs e)
        {

        }
    }
}
