using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var dayNames = InitMonthDays();
            var sumAtDay = new double[31];

            foreach(var str in names)
            {
                sumAtDay[str.BirthDate.Day - 1] += (str.Name == name) ? 1 : 0;
            }

            sumAtDay[0] = 0;

            return new HistogramData(
                string.Format("Рождаемость людей с именем '{0}'", name), dayNames, sumAtDay);
        }

        static string[] InitMonthDays()
        {
            var dayNames = new string[31];

            for (var i = 0; i < 31; i++)
            {
                dayNames[i] = Convert.ToString(i + 1);
            }

            return dayNames;
        }
    }
}