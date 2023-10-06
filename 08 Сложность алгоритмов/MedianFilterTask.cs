using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		/* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */
		public static double[,] MedianFilter(double[,] original)
		{
            int sizeX = original.GetLength(0);
            int sizeY = original.GetLength(1);
            var list = new List<double>();
            var medianOutMas = new double[sizeX, sizeY];

            for (int posX = 0; posX < sizeX; posX++)
            {
                for (int posY = 0; posY < sizeY; posY++)
                {
                    list.Clear();

                    for (int i = posX - 1; i <= posX + 1; i++)
                        for (int j = posY - 1; j <= posY + 1; j++)
                        {
                            if (i >= 0 && j >= 0 && i < sizeX && j < sizeY)
                                list.Add(original[i, j]);
                        }

                    medianOutMas[posX, posY] = СalculationMedianValue(list);
                }
            }
                    
			return medianOutMas;
		}

        public static double СalculationMedianValue(List<double> list)
        {
            list.Sort();

            if (list.Count % 2 == 0)
                return (list[list.Count / 2] + list[list.Count / 2 - 1]) / 2;
            else
                return list[list.Count / 2];
        }
	}
}
