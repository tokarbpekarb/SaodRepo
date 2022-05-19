using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountWords
{
    class Program
    {

        static void nataliaTest()
        {
            var loader = new Loader();
            var words = loader.Load("../../../big.txt");
            Console.WriteLine("Всего слов: " + words.Count);

            // Всего слов: 10611980
            // Уникальных: 525090
            // the 564373

            System.Diagnostics.Stopwatch watch;
            List<long> times = new List<long>();

            long elapsedMs;

            for (int i = 0; i < 5; i++)
            {
                watch = System.Diagnostics.Stopwatch.StartNew();


                var dict = new Dictionary<string, int>();
                //var dict = new Dictionary<string, int>();
                //var dict = new ВашКонтейнер<string, int>();
                foreach (var word in words)
                {
                    if (dict.ContainsKey(word)) // log(n)
                        ++dict[word];      // log(n)
                    else
                        dict.Add(word, 1);
                }
                Console.WriteLine("Уникальных: " + dict.Count);

                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                times.Add(elapsedMs);


            }
            var mediana = new mediana();

            var result = mediana.calc_mean_median_stddev(times);

            Console.WriteLine(result);


        }

        static void Main(string[] args)
        {
            nataliaTest();
        }




    }
}