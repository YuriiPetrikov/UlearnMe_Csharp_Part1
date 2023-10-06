// Вставьте сюда финальное содержимое файла SegmentExtensions.cs
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryTasks;

namespace GeometryPainting
{
	public static class SegmentExtensions
	{
		public static Dictionary<Segment, Color> DictionarySeg = new Dictionary<Segment, Color>();
		public static void SetColor(this Segment segment, Color color)
		{
			if (DictionarySeg.ContainsKey(segment))
				DictionarySeg[segment] = color;
			else
				DictionarySeg.Add(segment , color);
		}

		public static Color GetColor(this Segment segment)
		{
			if (DictionarySeg.ContainsKey(segment))
				return DictionarySeg[segment];
			else
				return Color.Black;
		}
	}
}
