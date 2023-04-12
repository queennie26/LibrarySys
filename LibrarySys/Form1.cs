using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibrarySys
{
    public partial class Form1 : Form
    {
        OleDbConnection conn;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Lourdes\\source\\repos\\LibrarySys\\libsys.mdb");
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text;
            string email = txtemail.Text;
            string password = txtpassword.Text;

            if (!IsPasswordStrong(password))
            {
                MessageBox.Show("Please enter a stronger password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtpassword.Text != txtconfirmpassword.Text)
            {
                MessageBox.Show("Please enter same password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string encryptedPassword = EncryptionHelper.EncryptPassword(password);
            AddRecord(username, email, encryptedPassword);
            MessageBox.Show("Sign Up Successfully. You can login now.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide();
            LogIn log = new LogIn();
            log.ShowDialog();
        }

        private bool IsPasswordStrong(string password)
        {
            // Add password complexity rules here using regular expressions
            return true;
        }

        private void AddRecord(string username, string email, string password)
        {
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Users values(@email, @username, @password)", conn);
            cmd.Parameters.AddWithValue("email", email);
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("password", password);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Hide();
            LogIn login = new LogIn();
            login.Show();
        }

        public static class EncryptionHelper
        {
            public static string EncryptPassword(string password)
            {
                byte[] salt = Encoding.UTF8.GetBytes("somerandomsalt"); // Add a random salt for extra security
                using (var sha256 = SHA256.Create())
                {
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                    byte[] passwordWithSalt = new byte[passwordBytes.Length + salt.Length];
                    passwordBytes.CopyTo(passwordWithSalt, 0);
                    salt.CopyTo(passwordWithSalt, passwordBytes.Length);
                    byte[] hashedPassword = sha256.ComputeHash(passwordWithSalt);
                    return Convert.ToBase64String(hashedPassword);
                }
            }
            public static bool DecryptPassword(string hashedPassword, string password)
            {
                byte[] salt = Encoding.UTF8.GetBytes("somerandomsalt"); // Add the same salt used for encryption
                byte[] hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
                using (var sha256 = SHA256.Create())
                {
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                    byte[] passwordWithSalt = new byte[passwordBytes.Length + salt.Length];
                    passwordBytes.CopyTo(passwordWithSalt, 0);
                    salt.CopyTo(passwordWithSalt, passwordBytes.Length);
                    byte[] hashedPasswordToCheck = sha256.ComputeHash(passwordWithSalt);
                    return hashedPasswordBytes.SequenceEqual(hashedPasswordToCheck);
                }
            }
        }
    }
}
