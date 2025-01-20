using System;
using System.Collections.Generic;
namespace BinaryTree
{
    public class Node<T> 
    {
        public List<T> Content;
        public int Key;
        public Node<T>? LeftChild;
        public Node<T>? RightChild;
        public Node(T c, int k)
        {
            Content = new List<T> { c };
            Key = k;
        }

        public override string ToString()
        {
            string s = "";
            foreach (var p in Content)
            {
                s += "(" + p + ")" + " ";
            }
            return $"Klic: {Key}, Obsah: {s}";
        }
    }
}

