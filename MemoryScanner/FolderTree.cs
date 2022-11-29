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

        public Node Parent { get; set; }

        public List<Node> Children { get; set; }

        public string Name { get; set; }

        public double WeightKB { get; set; }

        public bool IsCatalog { get; set; }

        //Создание объекта с известным родителем
        public Node(Node parent, string name, double weightKB, bool isCatalog)
        {
            this.Parent = parent;
            Children = new List<Node>();
            this.Name = name;
            this.WeightKB = weightKB;
            this.IsCatalog = isCatalog;
        }

        //Создание коренного объекта или объекта с неизвестным родителем
        public Node (string name, double weightKB, bool isCatalog)
        {
            Children = new List<Node>();
            this.Name = name;
            this.WeightKB = weightKB;
            this.IsCatalog = isCatalog;
        }

        public Node() { }


        //Добавление детей к объекту
        public void AddNodes(List<Node> nodes)
        {
            foreach (Node node in nodes) { node.Parent = this; }
            Children = nodes;
        }


        //Возвращение пути от коренного объекта до текущего
        public string ReturnPath()
        {
            string path = $"\\{this.Name}";
            return this.Parent?.ReturnPath() + path;
        }
    }

    public class FileTree
    {
        Node start;

        public Node Start { get; set; }

        //Создание дерева с единственным коренным объектом
        public FileTree(Node start)
        {
            this.start = start;
        }


        //Поиск объекта по имени путем обхода дерева. Изменяет isFinded на true при успешном поиске
        public void FindNode(string name, Node now, ref Node result, ref bool isFinded)
        {
            if (now.Name == name)
            {
                result = now;
                isFinded = true;
                return;
            }
            else
            {
                foreach(Node node in now.Children)
                {
                    FindNode(name, node, ref result, ref isFinded);
                }
            }
        }

        //Добавление потомков к элементу по имени
        public void AddLayer(List<Node> nodes, string nowName)
        {
            Node now = new Node();
            bool isFinded = false;
            FindNode(nowName, Start, ref now, ref isFinded);
            if (isFinded)
                now.AddNodes(nodes);
        }

        //Возврат потомков некорневого элемента (Ищем его по имени)
        public List<Node> GetNodes(string name)
        {
            Node now = new Node();
            bool isFinded = false;
            FindNode(name, this.start, ref now, ref isFinded);
            if (isFinded)
            return now.Children;
            else
                return null;
        }

        //Возврат потомков корневого элемента
        public List<Node> GetNode() 
        {
            return Start.Children;
        }
    }
}