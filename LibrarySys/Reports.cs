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
            loadDatagrid();
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Queennie\\OneDrive\\Documents\\libsys.mdb");
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            fillChart();
        }

        private void loadDatagrid()
        {
            conn.Open();

            OleDbCommand com = new OleDbCommand("Select * from transaction order by date desc", conn);
            com.ExecuteNonQuery();

            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            datagrid.DataSource = tab;

            conn.Close();

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string curdhead = "LIBRARY REPORTS";
            e.Graphics.DrawString(curdhead, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 350, 50);

            string l1 = "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
            e.Graphics.DrawString(l1, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 0, 100);

            string g1 = "transaction_type";
            e.Graphics.DrawString(g1, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 768, 140);

            string g2 = "booknum";
            e.Graphics.DrawString(g2, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 425, 140);

            string g3 = "Title";
            e.Graphics.DrawString(g3, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 550, 140);

            string g4 = "Author";
            e.Graphics.DrawString(g4, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 680, 140);

            string g5 = "Id_num";
            e.Graphics.DrawString(g5, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 150, 140);

            string g6 = "First Name";
            e.Graphics.DrawString(g6, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 240, 140);

            string g7 = "Last Name";
            e.Graphics.DrawString(g7, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 345, 140);

            string g8 = "transaction_date";
            e.Graphics.DrawString(g8, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 65, 140);

            string g9 = "Printed By: Admin";
            e.Graphics.DrawString(g9, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 640, 1050);

            string l2 = "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
            e.Graphics.DrawString(l2, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 0, 160);

            int height = 165;
            for (int l = numberOfItemsPrintedSoFar; l < datagrid.Rows.Count; l++)
            {
                numberOfItemsPerPage = numberOfItemsPerPage + 1;
                if (numberOfItemsPerPage <= 50)
                {
                    numberOfItemsPrintedSoFar++;

                    if (numberOfItemsPrintedSoFar <= datagrid.Rows.Count)
                    {

                        height += datagrid.Rows[0].Height;
                        e.Graphics.DrawString(datagrid.Rows[l].Cells[0].FormattedValue.ToString(), datagrid.Font = new Font("Book Antiqua", 7), Brushes.Black, new RectangleF(20, height, 150, datagrid.Rows[0].Height), new StringFormat() { Alignment = StringAlignment.Center });
                        e.Graphics.DrawString(datagrid.Rows[l].Cells[1].FormattedValue.ToString(), datagrid.Font = new Font("Book Antiqua", 7), Brushes.Black, new RectangleF(120, height, 150, datagrid.Rows[0].Height), new StringFormat() { Alignment = StringAlignment.Center });
                        e.Graphics.DrawString(datagrid.Rows[l].Cells[2].FormattedValue.ToString(), datagrid.Font = new Font("Book Antiqua", 7), Brushes.Black, new RectangleF(200, height, 150, datagrid.Rows[0].Height), new StringFormat() { Alignment = StringAlignment.Center });
                        e.Graphics.DrawString(datagrid.Rows[l].Cells[3].FormattedValue.ToString(), datagrid.Font = new Font("Book Antiqua", 7), Brushes.Black, new RectangleF(300, height, 150, datagrid.Rows[0].Height), new StringFormat() { Alignment = StringAlignment.Center });
                        e.Graphics.DrawString(datagrid.Rows[l].Cells[4].FormattedValue.ToString(), datagrid.Font = new Font("Book Antiqua", 7), Brushes.Black, new RectangleF(380, height, 150, datagrid.Rows[0].Height), new StringFormat() { Alignment = StringAlignment.Center });
                        e.Graphics.DrawString(datagrid.Rows[l].Cells[5].FormattedValue.ToString(), datagrid.Font = new Font("Book Antiqua", 7), Brushes.Black, new RectangleF(490, height, 150, datagrid.Rows[0].Height), new StringFormat() { Alignment = StringAlignment.Center });
                        e.Graphics.DrawString(datagrid.Rows[l].Cells[6].FormattedValue.ToString(), datagrid.Font = new Font("Book Antiqua", 7), Brushes.Black, new RectangleF(630, height, 150, datagrid.Rows[0].Height), new StringFormat() { Alignment = StringAlignment.Center });
                        e.Graphics.DrawString(datagrid.Rows[l].Cells[7].FormattedValue.ToString(), datagrid.Font = new Font("Book Antiqua", 7), Brushes.Black, new RectangleF(720, height, 150, datagrid.Rows[0].Height), new StringFormat() { Alignment = StringAlignment.Center });

                    }
                    else
                    {
                        e.HasMorePages = false;
                    }

                }
                else
                {
                    numberOfItemsPerPage = 0;
                    e.HasMorePages = true;
                    return;

                }


            }
            numberOfItemsPerPage = 0;
            numberOfItemsPrintedSoFar = 0;
        }

        private void print_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.PrintDialog PrintDialog1 = new PrintDialog();
            PrintDialog1.AllowSomePages = true;
            PrintDialog1.ShowHelp = true;
            PrintDialog1.Document = printDocument1;
            DialogResult result = PrintDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printrev_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void fillChart()
        {
            DataSet ds = new DataSet();
            conn.Open();
            OleDbDataAdapter adapt = new OleDbDataAdapter("SELECT title, Quantity FROM book", conn);
            adapt.Fill(ds);
            chart.DataSource = ds.Tables[0];
            //set the member of the chart data source used to data bind to the X-values of the series  
            chart.Series["Books"].XValueMember = "title";
            //set the member columns of the chart data source used to data bind to the X-values of the series  
            chart.Series["Books"].YValueMembers = "Quantity";
            chart.Titles.Add("Library Books Graph");
            conn.Close();
        }

        private void back_Click(object sender, EventArgs e)
        {
            MainPage mp = new MainPage();
            mp.ShowDialog();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogIn lg = new LogIn();
            MessageBox.Show("Log out Successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lg.Show();
            this.Hide();
        }
    }
}