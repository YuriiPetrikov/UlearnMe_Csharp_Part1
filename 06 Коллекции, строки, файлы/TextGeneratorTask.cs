// Вставьте сюда финальное содержимое файла TextGeneratorTask.cs
using System.Collections.Generic;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(Dictionary<string, string> nextWords,
            string phraseBeginning, int wordsCount)
        {
            int length;
            for (int i = 0; i < wordsCount; i++)
            {
                string[] words = phraseBeginning.Split(' '); //разбитие строки на слова
                
                if ((length = phraseBeginning.Split(' ').Length) >= 2)
                {
                    if (nextWords.ContainsKey(words[length - 2] + " " + words[length - 1]))
                    { 
                        phraseBeginning += " " + nextWords[words[length - 2] + " " + words[length - 1]];
                    }
                    else if (nextWords.ContainsKey(words[length - 1]))
                    {
                        phraseBeginning += " " + nextWords[words[length - 1]];
                    }
                }
                else if (nextWords.ContainsKey(words[length - 1]))
                    {
                        phraseBeginning += " " + nextWords[words[length - 1]];
                    }
            }

            return phraseBeginning;
        }
    }
}
