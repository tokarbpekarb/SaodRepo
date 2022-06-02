using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountWords
{
    class PrefixTreeNode
    {
        public List<PrefixTreeNode> childs;
        public bool isWord;
        public KeyValuePair<char, int> data;

        public PrefixTreeNode(char key, int value)
        {
            childs = new List<PrefixTreeNode>();
            data = new KeyValuePair<char, int>(key, value);
        }
    }
}
