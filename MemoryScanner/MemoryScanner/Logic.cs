using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryScanner
{
    class Logic
    {
        //считает вес выбранной папки
        public static void SumFolder(string path)
        {
            string[] files = Directory.GetFiles(path , "*.*", SearchOption.AllDirectories);
            int count = 0;
            double sum = 0;
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fi = new FileInfo(files[i]);
                count++;
                sum += fi.Length;
            }
            double kiloByte;
            kiloByte = sum / (1024);
            var Text = kiloByte.ToString("0.000") + " Кбайтов";
            MessageBox.Show(kiloByte + " Kbyte");
        }
    }
}
