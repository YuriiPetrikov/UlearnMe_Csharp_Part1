// Вставьте сюда финальное содержимое файла DistanceTask.cs
using System;

namespace DistanceTask
{
	public static class DistanceTask
	{
		public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
		{
			var dx = bx - ax;
			var dy = by - ay;
			
			if (dx == 0 && dy == 0)
				return Math.Sqrt((x - ax) * (x - ax) + (y - ay) * (y - ay));
			
			var t = ((x - ax) * dx + (y - ay) * dy) / (dx * dx + dy * dy);
			
			if (t < 0)
			{
				dx = x - ax;
				dy = y - ay;
			}
			else if (t > 1)
			{
				dx = x - bx;
				dy = y - by;
			}
			else
			{
				dx = x - (ax + t * dx);
				dy = y - (ay + t * dy);
			}

			return Math.Sqrt(dx * dx + dy * dy);
		}
	}
}
