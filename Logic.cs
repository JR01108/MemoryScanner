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
        // Делает лист с данными про папку, ее подпапки и файлы
        public static void ListOfSubfolder(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] dirs = dir.GetDirectories();
            FileInfo[] files = dir.GetFiles();
            Node[] nodes = new Node[dirs.Length];
            Node[] nodesFiles = new Node[files.Length];
            for (int i = 0; i < dirs.Length; i++)
            {
                nodes[i] = new Node(dirs[i].Name, SumFolder(dirs[i].FullName), IsCatalogOrFile(dirs[i].FullName));
            }
            for (int i = 0; i < files.Length; i++)
            {
                nodesFiles[i] = new Node(files[i].Name, SumFolder(files[i].FullName), IsCatalogOrFile(files[i].FullName));
            }
            var result = nodes.Union(nodesFiles);
            List<Node> Children = new List<Node>();
            Children.AddRange(result);

        }
        // Считает размер папки, подпапок и файлов
        public static double SumFolder(string path)
        {
            if (IsCatalogOrFile(path))
            {
                string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                double sum = 0;
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo fi = new FileInfo(files[i]);
                    sum += fi.Length;
                }
                double kiloByte;
                kiloByte = sum / 1024;
                return kiloByte;
            }
            else
            {
                FileInfo fileInfo = new FileInfo(path);
                double kiloByte = fileInfo.Length / 1024;
                return kiloByte;
            }
        }
        // Проверяет папка выбранный объект или файл
        public static bool IsCatalogOrFile(string path)
        {
            FileAttributes attr = File.GetAttributes(path);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
