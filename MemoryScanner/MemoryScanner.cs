using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MemoryScanner
{
    public partial class MemoryScanner : Form
    {
        public MemoryScanner()
        {
            InitializeComponent();
        }

        List<FileStruct> files = new List<FileStruct>();
        DataTable table = new DataTable();
        string Path;
        Stack<string> step = new Stack<string>();
        Point lastPoint;      

        private void Form1_Load(object sender, EventArgs e)
        {
            ButtonBack.Enabled = false;
            ButtonForward.Enabled = false;
            table.Columns.Add("Name", typeof(string));         
            table.Columns.Add("Size (MB)", typeof(string));
            table.Columns.Add("%", typeof(string));
            table.Columns.Add("Tipe", typeof(string));
        }

        private void FillinInTable(DataTable table)
        {
            table.Clear();
            for (int i = 0; i < files.Count; i++)
            {
                string temp1;

                if (files[i].isCatalog == true)
                    temp1 = "Catalog";
                else 
                    temp1 = "File";


                if (files[i].size == 0)
                {
                    table.Rows.Add(files[i].name, "No Access", "No Access", temp1);
                }
                else if (files[i].percent == -1)
                {
                    table.Rows.Add(files[i].name, files[i].size, "No Access", temp1);
                }
                else
                {
                    table.Rows.Add(files[i].name, files[i].size, Math.Round(files[i].percent, 1), temp1);
                }

                Table.DataSource = table;
            }
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

            int s = Path.Where(c => c == '\\').Count();
            textBox1.Text = Path;
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();

            if (!String.IsNullOrEmpty(Path))
            {
                if (s > 1)
                ButtonBack.Enabled = true;
                ButtonClear.Enabled = true;
            }

            files = Communicate.GetFiles(Path);
            FillinInTable(table);
        }

        private void ButtonAccept_Click(object sender, EventArgs e)
        {
            Path = textBox1.Text;

            if (Directory.Exists(Path))
            {
                ButtonBack.Enabled = true;
                files = Communicate.GetFiles(Path);
                FillinInTable(table);
                ButtonClear.Enabled = true;
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Введите или выберите путь.");
            }
            else
            {
                MessageBox.Show("Такого пути нет.");
            }
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
            table.Clear();
            ButtonBack.Enabled = false;
            ButtonForward.Enabled = false;
        }

        private void ButtonManual_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Мануал будет доступен в следующей версии");
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }        
    }
}
