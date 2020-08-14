using System;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var heatTable = new double[30, 12];
            var dayNames = InitName(30, 2);
            var monthNames = InitName(12, 1);

            foreach (var name in names)
            {
                var day = name.BirthDate.Day - 2;
                var month = name.BirthDate.Month - 1;
                if (day >= 0)
                    heatTable[day, month] += 1;
            }

            return new HeatmapData(
                "Пример карты интенсивностей",
                heatTable,
                dayNames,
                monthNames);
        }

        static string[] InitName(int days, int defect)
        {
            var dayNames = new string[days];

            for (var i = 0; i < days; i++)
            {
                dayNames[i] = Convert.ToString(i + defect);
            }

            return dayNames;
        }
    }
}