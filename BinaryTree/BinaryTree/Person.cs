using System;
using System.Collections.Generic;
namespace BinaryTree
{
    public class Person : IReturnKey
    {
        public int Age;
        public string Name;

        public int ReturnKey()
        {
            return Age;
        }
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

