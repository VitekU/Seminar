using System;
using System.Collections.Generic;
namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<Person> bst = new BinaryTree<Person>();
            
            bst.Insert(new Person(20, "E"), 20);
            bst.Insert(new Person(60, "F"), 60);
            bst.Insert(new Person(60, "G"), 60);
            bst.Insert(new Person(50, "C"), 50);
            bst.Insert(new Person(10, "A"), 10);
            bst.Insert(new Person(40, "D"), 40);
            bst.Insert(new Person(30, "B"), 30);
            
            
            int searchKey = 30;
            int delKey = 40;
            
            Console.WriteLine("vypise cely strom:");
            bst.Show();
            Console.WriteLine();
            
            Console.WriteLine($"najde element s klicem {searchKey}:");
            Console.WriteLine(bst.Find(searchKey) + "\n");
            
            Console.WriteLine("najde element s nejmensim klicem:");
            Console.WriteLine(bst.FindMin() + "\n");
            
            Console.WriteLine($"smaze clen s klicem {delKey}:");
            bst.Delete(delKey);
            Console.WriteLine();
            Console.WriteLine("znovu vypise strom:");
            bst.Show();
            Console.WriteLine();
            bst.TreeToAvl();
            bst.Show();
            
        }
    }
}

