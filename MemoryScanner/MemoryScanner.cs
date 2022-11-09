using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
            //Запись в табличку!
           
            files.Add(new File("Фото", 46, 3));
            files.Add(new File("Игры", 456, 41));
            files.Add(new File("Документы", 19, 1));

            DataTable table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Size", typeof(string));
            table.Columns.Add("%", typeof(int));

            int FileCount = 3; //Kollichestvo papok, failov 

            for (int i = 0; i < FileCount; i++)
            {
                table.Rows.Add(files[i].name, files[i].size, files[i].percent);
            }

            Table1.DataSource = table;
        }

        private void TableClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TextBox_Path(object sender, EventArgs e)
        {
            String Path = ((TextBox)sender).Text;
        }

        private void ChoiceOfPath(object sender, EventArgs e)
        {

        }
    }
}
