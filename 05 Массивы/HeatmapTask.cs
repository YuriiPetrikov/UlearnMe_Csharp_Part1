// Вставьте сюда финальное содержимое файла HeatmapTask.cs
using System;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var xLable = new string[30];
            for (int i = 0; i < xLable.Length; i++)
                xLable[i] = (i + 2).ToString();

            var yLable = new string[] { "1", "2","3","4","5","6","7","8","9","10","11","12"};
                       
            var heat = new double[30, 12];
            var xLength = heat.GetLength(0);
            var yLength = heat.GetLength(1);
            for (int i = 0; i < xLength; i++)
                for (int j = 0; j < yLength; j++)
                {
                    foreach (var name in names)
                        if (name.BirthDate.Day == (i + 2) & name.BirthDate.Month == (j + 1))
                            heat[i, j]++;
                }

            return new HeatmapData("Пример карты интенсивностей", heat, xLable, yLable);
        }
    }
}
