// Вставьте сюда финальное содержимое файла SentencesParserTask.cs
using System;
using System.Collections.Generic;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<string> SentenceWords(string sentence)//разбивает предложение на список слов
        {
            int lengthSentence = sentence.Length;
            string word = "";
            var listWord = new List<string>();

            for (int i = 0; i < lengthSentence; i++) //проходим посимвольно по предложению
            {
                if (char.IsLetter(sentence[i]) || sentence[i] == '\'')
                {
                    word += char.ToLower(sentence[i]);
                }
                else if(word != "")
                {
                    listWord.Add(word);
                    word = "";
                }
            }

            if(word != "")//добавление последенго слова в предложении
            {
                listWord.Add(word);
            }

            return listWord;
        }
        
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            
            char[] delimiterChars = { '.', '!', '?', ';', ':', '(', ')'};
            string[] sentences = text.Split(delimiterChars);//массив предложений
            int lengthSentence = sentences.Length;

            for (int i = 0; i < lengthSentence; i++) //проходим по предложениям
            {
                if (SentenceWords(sentences[i]).Count != 0)//проверка на пустые строки
                {
                    sentencesList.Add(SentenceWords(sentences[i]));
                }    
            }

            return sentencesList;
        }
    }
}
