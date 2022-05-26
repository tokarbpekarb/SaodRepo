using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountWords
{
    public class Dict<TKey, TValue>
    {
        private struct Entry
        {
            // Lower 31 bits of hash code, -1 if unused
            public int hashCode;
            public int next;        // Index of next entry, -1 if last
            public TKey key;           // Key of entry
            public TValue value;         // Value of entry
        }

        private int[] buckets;
        private Entry[] entries;
        private IEqualityComparer<TKey> comparer;
        private int count;
        private int freeList;
        private int freeCount;

        public Dict(int capacity)
        {
            comparer = EqualityComparer<TKey>.Default;

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
        public void Add(TKey key, TValue value)
        {
            int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
            int targetBucket = hashCode % buckets.Length;
            for (int i = buckets[targetBucket]; i >= 0;
            i = entries[i].next)
            {
                if (entries[i].hashCode == hashCode &&
                comparer.Equals(entries[i].key, key))
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

        public void Print()
        {
            Console.WriteLine("Index\tBuckets\t\tEntries\n");
            for (int i = 0; i < buckets.Length; i++)
            {
                Console.Write(i + "\t\t" + buckets[i] + "\t\tKey: "
                + entries[i].key + ", Hash: " + entries[i].hashCode + " ");
                Console.WriteLine(entries[i].next);
            }
        }

    }
}
