using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountWords
{
    class PrefixTree : ICountWords
    {
        PrefixTreeNode root, current;
        int count;

        public int Count { get => count; }

        public PrefixTree()
        {
            root = new PrefixTreeNode(' ', 0);
        }
        public void Add(string key, int value)
        {
            current = root;
            bool found = false; ;
            foreach (char c in key)
            {
                found = false;
                foreach (PrefixTreeNode t in current.childs)
                {
                    if (c == t.data.Key)
                    {
                        current = t;
                        found = true;
                        int a = t.data.Value + 1;
                        t.data = new KeyValuePair<char, int>(c, a);
                        break;
                    }
                }
                if (!found)
                {
                    PrefixTreeNode node  = new PrefixTreeNode(c, 1);
                    current.childs.Add(node);
                    current = current.childs.Last();
                }
            }
            if(!found)
            {
                current.isWord = true;
                count++;
            }
        }

        public bool ContainsKey(string key)
        {
            current = root;
            bool found;
            foreach(char c in key)
            {
                found = false;
                foreach(PrefixTreeNode t in current.childs)
                {
                    if (c == t.data.Key)
                    {
                        current = t;
                        break;
                    }
                }
                if (!found)
                    return false;

            }
            if (current.isWord)
                return true;
            return false;
        }

        public PrefixTreeNode Find(string key)
        {
            current = root;
            bool found;
            foreach (char c in key)
            {
                found = false;
                foreach (PrefixTreeNode t in current.childs)
                {
                    if (c == t.data.Key)
                    {
                        current = t;
                        break;
                    }
                }
                if (!found)
                    throw new Exception();

            }
            if (current.isWord)
                return current;
            throw new Exception();
        }

        public int this[string key]
        {
            get
            {
                return Find(key).data.Value;
            }
            set
            {
                PrefixTreeNode find = Find(key);
                int val = value;
                find.data = new KeyValuePair<char, int>(key[0], val);
            }
        }
    }


}
