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
    public partial class Books : Form
    {
        OleDbConnection conn;
        OleDbCommand com;
        OleDbDataReader dr;
        public Books()
        {
            InitializeComponent();
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Queennie\\OneDrive\\Documents\\libsys.mdb");
        }

        private void Books_Load(object sender, EventArgs e)
        {
            
            loadDatagrid();
            date.Text = DateTime.Now.ToLongDateString();
            time.Text = DateTime.Now.ToLongTimeString();
        }

        private void loadDatagrid()
        {
            conn.Open();

            OleDbCommand com = new OleDbCommand("Select * from book order by booknum asc", conn);
            com.ExecuteNonQuery();

            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            datagrid.DataSource = tab;

            conn.Close();

        }

        private void delete_Click(object sender, EventArgs e)
        {
            conn.Open();
            string num = bookNum.Text;

            DialogResult dr = MessageBox.Show("Are you sure you want to delete this?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                OleDbCommand com = new OleDbCommand("Delete from book where booknum = '" + num + "'", conn);
                com.ExecuteNonQuery();
                MessageBox.Show("Successfully DELETED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("CANCELLED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            conn.Close();
            loadDatagrid();
        }

        private void datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bookNum.Text = datagrid.Rows[e.RowIndex].Cells["booknum"].Value.ToString();
            title.Text = datagrid.Rows[e.RowIndex].Cells["title"].Value.ToString();
            author.Text = datagrid.Rows[e.RowIndex].Cells["author"].Value.ToString();
            quantity1.Text = datagrid.Rows[e.RowIndex].Cells["quantity"].Value.ToString();
            maxquanty.Text = datagrid.Rows[e.RowIndex].Cells["maxQuantity"].Value.ToString();
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            conn.Open();

            OleDbCommand com = new OleDbCommand("Select * from book where title like'%" + search.Text + "%'", conn);
            com.ExecuteNonQuery();

            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            datagrid.DataSource = tab;

            conn.Close();
        }

        private void add_Click(object sender, EventArgs e)
        {
            conn.Open();

            OleDbCommand com = new OleDbCommand("Insert into book values ('" + bookNum.Text + "','" + title.Text + "','" + author.Text + "','" + quantity1.Text + "','" + maxquanty.Text + "')", conn);

            com.ExecuteNonQuery();

            MessageBox.Show("Succesfully SAVED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            conn.Close();
            loadDatagrid();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            bookNum.Clear();
            title.Clear();
            author.Clear();
            quantity1.Clear();
            maxquanty.Clear();
        }

        private void update_Click(object sender, EventArgs e)
        {
            conn.Open();
            //string no;
            int no;
            no = int.Parse(bookNum.Text);

            OleDbCommand com = new OleDbCommand("Update book SET title= '" + title.Text + "', author='" + author.Text + "', quantity='" + quantity1.Text + "' where booknum= '" + no + "'", conn);

            com.ExecuteNonQuery();

            MessageBox.Show("Successfully UPDATED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            conn.Close();
            loadDatagrid();
        }

        private void back_Click(object sender, EventArgs e)
        {
            MainPage mp = new MainPage();
            this.Hide();
            mp.ShowDialog();
        }
    }
}
