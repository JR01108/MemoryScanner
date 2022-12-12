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
            public double size;
            public double percent;
            public bool isCatalog;

            public File(string _name, double _size, double _percent, bool _isCatalog) 
            { 
                name = _name;
                size = _size;
                percent = _percent;
                this.isCatalog = _isCatalog;
            }                
        }

        List<File> files = new List<File>();
        DataTable table = new DataTable();
        string Path;
        Stack<string> step = new Stack<string>();
        Point lastPoint;      

        private void Form1_Load(object sender, EventArgs e)
        {
            ButtonBack.Enabled = false;
            ButtonForward.Enabled = false;
            table.Columns.Add("Name", typeof(string));          
            table.Columns.Add("Size (MB)", typeof(int));
            table.Columns.Add("%", typeof(double));
            table.Columns.Add("Tipe", typeof(string)); ;
        }   
        
        private void FillinInTable(DataTable table)
        {
            for (int i = 0; i < files.Count; i++)
            {
                string temp;
                if (files[i].isCatalog == true)
                    temp = "Catalog";
                else
                    temp = "File";

                table.Rows.Add(files[i].name, files[i].size, Math.Round(files[i].percent, 1) , temp);
            }

            Table.DataSource = table;
        } 

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

            files = Communicate.GetFiles(Path);
            FillinInTable(table);
        }    
        
        private void button5_Click(object sender, EventArgs e)
        {
            Path = textBox1.Text;

            if (Directory.Exists(Path))
            {
                ButtonBack.Enabled = true;
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Введите или выберите путь.");
            }
            else
            {
                MessageBox.Show("Такого пути нет.");
            }

            files = Communicate.GetFiles(Path);
            FillinInTable(table);
            ButtonClear.Enabled = true;            
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

        private void button2_Click(object sender, EventArgs e)
        {
            //manual
        }
    }
}
