// Вставьте сюда финальное содержимое файла DragonFractalTask.cs
using System;
using System.Drawing;

namespace Fractals
{
	internal static class DragonFractalTask
	{
		public static double[] Convert1(double x, double y)
		{
			var x1 = (x * Math.Cos(45 * Math.PI / 180) - y * Math.Sin(45 * Math.PI / 180)) / Math.Sqrt(2.0);
			var y1 = (x * Math.Sin(45 * Math.PI / 180) + y * Math.Cos(45 * Math.PI / 180)) / Math.Sqrt(2.0);
			return new[] {x1, y1};
		}

		public static double[] Convert2(double x, double y)
		{
			var x1 = (x * Math.Cos(135 * Math.PI / 180) - y * Math.Sin(135 * Math.PI / 180)) / Math.Sqrt(2.0) + 1;
			var y1 = (x * Math.Sin(135 * Math.PI / 180) + y * Math.Cos(135 * Math.PI / 180)) / Math.Sqrt(2.0);
			return new[] { x1, y1 };
		}
		
		public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
		{
			double x, y;
			x = 1.0;
			y = 0.0;
			var random = new Random(seed);
			pixels.SetPixel(x, y);

			for (int i = 0; i < iterationsCount; i++)
			{
				var nextNumber = random.Next(2);
				if (nextNumber == 0)
				{
					var result = Convert1(x, y);
					x = result[0];
					y = result[1];
				}
                else 
				{
					var result = Convert2(x, y);
					x = result[0];
					y = result[1];
				}
				pixels.SetPixel(x, y);
			}
		}
	}
}
