using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountWords
{
    class Program
    {
        static void Main(string[] args)
        {
            var loader = new Loader();
            var words = loader.Load("../../../big.txt");
            Console.WriteLine("Всего слов: " + words.Count);

            // Всего слов: 10611980
            // Уникальных: 525090
            // the 564373

            //префиксное дерево(неправильно считает)
            var tree = new PrefixTree();
            foreach (var word in words)
            {
                if (tree.ContainsKey(word)) // log(n)
                    ++tree[word];      // log(n)
                else
                    tree.Add(word, 1);
            }
            Console.WriteLine("Уникальных: " + tree.Count);

            //System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            //List<long> times = new List<long>();

            //long elapsedMs;

            ////реализация через SortedList (двоичный поиск) задание 8
            //Console.WriteLine("Реализация через SortedList");
            //times.Clear();
            //for (int i = 0; i < 5; i++)
            //{
            //    watch.Reset();
            //    watch.Start();
            //    SortedList<string, int> list = new SortedList<string, int>();
            //    foreach (var word in words)
            //    {
            //        if (list.ContainsKey(word))
            //            list[word]++;
            //        else
            //            list.Add(word, 1);
            //    }
            //    watch.Stop();
            //    times.Add(watch.ElapsedMilliseconds);
            //}
            //Console.WriteLine(BoxPlot.MakeCalculations(times));
            ////Console.WriteLine("Уникальных: " + list.Count);

            ////реализация через AVL дерево (задание 9)
            //Console.WriteLine("Реализация через AVL дерево");
            //times.Clear();
            //for (int i = 0; i < 5; i++)
            //{
            //    watch.Reset();
            //    watch.Start();
            //    WordsCountAVL tree = new WordsCountAVL();
            //    foreach (var word in words)
            //    {
            //        if (tree.ContainsKey(word))
            //            ++tree[word];
            //        else
            //            tree.Add(word, 1);
            //    }
            //    watch.Stop();
            //    times.Add(watch.ElapsedMilliseconds);
            //}
            //Console.WriteLine(BoxPlot.MakeCalculations(times));
            ////Console.WriteLine("УникальныхЖ " + tree.Count);

            ////реализация через хэш таблицу (задание 11)
            //Console.WriteLine("Реализация через Хэш-таблицу");
            //times.Clear();
            //for (int i = 0; i < 5; i++)
            //{
            //    watch.Reset();
            //    watch.Start();
            //    var dict = new CountWordsDict(10000);
            //    foreach (var word in words)
            //    {
            //        if (dict.ContainsKey(word)) // log(n)
            //            ++dict[word];      // log(n)
            //        else
            //            dict.Add(word, 1);
            //    }
            //    watch.Stop();
            //    times.Add(watch.ElapsedMilliseconds);
            //}
            //Console.WriteLine(BoxPlot.MakeCalculations(times));

            ///////////////////////////////////////////////////////////////////////////////////////////////

            //var dict = new SortedDictionary<string, int>();
            //var dict = new Dictionary<string, int>();
            //var dict = new ВашКонтейнер<string, int>();
            //foreach (var word in words)
            //{
            //    if (dict.ContainsKey(word)) // log(n)
            //        ++dict[word];      // log(n)
            //    else
            //        dict.Add(word, 1);
            //}
            //Console.WriteLine("Уникальных: " + dict.Count);

            //string str = "the";
            //if (dict.ContainsKey(str))
            //    Console.WriteLine(str + " " + dict[str]);
            //else
            //    Console.WriteLine(str + " 0");
            Console.ReadKey();
        }
    }
}
