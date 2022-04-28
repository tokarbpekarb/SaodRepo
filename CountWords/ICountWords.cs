using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountWords
{
    interface ICountWords
    {
        int Count { get; }
        bool ContainsKey(string key);
        void Add(string key, int value);
        int this[string key] { get; set; }
    }
}
