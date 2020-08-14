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
                // Перебор с числом действий в строке всегда плохо, ибо надо вдумываться чо тут вообше происходит
                // то шо ты крут лучше покажет переменная с промежуточным результатом --J
                sumAtDay[str.BirthDate.Day - 1] += (str.Name == name) ? 1 : 0;
            }

            sumAtDay[0] = 0;

            // длинное рождение title тоже можно сделать отдельной инструкцией --J             
            return new HistogramData(
                string.Format("Рождаемость людей с именем '{0}'", name), dayNames, sumAtDay);
            
            // кстати, для Format есть следующий удобный синтаксис: 
            var title = $"Рождаемость людей с именем {name}";
            return new HistogramData(title, dayNames, sumAtDay);
        }

        static string[] InitMonthDays()
        {
            var dayNames = new string[31];
            
            for (var i = 0; i < 31; i++)
            {
                dayNames[i] = Convert.ToString(i + 1);
                // Еще можно так:
                // dayNames[i] = (i + 1).ToString(); 
                // --J
            }

            return dayNames;
        }
    }
}