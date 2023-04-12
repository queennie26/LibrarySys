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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace LibrarySys
{
    public partial class Borrower : Form
    {
        OleDbConnection conn;
        OleDbCommand com;
        OleDbDataReader dr;

        public Borrower()
        {
            InitializeComponent();
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Queennie\\OneDrive\\Documents\\libsys.mdb");
        }

        private void Borrower_Load(object sender, EventArgs e)
        {
            loadDatagrid();
            date.Text = DateTime.Now.ToLongDateString();
            time.Text = DateTime.Now.ToLongTimeString();
        }

        private void loadDatagrid()
        {
            conn.Open();

            OleDbCommand com = new OleDbCommand("Select * from book order by borrow_Id asc", conn);
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
            string numb = borrow.Text;

            DialogResult dr = MessageBox.Show("Are you sure you want to delete this?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                OleDbCommand com = new OleDbCommand("Delete from borrower where borrow_Id = '" + numb + "'", conn);
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
            borrow.Text = datagrid.Rows[e.RowIndex].Cells["borrow_Id"].Value.ToString();
            name.Text = datagrid.Rows[e.RowIndex].Cells["first_name"].Value.ToString();
            lastname.Text = datagrid.Rows[e.RowIndex].Cells["last_name"].Value.ToString();
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            conn.Open();

            OleDbCommand com = new OleDbCommand("Select * from borrower where first_name like'%" + search.Text + "%'", conn);
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

            OleDbCommand com = new OleDbCommand("Insert into borrower values ('" + borrow.Text + "','" + name.Text + "','" + lastname.Text + "')", conn);

            com.ExecuteNonQuery();

            MessageBox.Show("Succesfully SAVED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            conn.Close();
            loadDatagrid();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            borrow.Clear();
            name.Clear();
            lastname.Clear();
        }

        private void update_Click(object sender, EventArgs e)
        {
            conn.Open();
            //string no;
            int nom;
            nom = int.Parse(borrow.Text);

            OleDbCommand com = new OleDbCommand("Update borrower SET first_name= '" + name.Text + "', last_name='" + lastname.Text + "' where borrow_Id= '" + nom + "'", conn);

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
