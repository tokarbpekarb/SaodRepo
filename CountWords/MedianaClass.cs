using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountWords
{
    class MedianaClass
    {
        public static Tuple<double, double, double> calc_mean_median_stddev(List<long> times)
        {
            double mean = 0, median = 0, stddev = 0;

            double sum = 0;
            for (int i = 0; i < times.Count; i++)
            {
                sum += Convert.ToDouble(times[i]);
            }

            mean = sum / times.Count;

            times.Sort();
            median = times.Count % 2 != 0 ? Convert.ToDouble((times[times.Count / 2] + times[times.Count / 2 + 1]) / 2) : Convert.ToDouble((times[times.Count] / 2) / 2);

            for (int i = 0; i < times.Count; i++)
            {
                stddev += Convert.ToDouble(Math.Pow(times[i] - mean, 2));
            }

            stddev /= times.Count - 1;
            stddev = Math.Sqrt(stddev);


            return new Tuple<double, double, double>(mean, median, stddev);
        }
    }
}
