using System;
using System.Collections.Generic;
using System.Data;
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
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

            DataTable table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Size", typeof(int));
            table.Columns.Add("%", typeof(int));

            //if (Directory.Exists(Path))
            //{
            //    //podkatologi
            //    string[] dirs = Directory.GetDirectories(Path);   
                
            //    //faili
            //    string[] fileS = Directory.GetFiles(Path);

            //   // FolderCount = dirs.Length + fileS.Length;

            //    for (int i = 0; i < dirs.Length; i++)
            //        table.Rows.Add(dirs[i]);

            //    for (int i = 0; i < fileS.Length; i++)
            //        table.Rows.Add(fileS[i]);
            //}

            //    //files.Add(new File(dirs[i], 0, 0));
                
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
            }

            textBox1.Text = Path;

            if (!String.IsNullOrEmpty(Path))
            {
                button2.Enabled = true;
                button4.Enabled = true;
            }
            Logic.SumFolder(Path);

           // nujno sdelat zapusk programmi(poisk razmera)
        }

        private void StepBack_Click(object sender, EventArgs e)
        {
            int s = Path.Where(c => c == '\\').Count();
            int indexOfSubstring = Path.LastIndexOf('\\');

            if (s == 1)
            {              
                indexOfSubstring++;
                button2.Enabled = false;
            }

            step.Push(Path.Substring(indexOfSubstring));
            Path = Path.Remove(indexOfSubstring);
            textBox1.Text = Path;
            button3.Enabled = true;
        }

        private void StepForward_Click(object sender, EventArgs e)
        {

            if (step.Count == 1)
                button3.Enabled = false;

            Path = Path + step.Pop();
            textBox1.Text = Path;
            button2.Enabled = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Path = textBox1.Text;

            if (Directory.Exists(Path))
            {
               button2.Enabled = true;
            }
            else
            {
                MessageBox.Show("Такого пути нет.");
            }

            button4.Enabled = true;
            //nujno sdelat zapusk programmi(poisk razmera)
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

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
