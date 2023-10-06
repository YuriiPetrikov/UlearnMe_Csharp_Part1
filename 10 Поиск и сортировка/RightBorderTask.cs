// Вставьте сюда финальное содержимое файла RightBorderTask.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class RightBorderTask
	{
		public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
		{ 
			if (phrases.Count == 0 && right == 0) return right++; 
			if (right == phrases.Count && 
(string.Compare(prefix, phrases[right - 1], StringComparison.OrdinalIgnoreCase) >= 0
                || phrases[right - 1].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))) return right; 
			if (right == phrases.Count && 
string.Compare(prefix, phrases[right - 1], StringComparison.OrdinalIgnoreCase) < 0) right--;
			if (right == 0 && 
string.Compare(prefix, phrases[right], StringComparison.OrdinalIgnoreCase) < 0) return right; 
            if (prefix.Equals("")) return phrases.Count; 
            while (left < right)
            {
                var m = (left + right) / 2;
                
                if (string.Compare(prefix, phrases[right], StringComparison.OrdinalIgnoreCase) >= 0
                        || phrases[right].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    return right + 1;
                if (string.Compare(prefix, phrases[m], StringComparison.OrdinalIgnoreCase) < 0
                    && !phrases[m].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    right = m - 1;
                else
                    left = m + 1;
            }

            if (right < 0) return 0;
            if (phrases[right].StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) return ++right;
            return left;
        }
    }
}
