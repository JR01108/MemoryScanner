using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

            if (Directory.Exists(Path))
            {
                //podkatologi

                string[] dirs = Directory.GetDirectories(Path);
                foreach (string s1 in dirs)
                {
                    string SpisokD = s1;
                }

                //faili
                string[] files = Directory.GetFiles(Path);
                foreach (string s2 in files)
                {
                    string SpisokF = s2;
                }
            }

            int FileCount = 3; //Kollichestvo papok, failov 

            for (int i = 0; i < FileCount; i++)
            {
                table.Rows.Add(files[i].name, files[i].size, files[i].percent);
            }

            Table1.DataSource = table;
        }

        private void TableClick(object sender, DataGridViewCellEventArgs e)
        {
            //
        }

        private void TextBox_Path(object sender, EventArgs e)
        {
            textBox1.Text = Path;
            // если папка существует           
        }

        private string Path; // тут будем хранить путь к папке

        private void ChoiceOfPath(object sender, EventArgs e)
        {
            // показать диалог выбора папки
            DialogResult result = folderBrowserDialog1.ShowDialog();

            // если папка выбрана и нажата клавиша `OK` - значит можно получить путь к папке
            if (result == DialogResult.OK)
            {
                // запишем в нашу переменную путь к папке
                Path = folderBrowserDialog1.SelectedPath;
                
            }
        }
    }
}
