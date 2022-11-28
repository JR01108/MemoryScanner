using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryScanner
{
   public class Node
    {
        Node _parent;
        List<Node> _children;
        string _name;
        double _weightKB;
        bool _isCatalog;

        public List<Node> Children { get; set; }

        public string Name { get; set; }

        public double WeightKB { get; set; }

        public bool IsCatalog { get; set; }

        public Node Parent { get; set; }

        public Node(Node parent, string name, double weightKB, bool isCatalog)
        {
            this.Parent = parent;
            Children= new List<Node>();
            this.Name = name;
            this.WeightKB = weightKB;
            this.IsCatalog = isCatalog;
        }

        public Node (string name, double weightKB, bool isCatalog)
        {
            Children = new List<Node>();
            this.Name = name;
            this.WeightKB = weightKB;
            this.IsCatalog = isCatalog;
        }

        public Node() { }

        public void Add(Node node)
        {
            Children.Add(node);
        }
    }


    public class FileTree
    {
        Node start;

        public Node Start { get; set; }

        public FileTree(Node start)
        {
            this.start = start;
        }


        public void FindNode(string name, Node now, ref Node result)
        {
            if (now.Name == name)
            {
                result = now;
                return;
            }
            else
            {
                foreach(Node node in now.Children)
                {
                    FindNode(name, node, ref result);
                }
            }
        }

        public void AddLayer(List<Node> nodes, string nowName)
        {
            Node now = new Node();
            FindNode(nowName, Start, ref now);
            now.Children = nodes;
        }

        public List<Node> GetNodes(string name)
        {
            Node now = new Node();
            FindNode(name, this.start, ref now);
            return now.Children;
        }

        public List<Node> GetNode() 
        {
            return Start.Children;
        }
    }
}