using System.Collections.Generic;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
			int sizeX = original.GetLength(0);
			int sizeY = original.GetLength(1);
			double t;
			int amountWhitePixels = (int)(whitePixelsFraction * sizeX * sizeY);
			
			var outMas = new double[sizeX, sizeY];
			var list = SortList(original);
			t = (amountWhitePixels <= 0) ? 0 : list[amountWhitePixels - 1];

			for (int i = 0; i < sizeX; i++)
				for (int j = 0; j < sizeY; j++)
                {
					if (t == 0)
						outMas[i, j] = 0;
					else if (original[i, j] >= t)
						outMas[i, j] = 1;
					else
						outMas[i, j] = 0;
				}

			return outMas;
		}

		static List<double> SortList(double[,] original)
		{
			int sizeX = original.GetLength(0);
			int sizeY = original.GetLength(1);
			var list = new List<double>();
			
			for (int i = 0; i < sizeX; i++)
				for (int j = 0; j < sizeY; j++)
					list.Add(original[i, j]);

			list.Sort();
			list.Reverse();
			return list;
		}
	}
}
