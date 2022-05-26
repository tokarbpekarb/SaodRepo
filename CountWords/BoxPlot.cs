using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountWords
{
    class BoxPlot
    {
        public static Tuple<double, double,double, double> MakeCalculations(List<long> times)
        {
            double mean = 0, median = 0, lq, stddev = 0;

            double sum = 0;
            for (int i = 0; i < times.Count; i++)
            {
                sum += Convert.ToDouble(times[i]);
            }

            mean = sum / times.Count;

            times.Sort();
            median = times.Count % 2 == 0 ? Convert.ToDouble((times[times.Count / 2] + times[times.Count / 2 + 1]) / 2) : Convert.ToDouble(times[times.Count / 2]);
            lq = GetLQ(times);
            for (int i = 0; i < times.Count; i++)
            {
                stddev += Convert.ToDouble(Math.Pow(times[i] - mean, 2));
            }

            stddev /= times.Count - 1;
            stddev = Math.Sqrt(stddev);


            return new Tuple<double, double, double,double>(mean, median, lq, stddev);
        }

        private static double GetLQ(List<long> times)
        {
            times.Sort();
            double index = (Convert.ToDouble(times.Count) + 1.0) / 4.0;
            int c = Convert.ToInt32(index);
            return index % 1.0 == 0 ? Convert.ToDouble(times[c]) : Convert.ToDouble((times[c] + times[c + 1]) / 2);
        }
    }
}
