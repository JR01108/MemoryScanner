using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MemoryScanner
{
    public partial class MemoryScanner : Form
    {
        public MemoryScanner()
        {
            InitializeComponent();
        }

        public struct File
        {
            public string name;
            public int size;
            public int percent;

            public File(string _name, int _size, int _percent) 
            { 
                name = _name;
                size = _size;
                percent = _percent;
            }                
        }

        List<File> files = new List<File>();
    
        private void Form1_Load(object sender, EventArgs e)
        {

            ButtonBack.Enabled = false;
            ButtonForward.Enabled = false;

            DataTable table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Size", typeof(int));
            table.Columns.Add("%", typeof(int));
               
        }     
       
        private string Path;

        Stack<string> step = new Stack<string>();

        private void ChoiceOfPath(object sender, EventArgs e)
        {
            step.Clear();
            
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                Path = folderBrowserDialog1.SelectedPath;
                ButtonForward.Enabled = false;
            }

            textBox1.Text = Path;

            if (!String.IsNullOrEmpty(Path))
            {
                ButtonBack.Enabled = true;
                ButtonClear.Enabled = true;
            }

           // nujno sdelat zapusk programmi(poisk razmera)
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            Path = textBox1.Text;

            if (Directory.Exists(Path))
            {
                ButtonBack.Enabled = true;
            }
            else
            {
                MessageBox.Show("Такого пути нет.");
            }

            ButtonClear.Enabled = true;
            //nujno sdelat zapusk programmi(poisk razmera)
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            int s = Path.Where(c => c == '\\').Count();
            int indexOfSubstring = Path.LastIndexOf('\\');

            if (s == 1)
            {
                indexOfSubstring++;
                ButtonBack.Enabled = false;
            }

            step.Push(Path.Substring(indexOfSubstring));
            Path = Path.Remove(indexOfSubstring);
            textBox1.Text = Path;
            ButtonForward.Enabled = true;
        }

        private void ButtonForward_Click(object sender, EventArgs e)
        {
            if (step.Count == 1)
                ButtonForward.Enabled = false;

            Path = Path + step.Pop();
            textBox1.Text = Path;
            ButtonBack.Enabled = true;
        }

        Point lastPoint;
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            ButtonBack.Enabled = false;
            ButtonForward.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //
        }
        
        private void TableClick(object sender, DataGridViewCellEventArgs e)
        {
            //
        }

        private void TextBox_Path(object sender, EventArgs e)
        {
            //       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //
        }

       
    }
}
