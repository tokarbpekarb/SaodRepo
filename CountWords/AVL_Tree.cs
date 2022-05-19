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
            UpdateHeight(subroot);
            int b = GetBalance(subroot);

            // повороты
            if (b > 1 && item.CompareTo(subroot.left.value) < 0)
                return _RightRotate(subroot);
            if (b < -1 && item.CompareTo(subroot.right.value) > 0)
                return _LeftRotate(subroot);
            if (b > 1 && item.CompareTo(subroot.left.value) > 0)
            {
                subroot.left = _LeftRotate(subroot.left);
                return _RightRotate(subroot);
            }
            if (b < -1 && item.CompareTo(subroot.right.value) < 0)
            {
                subroot.right = _RightRotate(subroot.right);
                return _LeftRotate(subroot);
            }

            return subroot;
        }

        private int GetHeight(AVLNode<T> node)
        {
            return node == null ? 0 : node.height;
        }

        private int GetBalance(AVLNode<T> node)
        {
            return node == null ? 0 : GetHeight(node.left) - GetHeight(node.right);
        }

        private void UpdateHeight(AVLNode<T> node)
        {
            node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));
        }

        private AVLNode<T> _RightRotate(AVLNode<T> subroot)
        {
            AVLNode<T> b = subroot.left;
            subroot.left = b.right;
            b.right = subroot;
            UpdateHeight(subroot);
            UpdateHeight(b);
            return b;
        }

        //малое левое вращение
        private AVLNode<T> _LeftRotate(AVLNode<T> subroot)
        {
            AVLNode<T> b = subroot.right;
            subroot.right = b.left;
            b.left = subroot;
            UpdateHeight(subroot);
            UpdateHeight(b);
            return b;
        }

        public int GetDeep()
        {
            return _GetDeep(root);
        }

        private int _GetDeep(AVLNode<T> subroot)
        {
            if (subroot == null) return 0;
            return 1 + Math.Max(_GetDeep(subroot.left), _GetDeep(subroot.right));
        }

        private KeyValuePair<int, string> ToStringHelper(AVLNode<T> n)
        {
            if (n == null)
                return new KeyValuePair<int, string>(1, "\n");

            var left = ToStringHelper(n.left);
            var right = ToStringHelper(n.right);

            var objString = n.value.ToString();
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(' ', left.Key - 1);
            stringBuilder.Append(objString);
            stringBuilder.Append(' ', right.Key - 1);
            stringBuilder.Append('\n');

            var i = 0;
            while (i * left.Key < left.Value.Length && i * right.Key < right.Value.Length)
            {
                stringBuilder.Append(left.Value, i * left.Key, left.Key - 1);
                stringBuilder.Append(' ', objString.Length);
                stringBuilder.Append(right.Value, i * right.Key, right.Key);
                ++i;
            }

            while (i * left.Key < left.Value.Length)
            {
                stringBuilder.Append(left.Value, i * left.Key, left.Key - 1);
                stringBuilder.Append(' ', objString.Length + right.Key - 1);
                stringBuilder.Append('\n');

                ++i;
            }

            while (i * right.Key < right.Value.Length)
            {
                stringBuilder.Append(' ', left.Key + objString.Length - 1);
                stringBuilder.Append(right.Value, i * right.Key, right.Key);
                ++i;
            }
            return new KeyValuePair<int, string>(left.Key + objString.Length + right.Key - 1, stringBuilder.ToString());
        }
        public string Print()
        {
            var res = ToStringHelper(root).Value;
            return res;
        }

        public bool Contains(T item)
        {
            AVLNode<T> current = root;
            while (current != null)
            {
                if (current.value.CompareTo(item) == 0)
                    return true;
                if (current.value.CompareTo(item) > 0)
                    current = current.left;
                else
                    current = current.right;
            }
            return false;
        }

        public bool ContainsKey(T item)
        {
            AVLNode<T> current = root;
            while (current != null)
            {
                if (current.value.CompareTo(item) == 0)
                    return true;
                if (current.value.CompareTo(item) > 0)
                    current = current.left;
                else
                    current = current.right;
            }
            return false;
        }
    }
}
