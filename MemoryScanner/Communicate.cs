using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryScanner
{
    public interface ICommunicate
    {
        List<FileStruct> GetFiles(string startPath);
    }
    static public class Communicate
    {
        static public List<FileStruct> GetFiles(string startPath)
        {
            List<FileStruct> files = new List<FileStruct>();
            FileTree fileTree = Logic.ListOfSubfolder(startPath);
            foreach (Node node in fileTree.Start.Children)
            {
                files.Add(new FileStruct(node.Name, node.WeightKB, (node.WeightKB / fileTree.Start.WeightKB) * 100, node.IsCatalog));
            }
            return files;
        }
    }
}
