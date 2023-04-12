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
using static LibrarySys.Form1;

namespace LibrarySys
{
    public partial class LogIn : Form
    {
        OleDbConnection conn;
        OleDbCommand com;
        OleDbDataReader dr;

        public LogIn()
        {
            InitializeComponent();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Queennie\\OneDrive\\Documents\\libsys.mdb");
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {

            conn.Open();
            if (textbPassword.Text != string.Empty || textbUsername.Text != string.Empty)
            {
                string encryptedPassword = EncryptionHelper.EncryptPassword(textbPassword.Text);

                com = new OleDbCommand("Select * from Users where username = '" + textbUsername.Text + "' and password = '" + encryptedPassword + "'", conn);
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    this.Hide();
                    MainPage home = new MainPage();
                    home.ShowDialog();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Account Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }

        private void linkSignup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Form1 reg = new Form1();
            this.Hide();
            reg.ShowDialog();
        }
    }
}
