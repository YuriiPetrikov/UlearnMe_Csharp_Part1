using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        Dictionary<string, Dictionary<int, List<int>>> wordsIdsPositions = 
            new Dictionary<string, Dictionary<int, List<int>>>();
        
        public void Add(int id, string documentText)
        {
            var split = new char[] { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };
            var subs = documentText.Split(split);
            var index = 0;
            for (int i = 0; i < subs.Length; i++)
            {
                if (i > 0)
                    index += subs[i - 1].Length + 1;

                if (!wordsIdsPositions.ContainsKey(subs[i]))
                {
                    wordsIdsPositions.Add(subs[i], new Dictionary<int, List<int>>
                        { {id, new List<int>(){ index }}});
                }
                else
                {
                    if (!wordsIdsPositions[subs[i]].ContainsKey(id))
                        wordsIdsPositions[subs[i]].Add(id, new List<int>() { index });
                    else
                        wordsIdsPositions[subs[i]][id].Add(index);
                }
            }
        }

        public List<int> GetIds(string word)
        {
            var getIds = new List<int>();
            if (wordsIdsPositions.ContainsKey(word))
            {
                foreach (var key in wordsIdsPositions[word])
                    getIds.Add(key.Key);
            }

            return getIds;
        }

        public List<int> GetPositions(int id, string word)
        {
            var getPositions = new List<int>();
            if (wordsIdsPositions.ContainsKey(word))
            {
                if (wordsIdsPositions[word].ContainsKey(id))
                {
                    getPositions = wordsIdsPositions[word][id];
                }
            }
          
            return getPositions;
        }

        public void Remove(int id)
        {
            foreach(var idWord in wordsIdsPositions)
            {
                idWord.Value.Remove(id);
            }
        }
    }
}
