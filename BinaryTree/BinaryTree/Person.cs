using System;
using System.Collections.Generic;
namespace BinaryTree
{
    public class Person
    {
        public int Age;
        public string Name;
        
        public Person(int a, string n)
        {
            Age = a;
            Name = n;
        }

        public override string ToString()
        {
            return $"{Name}, {Age}";
        }
    }
}

