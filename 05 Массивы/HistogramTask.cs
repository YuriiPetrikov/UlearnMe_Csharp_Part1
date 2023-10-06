// Вставьте сюда финальное содержимое файла HistogramTask.cs
using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            int dayCount = 31;
            var days = new string[dayCount];
            for (int i = 0; i < 31; i++)
                days[i] = (i + 1).ToString();
           
            var birthsCounts = new double[dayCount];
            foreach (var name1 in names)
            {
                if(name.Equals(name1.ToString().Substring(14)))
                    birthsCounts[name1.BirthDate.Day - 1]++;
            }
            birthsCounts[0] = 0;

            return new HistogramData(
               string.Format("Рождаемость людей с именем '{0}'", name),
               days, birthsCounts);
        }
    }
}
