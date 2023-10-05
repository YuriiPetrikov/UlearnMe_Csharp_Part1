// Вставьте сюда финальное содержимое файла PluralizeTask.cs
namespace Pluralize
{
	public static class PluralizeTask
	{
		public static string PluralizeRubles(int count)
		{
			// Напишите функцию склонения слова "рублей" в зависимости от предшествующего числительного count.
			if (count > 100)
				count %= 100;
			if (count == 1)
				return "рубль";
			if (count >= 2 && count <=4)
				return "рубля";
			if (count >= 5 && count <= 20)
				return "рублей";
			if(count%10 == 1)
				return "рубль";
			if (count%10 >= 2 && count%10 <= 4)
				return "рубля";

			return "рублей";
		}
	}
}
