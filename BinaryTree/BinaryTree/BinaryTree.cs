using System;
using System.Collections.Generic;
namespace BinaryTree
{
    public interface IReturnKey
    {
        int ReturnKey();
    }
    
    public class BinaryTree<T> where T : IReturnKey
    {
        public Node<T>? Root;
        
        public void Insert(T item, Node<T>? root)
        {
            Node<T> newNode = new Node<T>(item, item.ReturnKey());
            MoveItemToPositon(Root, newNode);
            
            
        }
        public void MoveItemToPositon(Node<T>? root, Node<T> addedNode)
        {
            if (root == null)
            {
                Root = addedNode;
                return;
            }

            if (addedNode.Key > root.Key)
            {
                if (root.RightChild == null)
                {
                    root.RightChild = addedNode;
                }
                else
                {
                    MoveItemToPositon(root.RightChild, addedNode);
                }
            }
            else if (addedNode.Key < root.Key)
            {
                if (root.LeftChild == null)
                {
                    root.LeftChild = addedNode;
                }
                else
                {
                    MoveItemToPositon(root.LeftChild, addedNode);
                }
            }
            else
            {
                root.Content.Add(addedNode.Content[0]);
            }
        }

        public void Show(Node<T>? root)
        {
            if (root == null)
            {
                return;
            }
            Show(root.LeftChild);
            Console.WriteLine(root);
            Show(root.RightChild);
            
        }

        public Node<T>? Find(int key, Node<T>? root)
        {
            if (root == null)
            {
                return null;
            }
            if (root.Key == key)
            {
                return root;
            }
            if (root.Key < key)
            {
                return Find(key, root.RightChild);
            }
            if (root.Key > key)
            {
                return Find(key, root.LeftChild);
            }
            return null;
        }

        public Node<T> FindMin(Node<T>? root)
        {
            if (root == null)
            {
                return null;
            }

            if (root.LeftChild == null)
            {
                return root;
            }

            return FindMin(root.LeftChild);
        }

        public Node<T>? Delete(int key, Node<T>? root)
        {
            if (root == null)
            {
                return null;
            }

            if (key < root.Key)
            {
                root.LeftChild = Delete(key, root.LeftChild);
            }
            else if (key > root.Key)
            {
                root.RightChild = Delete(key, root.RightChild);
            }
            else
            {
                if (root.LeftChild == null)
                {
                    return root.RightChild;
                }
                if (root.RightChild == null)
                {
                    return root.LeftChild;
                }

                Node<T>? s = FindMin(root.RightChild);
                root.Key = s.Key;
                root.RightChild = Delete(s.Key, root.RightChild);
                return root;
            }
            return root;
        }
    }
}

