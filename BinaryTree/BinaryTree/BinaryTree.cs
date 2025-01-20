using System;
using System.Collections.Generic;
namespace BinaryTree
{
    public class BinaryTree<T>
    {
        private Node<T>? _root;
        private List<Node<T>> _listNodes = new List<Node<T>>();
        private HashSet<int> _unikatniKlice = new HashSet<int>();
         
        public void Insert(T item, int value)
        {
            Node<T> newNode = new Node<T>(item, value);
            if (!_unikatniKlice.Contains(newNode.Key))
            {
                _unikatniKlice.Add(newNode.Key);
                _listNodes.Add(newNode);
            }
            MoveItemToPositon(_root, newNode);
        }
        
        private void MoveItemToPositon(Node<T>? root, Node<T> addedNode)
        {
            if (root == null)
            {
                _root = addedNode;
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

        // show funkce, private a public aby root mohl byt private 
        public void Show()
        {
            _show(_root);
        }
        private void _show(Node<T>? root)
        {
            if (root == null)
            {
                return;
            }
            _show(root.LeftChild);
            Console.WriteLine(root);
            _show(root.RightChild);
        }

        // encapsulation stejne jako u find
        public Node<T>? Find(int key)
        {
            return _find(key, _root);
        }
        private Node<T>? _find(int key, Node<T>? root)
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
                return _find(key, root.RightChild);
            }
            if (root.Key > key)
            {
                return _find(key, root.LeftChild);
            }
            return null;
        }

        
        // encapsulation stejne jako u find
        public Node<T>? FindMin()
        {
            return _findMin(_root);
        }
        private Node<T> _findMin(Node<T>? root)
        {
            if (root == null)
            {
                return null;
            }

            if (root.LeftChild == null)
            {
                return root;
            }

            return _findMin(root.LeftChild);
        }

        // encapsulation stejne jako u find
        public void Delete(int key)
        {
            _delete(key, _root);
        }
        private Node<T>? _delete(int key, Node<T>? root)
        {
            if (root == null)
            {
                return null;
            }

            if (key < root.Key)
            {
                root.LeftChild = _delete(key, root.LeftChild);
            }
            else if (key > root.Key)
            {
                root.RightChild = _delete(key, root.RightChild);
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

                Node<T>? s = _findMin(root.RightChild);
                root.Key = s.Key;
                root.RightChild = _delete(s.Key, root.RightChild);
                return root;
            }
            return root;
        }

        // encapsulation stejne jako u find
        public void TreeToAvl()
        {
            _listNodes = _listNodes.OrderBy(node => node.Key).ToList();
            _root = _treeToAvl(_listNodes, 0, _listNodes.Count - 1);
        }
        private Node<T>? _treeToAvl(List<Node<T>> list, int left, int right)
        {
            if (left > right)
            {
                return null;
            }

            int mid = (left + right) / 2;
            Node<T> root = list[mid];
            root.LeftChild = _treeToAvl(list, left, mid - 1);
            root.RightChild = _treeToAvl(list, mid + 1, right);
            return root;
        }
    }
}

