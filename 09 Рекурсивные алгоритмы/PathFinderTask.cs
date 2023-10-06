using System;
using System.Collections.Generic;
using System.Drawing;

namespace RoutePlanning
{
	public static class PathFinderTask
	{
		public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
		{
			List<int[]> pricesAll = new List<int[]>();
			MakePermutations(new int[checkpoints.Length], 1, pricesAll);

			double length = PointExtensions.GetPathLength(checkpoints, pricesAll[0]);

			int j = 0;
			for (int i = 1; i < pricesAll.Count; i++)
			{
				if (PointExtensions.GetPathLength(checkpoints, pricesAll[i]) <= length)
				{
					length = PointExtensions.GetPathLength(checkpoints, pricesAll[i]);
					j = i;
				}
			}

			return pricesAll[j];
		}

		private static int[] MakeTrivialPermutation(int size)
		{
			var bestOrder = new int[size];
			for (int i = 0; i < bestOrder.Length; i++)
				bestOrder[i] = i;
			return bestOrder;
		}

		static void MakePermutations(int[] permutation, int position, List<int[]> pricesAll)
		{
			if (position == permutation.Length)
			{
				Evaluate(permutation, pricesAll);
				return;
			}

			for (int i = 0; i < permutation.Length; i++)
			{
				var index = Array.IndexOf(permutation, i, 0, position);
				if (index != -1)
					continue;
				permutation[position] = i;
				MakePermutations(permutation, position + 1, pricesAll);
			}
		}

		static void Evaluate(int[] permutation, List<int[]> pricesAll)
		{
			var mas = new int[permutation.Length];
			for (int i = 0; i < permutation.Length; i++)
				mas[i] = permutation[i];

			pricesAll.Add(mas);
		}
	}
}
