using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibrarySys
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {

        }

        private void bOOKSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Books books = new Books();
            this.Hide();
            books.ShowDialog();
        }

        private void bORROWERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Borrower borrower = new Borrower();
            this.Hide();
            borrower.ShowDialog();
        }

        private void tRANSACTIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Transaction tran = new Transaction();
            this.Hide();
            tran.ShowDialog();
            
        }

        private void rEORTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports rep = new Reports();
            this.Hide();
            rep.ShowDialog();
          
        }
    }
}
