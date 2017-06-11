using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSE
{
    public partial class StartPage : Form
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string x = textBox2.Text;
            bool correct = false;
            foreach (char c in x)
            {
                if (c != ' ')
                {


                    correct = true;

                }
            }
            if (correct == true)
            {
                Form1 frm = new Form1(textBox2.Text);
                this.Hide();
                frm.ShowDialog();

                this.Close();
            }
            
            else
                MessageBox.Show("Please Enter A Correct Query");


        }







        private void StartPage_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

    }
}

