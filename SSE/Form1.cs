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
    public partial class Form1 : Form
    {
            
        public Form1(string query)
        {
         
            InitializeComponent();
           
            Functions.InverseIndexing();
            Functions.GetQuery(query.ToLower());
            Functions.BatchesFiller();
            if (Functions.Batches.Count == 1)
            {

                MessageBox.Show("No Result Has Been Found");
            }
            Functions.Output();
            Functions.Batches.Clear();
            Functions.WordOccurenceInFiles.Clear();
            Functions.QueryWords.Clear();
            int y = 200, reslabloc = 16, wordloc = 60, firstloc = 100, resloc = 20;
            counter = 0;
            resetform();
            LinkLabel[] ResultLabel = new LinkLabel[Functions.counter + 1];
            while (counter < Functions.counter)   //while there is results
            {
                Label WordsLabels = new Label();    //Create new Label
                Label FirstLines = new Label();
                ResultLabel[counter] = new LinkLabel();    //Create a new linklabel
                Label Result = new Label();
                ResultLabel[counter].Click += new EventHandler(ResultLabel_Click);     //create an eventhandler to each linklabel to know which linklabel has been pressed
                // Initialize the Label and LinkLabel controls.
                ResultLabel[counter].Location = new Point(16, reslabloc);
                // ResultLabel[counter].Size = new Size(1400, 30);
                ResultLabel[counter].AutoSize = true;
                ResultLabel[counter].Text = Functions.filenames.getvalue(counter);
                ResultLabel[counter].Font = new Font("Arial", 18);
                WordsLabels.Location = new Point(16, wordloc);
                WordsLabels.Text = Functions.body2.getvalue(counter);
                WordsLabels.Font = new Font("Italic", 13);
                //WordsLabels.Size = new Size(1200, 100);
                WordsLabels.AutoSize = true;
                //WordsLabels.MaximumSize = new Size(0,30);
                FirstLines.Location = new Point(10, firstloc);
                FirstLines.Text = Functions.body3.getvalue(counter);
                FirstLines.Font = new Font("Arial Rounded MT", 8);
                FirstLines.AutoSize =true;
                Result.Location = new Point(120, resloc);
                Result.AutoSize = true;
                Result.Font = new Font("Arial", 13);
                Result.Text = Functions.body1.getvalue(counter);
                // Add the Label and LinkLabel controls to the Panel.
                panel1.Controls.Add(ResultLabel[counter]);
                panel1.Controls.Add(WordsLabels);
                panel1.Controls.Add(FirstLines);
                panel1.Controls.Add(Result);
                y += 180;
                reslabloc += 150;
                wordloc += 150;
                firstloc += 150;
                resloc += 150;
                counter++;
            }
        }
        public int counter = 0,counter2=0;
        
        private void resetform()
        {
            panel1.Controls.Clear();
            foreach (Control c in panel1.Controls)
                c.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
       
            Functions.GetQuery(textBox2.Text.ToLower());
            Functions.BatchesFiller();
                 if(Functions.Batches.Count==1)
            {

                MessageBox.Show("No Result Has Been Found");
            }
                 

                     Functions.Output();
                     Functions.Batches.Clear();
                     Functions.WordOccurenceInFiles.Clear();
                     Functions.QueryWords.Clear();
                     int y = 200, reslabloc = 16, wordloc = 60, firstloc = 100, resloc = 20;
                     counter = 0;
                     resetform();
                     LinkLabel[] ResultLabel = new LinkLabel[Functions.counter + 1];

                     while (counter < Functions.counter)   //while there is results
                     {

                         Label WordsLabels = new Label();    //Create new Label
                         Label FirstLines = new Label();
                         ResultLabel[counter] = new LinkLabel();    //Create a new linklabel
                         Label Result = new Label();
                         ResultLabel[counter].Click += new EventHandler(ResultLabel_Click);     //create an eventhandler to each linklabel to know which linklabel has been pressed
                         // Initialize the Label and LinkLabel controls.
                         ResultLabel[counter].Location = new Point(16, reslabloc);
                         // ResultLabel[counter].Size = new Size(1400, 30);
                         ResultLabel[counter].AutoSize = true;
                         ResultLabel[counter].Text = Functions.filenames.getvalue(counter);
                         ResultLabel[counter].Font = new Font("Arial", 18);
                         WordsLabels.Location = new Point(16, wordloc);
                         WordsLabels.Text = Functions.body2.getvalue(counter);
                         WordsLabels.Font = new Font("Italic", 13);
                         //WordsLabels.Size = new Size(1200, 100);
                         WordsLabels.AutoSize = true;
                         //WordsLabels.MaximumSize = new Size(0,30);
                         FirstLines.Location = new Point(16, firstloc);
                         FirstLines.Text = Functions.body3.getvalue(counter);
                         FirstLines.Font = new Font("Arial Rounded MT", 12);
                         FirstLines.AutoSize = true;
                         Result.Location = new Point(120, resloc);
                         Result.AutoSize = true;
                         Result.Font = new Font("Arial", 13);
                         Result.Text = Functions.body1.getvalue(counter);
                         //  Add the Label and LinkLabel controls to the Panel.

                         panel1.Controls.Add(ResultLabel[counter]);
                         panel1.Controls.Add(WordsLabels);
                         panel1.Controls.Add(FirstLines);
                         panel1.Controls.Add(Result);
                         y += 180;
                         reslabloc += 150;
                         wordloc += 150;
                         firstloc += 150;
                         resloc += 150;
                         counter++;

                 
           
            }
        }

       
        void ResultLabel_Click(object sender, EventArgs s)
        {
            LinkLabel lb = (LinkLabel)sender;//Get the linklabel pressed
            System.Diagnostics.Process.Start(Functions.dir + @"\Files\" + lb.Text);
            //MessageBox.Show(lb.Text);

        }
        void FileClick(object sender, EventArgs s)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}
