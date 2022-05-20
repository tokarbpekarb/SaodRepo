using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountWords
{
    public class Dict<TKey,TValue>
    {
        private int[] buckets;
        //private Entry[] entries;
        private IEqualityComparer<TKey> comparer;
        private int count;
        private int freeList;
        private int freeCount;
        public Dict()
        {

        }
    }
}
