using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

            if (Directory.Exists(Path))
            {
                //podkatologi
                string[] dirs = Directory.GetDirectories(Path);   
                
                //faili
                string[] fileS = Directory.GetFiles(Path);

               // FolderCount = dirs.Length + fileS.Length;

                for (int i = 0; i < dirs.Length; i++)
                    table.Rows.Add(dirs[i]);

                for (int i = 0; i < fileS.Length; i++)
                    table.Rows.Add(fileS[i]);
            }

                //files.Add(new File(dirs[i], 0, 0));
                
        }     

        private void TextBox_Path(object sender, EventArgs e)
        {
            textBox1.Clear();
            Path = textBox1.Text;         
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
        }

        private void StepBack_Click(object sender, EventArgs e)
        {
            int s = Path.Where(c => c == '\\').Count();
            int indexOfSubstring = 0;

            if (s > 1)
            {
                indexOfSubstring = Path.LastIndexOf('\\');
            }

            if (s == 1)
            {
                indexOfSubstring = Path.LastIndexOf('\\') + 1;
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //
        }

        private void TableClick(object sender, DataGridViewCellEventArgs e)
        {
            //
        }       
    }
}
