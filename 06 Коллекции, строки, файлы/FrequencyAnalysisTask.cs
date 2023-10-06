using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var outDictionary = new Dictionary<string, string>();
            var frequencyDictionary = new Dictionary<string, Dictionary<string, int>>();

            frequencyDictionary = MakeBiGramms(text);
            frequencyDictionary.Append(MakeTriGramms(text));

            outDictionary = MaxGrammy(frequencyDictionary);
            return outDictionary;
        }

		public static Dictionary<string, string> 
MinValues(string st, Dictionary<string, Dictionary<string, int>> frequencyDictionary)
        {
            string min = frequencyDictionary[st].Keys.Min();

            foreach (var str in frequencyDictionary[st])
            {
                if (string.CompareOrdinal(str.Key, min) < 0)
                    min = str.Key;
            }

            return new Dictionary<string, string> { [st] = min };
        }

		public static Dictionary<string, string> 
MaxValues(string st, Dictionary<string, Dictionary<string, int>> frequencyDictionary)
        {
            bool flagMin = true;
            string min = "";

            foreach (var str in frequencyDictionary[st])
            {
                if (str.Value == frequencyDictionary[st].Values.Max())
                {
                    if (flagMin)
                    {
                        flagMin = false;
                        min = str.Key;
                    }
                    else
                    {
                        if (string.CompareOrdinal(str.Key, min) < 0)
                            min = str.Key;
                    }
                }
            }
            
            return new Dictionary<string, string> { [st] = min };
        }

        //функция нахождения самого частного продолжения
        static public Dictionary<string, string>
        MaxGrammy(Dictionary<string, Dictionary<string, int>> frequencyDictionary)
        {
            var outDictionary = new Dictionary<string, string>();

            foreach (var st in frequencyDictionary)
            {
                if (frequencyDictionary[st.Key].Values.Max() == frequencyDictionary[st.Key].Values.Min())
                {
                    outDictionary.Append(MinValues(st.Key, frequencyDictionary));
                }
                else
                {
                   outDictionary.Append(MaxValues(st.Key, frequencyDictionary));
                }
            }
            return outDictionary;
        }
       
		static public void 
AddBiGrammsDictionary(List<string> sentence, Dictionary<string, Dictionary<string, int>> frequencyDictionary)
        {
            int i;
            var length = sentence.Count;

            if (length >= 2) //создание биграмм
            {
                //создание частотного словаря биграмм
                for (i = 0; i < length - 1; i++)
                {
                    if (!frequencyDictionary.ContainsKey(sentence[i]))
                        frequencyDictionary.Add(sentence[i], new Dictionary<string, int> { [sentence[i + 1]] = 1 });
                    else
                    {
                        if (!frequencyDictionary[sentence[i]].ContainsKey(sentence[i + 1]))
                            frequencyDictionary[sentence[i]].Add(sentence[i + 1], 1);
                        else
                            frequencyDictionary[sentence[i]][sentence[i + 1]]++;
                    }
                }
            }
        }

        static public Dictionary<string, Dictionary<string, int>> MakeBiGramms(List<List<string>> text)
        {
            var frequencyDictionary = new Dictionary<string, Dictionary<string, int>>();
                     
            foreach (var sentence in text)//проход по предложениям Биграммама
            {
               AddBiGrammsDictionary(sentence, frequencyDictionary);
            }

            return frequencyDictionary;
        }

		static public void 
AddTriGrammsDictionary(List<string> sentence, Dictionary<string, Dictionary<string, int>> frequencyDictionary)
        {
            int i;
            var length = sentence.Count;//количество слов в предложении

            if (length >= 3) //создание триграмм
            {
                //создание частотного словаря триграмм
                for (i = 0; i < length - 2; i++)
                {
                    if (!frequencyDictionary.ContainsKey(sentence[i] + " " + sentence[i + 1]))
                        frequencyDictionary.Add(sentence[i] + " " + sentence[i + 1],
                            new Dictionary<string, int> { [sentence[i + 2]] = 1 });
                    else
                    {
                        if (!frequencyDictionary[sentence[i] + " " + sentence[i + 1]].ContainsKey(sentence[i + 2]))
                            frequencyDictionary[sentence[i] + " " + sentence[i + 1]].Add(sentence[i + 2], 1);
                        else
                            frequencyDictionary[sentence[i] + " " + sentence[i + 1]][sentence[i + 2]]++;
                    }
                }
            }
		}

        static public Dictionary<string, Dictionary<string, int>> MakeTriGramms(List<List<string>> text)
        {
            var frequencyDictionary = new Dictionary<string, Dictionary<string, int>>();
            
            foreach (var sentence in text)//проход по предложениям Триграммами
            {
               AddTriGrammsDictionary(sentence, frequencyDictionary);
            }

            return frequencyDictionary;
        }

        //копирует один словать в конец другого
        public static void Append<K, V>(this Dictionary<K, V> first, Dictionary<K, V> second)
        {
            List<KeyValuePair<K, V>> pairs = second.ToList();
            pairs.ForEach(pair => first.Add(pair.Key, pair.Value));
        }
    }
}
