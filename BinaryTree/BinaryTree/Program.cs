using System;
using System.Collections.Generic;
namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<Person> bst = new BinaryTree<Person>();
            
            bst.Insert(new Person(30, "B"), bst.Root);
            bst.Insert(new Person(10, "A"), bst.Root);
            bst.Insert(new Person(50, "C"), bst.Root);
            bst.Insert(new Person(40, "D"), bst.Root);
            bst.Insert(new Person(20, "E"), bst.Root);
            bst.Insert(new Person(60, "F"), bst.Root);
            bst.Insert(new Person(60, "G"), bst.Root);


            int searchKey = 30;
            int delKey = 40;
            
            Console.WriteLine("vypise cely strom:");
            bst.Show(bst.Root);
            Console.WriteLine();
            Console.WriteLine($"najde element s klicem {searchKey}:");
            Console.WriteLine(bst.Find(searchKey, bst.Root));
            Console.WriteLine();
            Console.WriteLine("najde element s nejmensim klicem:");
            Console.WriteLine(bst.FindMin(bst.Root));
            Console.WriteLine();
            Console.WriteLine($"smaze clen s klicem {delKey}:");
            bst.Delete(delKey, bst.Root);
            Console.WriteLine();
            Console.WriteLine("znovu vypise strom:");
            bst.Show(bst.Root);
        }
    }
}

