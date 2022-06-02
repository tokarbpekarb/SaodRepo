using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountWords
{
    class CountWordsDict : ICountWords
    {
        private struct Entry
        {
            // Lower 31 bits of hash code, -1 if unused
            public int hashCode;
            public int next;        // Index of next entry, -1 if last
            public string key;           // Key of entry
            public int value;         // Value of entry
        }

        private int[] buckets;
        private Entry[] entries;
        //private IEqualityComparer<TKey> comparer;
        private int count;
        private int freeList;
        private int freeCount;

        public int Count { get => count; }
        public CountWordsDict(int capacity)
        {
            //comparer = EqualityComparer<TKey>.Default;

            int size = capacity;
            buckets = new int[size];
            for (int i = 0; i < buckets.Length; i++)
                buckets[i] = -1;
            entries = new Entry[size];
            freeList = -1;
        }

        private void Resize()
        {
            Resize(count * 2);
        }
        private void Resize(int newSize)
        {

            int[] newBuckets = new int[newSize];
            for (int i = 0; i < newBuckets.Length; i++)
                newBuckets[i] = -1;
            Entry[] newEntries = new Entry[newSize];
            Array.Copy(entries, 0, newEntries, 0, count);

            for (int i = 0; i < count; i++)
            {
                if (newEntries[i].hashCode >= 0)
                {
                    int bucket =
                    newEntries[i].hashCode % newSize;
                    newEntries[i].next = newBuckets[bucket];
                    newBuckets[bucket] = i;
                }
            }
            buckets = newBuckets;
            entries = newEntries;
        }

        public void Add(string key, int value)
        {
            int hashCode = key.GetHashCode() & 0x7FFFFFFF;
            //int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
            int targetBucket = hashCode % buckets.Length;
            for (int i = buckets[targetBucket]; i >= 0;
            i = entries[i].next)
            {
                if (entries[i].hashCode == hashCode &&
                string.Equals(entries[i].key, key))
                {

                    entries[i].value = value;
                    return;
                }
            }

            int index;
            if (freeCount > 0)
            {
                index = freeList;
                freeList = entries[index].next;
                freeCount--;
            }
            else
            {
                if (count == entries.Length)
                {
                    Resize();
                    targetBucket = hashCode % buckets.Length;
                }
                index = count;
                count++;
            }

            entries[index].hashCode = hashCode;
            entries[index].next = buckets[targetBucket];
            entries[index].key = key;
            entries[index].value = value;
            buckets[targetBucket] = index;
        }

        public int FindEntry(string key)
        {
            if (buckets != null)
            {
                int hashCode = key.GetHashCode() & 0x7FFFFFFF;
                //int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
                for (int i =
                buckets[hashCode % buckets.Length];
                i >= 0; i = entries[i].next)
                {
                    if (entries[i].hashCode == hashCode
                    && string.Equals(entries[i].key, key))
                        return i;
                }
            }
            return -1;
        }

        public bool ContainsKey(string key)
        {
            return FindEntry(key) >= 0;
        }

        public int this [string key]
        {
            get
            {
                return entries[FindEntry(key)].value;
            }
            set
            {
                entries[FindEntry(key)].value = value;
            }
        }
    }
}
