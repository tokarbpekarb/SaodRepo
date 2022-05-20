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
            //WordsCountAVL tree = new WordsCountAVL();
            //tree.Add("first", 1);
            //tree.Add("second", 1);
            //tree.Add("third", 1);
            //tree.Add("f", 1);
            //tree["second"]++;
            //Console.WriteLine(tree.Print());
            //Console.ReadKey();

            var loader = new Loader();
            var words = loader.Load("../../../big.txt");
            Console.WriteLine("Всего слов: " + words.Count);

            // Всего слов: 10611980
            // Уникальных: 525090
            // the 564373

            //реализация через AVL дерево
            WordsCountAVL tree = new WordsCountAVL();
            foreach (var word in words)
            {
                if (tree.ContainsKey(word))
                    ++tree[word];
                else
                    tree.Add(word, 1);
            }
            Console.WriteLine("УникальныхЖ " + tree.Count);


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
