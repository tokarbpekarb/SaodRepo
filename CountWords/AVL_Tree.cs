using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountWords
{
    class AVLNode<T> where T:IComparable<T>
    {
        public T value;
        public int height;
        public AVLNode<T> left, right;

        public AVLNode(T item)
        {
            value = item;
        }
    }
    class AVL_Tree<T> where T:IComparable<T>
    {
        AVLNode<T> root;

        public void Add(T item)
        {
            root = _Add(item, root);
        }

        private AVLNode<T> _Add(T item, AVLNode<T> subroot)
        {
            if (subroot == null)
                return new AVLNode<T>(item);
            if (item.CompareTo(subroot.value) < 0)
                subroot.left = _Add(item, subroot.left);
            else
                subroot.right = _Add(item, subroot.right);
            return subroot;
        }
    }
}
