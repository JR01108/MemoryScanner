using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryScanner
{
    internal class Communicate
    {
        public List<MemoryScanner.File> GetFiles(string startPath)
        {
            List<MemoryScanner.File> files = new List<MemoryScanner.File>();
            FileTree fileTree = Logic.ListOfSubfolder(startPath);
            foreach(Node node in fileTree.Start.Children)
            {
                files.Add(new MemoryScanner.File(node.Name, node.WeightKB, node.WeightKB / fileTree.Start.WeightKB * 100, node.IsCatalog));
            }
            return files;
        }

        public void DoNothinfJustForTest()
        {

        }
    }
}
