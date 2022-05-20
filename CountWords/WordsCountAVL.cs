using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountWords
{
    class TreeNodeAVL
    {
        public KeyValuePair<string, int> item;
        public TreeNodeAVL left, right;
        public int height;

        public TreeNodeAVL(string key, int value)
        {
            item = new KeyValuePair<string, int>(key, value);
        }
    }
    class WordsCountAVL : ICountWords
    {
        TreeNodeAVL root;
        int size;

        public int Count
        {
            get =>  size;
        }
        
        public TreeNodeAVL Find(string key)
        {
            TreeNodeAVL current = root;
            while (current != null)
            {
                if (current.item.Key == key)
                    return current;
                if (String.Compare(key, current.item.Key) < 0)
                    current = current.left;
                else
                    current = current.right;
            }
            throw new Exception("haha");
        }

        public int this[string key]
        {
            get
            {
                return Find(key).item.Value;
            }
            set
            {
                TreeNodeAVL find = Find(key);
                //string k = find.item.Key;
                int val = value;
                find.item = new KeyValuePair<string, int>(key, val);
            }
        }
        public void Add(string key, int value)
        {
            root = _Add(key,value, root);
            size++;
        }


        private TreeNodeAVL _Add(string key, int value, TreeNodeAVL subroot)
        {
            if (subroot == null)
                return new TreeNodeAVL(key,value);
            if (String.Compare(key,subroot.item.Key) < 0)
                subroot.left = _Add(key, value, subroot.left);
            else
                subroot.right = _Add(key,value, subroot.right);
            UpdateHeight(subroot);
            int b = GetBalance(subroot);

            // повороты
            if (b > 1 && String.Compare(key, subroot.left.item.Key) < 0)
                    return _RightRotate(subroot);
            if (b < -1 && String.Compare(key, subroot.right.item.Key) > 0)
                return _LeftRotate(subroot);
            if (b > 1 && String.Compare(key, subroot.left.item.Key) > 0)
            {
                subroot.left = _LeftRotate(subroot.left);
                return _RightRotate(subroot);
            }
            if (b < -1 && String.Compare(key, subroot.right.item.Key) < 0)
            {
                subroot.right = _RightRotate(subroot.right);
                return _LeftRotate(subroot);
            }

            return subroot;
        }

        private int GetHeight(TreeNodeAVL node)
        {
            return node == null ? 0 : node.height;
        }

        private int GetBalance(TreeNodeAVL node)
        {
            return node == null ? 0 : GetHeight(node.left) - GetHeight(node.right);
        }

        private void UpdateHeight(TreeNodeAVL node)
        {
            node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));
        }

        private TreeNodeAVL _RightRotate(TreeNodeAVL subroot)
        {
            TreeNodeAVL b = subroot.left;
            subroot.left = b.right;
            b.right = subroot;
            UpdateHeight(subroot);
            UpdateHeight(b);
            return b;
        }

        //малое левое вращение
        private TreeNodeAVL _LeftRotate(TreeNodeAVL subroot)
        {
            TreeNodeAVL b = subroot.right;
            subroot.right = b.left;
            b.left = subroot;
            UpdateHeight(subroot);
            UpdateHeight(b);
            return b;
        }

        public bool ContainsKey(string key)
        {
            TreeNodeAVL current = root;
            while(current != null)
            {
                if (key == current.item.Key)
                    return true;
                if (String.Compare(key, current.item.Key) < 0)
                    current = current.left;
                else
                    current = current.right;
            }
            return false;
        }

        private KeyValuePair<int, string> ToStringHelper(TreeNodeAVL n)
        {
            if (n == null)
                return new KeyValuePair<int, string>(1, "\n");

            var left = ToStringHelper(n.left);
            var right = ToStringHelper(n.right);

            var objString = n.item.ToString();
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
    }
}
