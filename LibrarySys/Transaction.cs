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
    public partial class Transaction : Form
    {
        OleDbConnection conn;
        OleDbCommand com;
        OleDbDataReader dr;

        public Transaction()
        {
            InitializeComponent();
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Queennie\\OneDrive\\Documents\\libsys.mdb");
        }

        private void Transaction_Load(object sender, EventArgs e)
        {

        }
       

        private void borrow_Click(object sender, EventArgs e)
        {
            if (idnum.Text != string.Empty && quantity.Text != string.Empty)
            {
                conn.Open();
                int no;
                int value = 1;
                no = int.Parse(booknum.Text);
                string titlee = title.Text;
                string authorr = author.Text;
                string lnamee = lname.Text;
                string fnamee = fname.Text;
                int id;
                id = int.Parse(idnum.Text);
                DateTime dt = DateTime.Now;
                string sqlFormattedDate = dt.ToString("yyyy-MMMM-dd hh:mm tt");
                string r = "BORROWED";

                int oldquantity = Convert.ToInt32(quantity.Text);
                int newquantity = oldquantity - value;

                if (newquantity >= 0)
                {
                    conn.Close();
                    string query = "SELECT COUNT(*) FROM id_borrower WHERE Idnum = ?";
                    OleDbCommand command = new OleDbCommand(query, conn);
                    command.Parameters.AddWithValue("@Idnum", id);

                    // open the connection and execute the query
                    conn.Open();
                    int count = (int)command.ExecuteScalar();

                    // check if the username already exists
                    if (count > 0)
                    {
                        conn.Close();
                        MessageBox.Show("User already borrowed a book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        conn.Close();
                        conn.Open();
                        OleDbCommand com = new OleDbCommand("UPDATE book SET Quantity= '" + newquantity + "', title= '" + titlee + "', author='" + authorr + "'  WHERE booknum = '" + no + "'", conn);
                        com.ExecuteNonQuery();

                        conn.Close();
                        loadDatagrid();

                        MessageBox.Show("Successfully UPDATED!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDatagrid();

                        conn.Open();
                        OleDbCommand com1 = new OleDbCommand("INSERT INTO transaction ([transaction_type], [booknum], [title], [author], [name], [lastname], [transaction_date]) VALUES (@tansaction_type, @booknum, @title, @author, @name, @lastname, @transaction_date)", conn);
                        com1.Parameters.AddWithValue("@transaction_type", r);
                        com1.Parameters.AddWithValue("@booknum", no);
                        com1.Parameters.AddWithValue("@title", titlee);
                        com1.Parameters.AddWithValue("@author", authorr);
                        com1.Parameters.AddWithValue("@Id_num", id);
                        com1.Parameters.AddWithValue("@name", fnamee);
                        com1.Parameters.AddWithValue("@lastname", lnamee);
                        com1.Parameters.AddWithValue("@transaction_date", sqlFormattedDate);
                        com1.ExecuteNonQuery();

                        conn.Close();
                        loadDatagrid();

                        conn.Open();
                        OleDbCommand com2 = new OleDbCommand("INSERT INTO id_borrower ([Idnum], [book_Id]) VALUES (@Idnum, @book_Id)", conn);
                        com2.Parameters.AddWithValue("@Idnum", id);
                        com2.Parameters.AddWithValue("@book_Id", no);

                        com2.ExecuteNonQuery();

                        conn.Close();
                        loadDatagrid();
                    }
                }
                else
                {
                    MessageBox.Show("The Book is not available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Please complete all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
            EmptyTextBox();

        }

        private void datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            booknum.Text = datagrid.Rows[e.RowIndex].Cells["booknum"].Value.ToString();
            title.Text = datagrid.Rows[e.RowIndex].Cells["title"].Value.ToString();
            author.Text = datagrid.Rows[e.RowIndex].Cells["author"].Value.ToString();
            quantity.Text = datagrid.Rows[e.RowIndex].Cells["quantity"].Value.ToString();
            maxquanty.Text = datagrid.Rows[e.RowIndex].Cells["maxQuantity"].Value.ToString();
        }

        private void EmptyTextBox()
        {
            booknum.Text = "";
            author.Text = "";
            title.Text = "";
            quantity.Text = "";
            maxquanty.Text = "";

        }

        private void returnn_Click(object sender, EventArgs e)
        {
            if (idnum.Text != string.Empty && quantity.Text != string.Empty)
            {
                conn.Open();
                int no;
                int value = 1;
                no = int.Parse(booknum.Text);
                string titlee = title.Text;
                string authorr = author.Text;
                string lnamee = lname.Text;
                string fnamee = fname.Text;
                int id;
                id = int.Parse(idnum.Text);
                DateTime dt = DateTime.Now;
                string sqlFormattedDate = dt.ToString("yyyy-MMMM-dd hh:mm tt");
                string r = "RETURNED";

                int oldquantity = Convert.ToInt32(quantity.Text);
                int maxquantity = Convert.ToInt32(maxquanty.Text);
                int newquantity = oldquantity + value;


                if (newquantity <= maxquantity)
                {
                    conn.Close();
                    string query = "SELECT COUNT(*) FROM id_borrower WHERE Idnum = ? AND book_Id =?";
                    OleDbCommand command = new OleDbCommand(query, conn);
                    command.Parameters.AddWithValue("@Idnum", id);
                    command.Parameters.AddWithValue("@book_Id", no);

                    // open the connection and execute the query
                    conn.Open();
                    int count = (int)command.ExecuteScalar();

                    // check if the username already exists
                    if (count > 0)
                    {
                        OleDbCommand com = new OleDbCommand("UPDATE book SET Quantity= '" + newquantity + "', title= '" + titlee + "', author='" + authorr + "'  WHERE booknum = '" + no + "'", conn);
                        com.ExecuteNonQuery();

                        conn.Close();
                        loadDatagrid();

                        MessageBox.Show("Successfully UPDATED!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDatagrid();

                        conn.Open();
                        OleDbCommand com1 = new OleDbCommand("INSERT INTO transaction ([transaction_type], [booknum], [title], [author], [name], [lastname], [transaction_date]) VALUES (@tansaction_type, @booknum, @title, @author, @name, @lastname, @transaction_date)", conn);
                        com1.Parameters.AddWithValue("@transaction_type", r);
                        com1.Parameters.AddWithValue("@booknum", no);
                        com1.Parameters.AddWithValue("@title", titlee);
                        com1.Parameters.AddWithValue("@author", authorr);
                        com1.Parameters.AddWithValue("@Id_num", id);
                        com1.Parameters.AddWithValue("@name", fnamee);
                        com1.Parameters.AddWithValue("@lastname", lnamee);
                        com1.Parameters.AddWithValue("@transaction_date", sqlFormattedDate);
                        com1.ExecuteNonQuery();

                        conn.Close();
                        loadDatagrid();

                        conn.Open();
                        OleDbCommand com2 = new OleDbCommand("Delete from id_borrower where Idnum= '" + id + "'", conn);
                        com2.ExecuteNonQuery();

                        conn.Close();
                        loadDatagrid();
                    }
                    else
                    {
                        conn.Close();
                        MessageBox.Show("Transaction error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                }
                else
                {
                    MessageBox.Show("All the books have been returned.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Please complete all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void retrieve_Click(object sender, EventArgs e)
        {
            if (idnum.Text != string.Empty)
            {
                conn.Open();
                int idNum = int.Parse(idnum.Text);


                OleDbCommand com = new OleDbCommand("Select * from borrower where borrow_Id = '" + idNum + "'", conn);
                OleDbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    fname.Text = reader.GetValue(1).ToString();
                    lname.Text = reader.GetValue(2).ToString();
                }
                reader.Close();
                conn.Close();
            }
            else
            {
                MessageBox.Show("Please complete all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void labalsearch_Click(object sender, EventArgs e)
        {

        }

        private void back_Click(object sender, EventArgs e)
        {
            MainPage mp = new MainPage();
            mp.ShowDialog();
            this.Hide();
        }
    }
}
