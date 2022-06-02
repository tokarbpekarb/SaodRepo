using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace SAOD_8
{
    class Program
    {
        static void Main(string[] args)
        {
			var loader = new Loader();
			var words = loader.Load("big.txt");
			Console.WriteLine("Всего слов: " + words.Count);

			// Всего слов: 10611980
			// Уникальных: 525090
			// the 564373

			Stopwatch watch = new Stopwatch();
			List<long> times = new List<long>();

			times.Clear();
			for (int i = 0; i < 5; i++)
			{
				watch.Reset();
				watch.Start();
				SortedList<string, int> list = new SortedList<string, int>();
				foreach (var word in words)
				{
					if (list.ContainsKey(word))
						list[word]++;
					else
						list.Add(word, 1);
				}
				watch.Stop();
				times.Add(watch.ElapsedMilliseconds);
			}

			foreach(var time in times)
            {
				Console.Write(time + " ");
            }

            var dict = new SortedDictionary<string, int>();
            foreach (var word in words)
            {
                if (dict.ContainsKey(word)) // log(n)
                    ++dict[word];      // log(n)
                else
                    dict.Add(word, 1);
            }
            Console.WriteLine("Уникальных: " + dict.Count);

            string str = "the";
            if (dict.ContainsKey(str))
                Console.WriteLine(str + " " + dict[str]);
            else
                Console.WriteLine(str + " 0");
            Console.ReadKey();
		}
    }

	class Loader
	{
		public List<string> Load(string filename)
		{
			var words = new List<string>();
			string text = System.IO.File.ReadAllText(filename);
			string word = "";
			foreach (var ch in text)
			{
				if (ch >= 'a' && ch <= 'z' ||
							ch >= 'A' && ch <= 'Z' || ch == '\'')
					word += ch;
				else if (word.Length > 0)
				{
					words.Add(word);
					word = "";
				}
			}
			return words;
		}
	}
}
