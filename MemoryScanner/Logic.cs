using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;

namespace MemoryScanner
{
    class Logic
    {
        public static FileTree ListOfSubfolder(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            Node startDireectory = new Node(path, SumFolder(path), true);
            FileTree fileTree = new FileTree(startDireectory);
            DirectoryInfo[] dirs = dir.GetDirectories();
            FileInfo[] files = dir.GetFiles();
            List<Node> Children = new List<Node>();
            for (int i = 0; i < dirs.Length; i++)
            {
                Children.Add(new Node(dirs[i].Name, SumFolder(dirs[i].FullName), IsCatalogOrFile(dirs[i].FullName)));
            }
            for (int i = 0; i < files.Length; i++)
            {
                Children.Add(new Node(files[i].Name, SumFolder(files[i].FullName), IsCatalogOrFile(files[i].FullName)));
            }
            fileTree.AddLayer(Children, path);
            return fileTree;
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
            return ((attr & FileAttributes.Directory) == FileAttributes.Directory);
        }
    }
}
