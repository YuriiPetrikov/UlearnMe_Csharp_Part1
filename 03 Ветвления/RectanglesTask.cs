// Вставьте сюда финальное содержимое файла RectanglesTask.cs
using System;

namespace Rectangles
{
	public static class RectanglesTask
	{
		public static bool AreIntersected(Rectangle r1, Rectangle r2)
		{
			return !(r2.Left > (r1.Left + r1.Width) || r1.Left > (r2.Left + r2.Width)
			     || r2.Top  > (r1.Top + r1.Height)   || r1.Top  >(r2.Top + r2.Height));
		}

		public static int IntersectionSquare(Rectangle r1, Rectangle r2)
		{
			int left = Math.Max(r1.Left, r2.Left);
			int bottom = Math.Max(r1.Top, r2.Top);
			int top = Math.Min(r1.Left + r1.Width, r2.Left + r2.Width);
			int right = Math.Min(r1.Top + r1.Height, r2.Top + r2.Height);

			int width = top - left;
			int height = right - bottom;

			if (width < 0 || height < 0)
				return 0;
			
			return width * height;
		}

		public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
		{
			if (   r1.Left >= r2.Left && r1.Left + r1.Width <= r2.Left + r2.Width
				&& r1.Top >= r2.Top && r1.Top + r1.Height <= r2.Top + r2.Height
			    )
				return 0;

			if (r2.Left >= r1.Left && r2.Left + r2.Width <= r1.Left + r1.Width
				&& r2.Top >= r1.Top && r2.Top + r2.Height <= r1.Top + r1.Height
				)
				return 1;
			
			return -1;
		}
	}
}
