using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase("\"abc\"", 0, "abc", 5)]
        [TestCase("'", 0, "", 1)]
        [TestCase("\"", 0, "", 1)]
        [TestCase("'a\"\"'", 0, "a\"\"", 5)]
        [TestCase("\"abc\"asas", 0, "abc", 5)]
        [TestCase("'a\"\"'", 0, "a\"\"", 5)]
        [TestCase("'a' b'", 0, "a", 3)]
        [TestCase("'a", 0, "a", 2)]
        [TestCase("b \"a'\"", 2, "a'", 4)]
        [TestCase("sx\"a'\"", 2, "a'", 4)]
      //[TestCase("\"'\"", 0, "", 3)]

        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static int CalculatingLengthToken(string line, int startIndex, char startSymbol)
        {
            int i;
            for (i = startIndex + 1; i < line.Length; i++)
            {
                if (line[i] == '\\')
                    i += 2;
                if (line[i] == startSymbol)
                {
                    i++;
                    break;
                }
            }
            return i - startIndex;
        }

        public static string NewToken(string line, int startIndex)
        {
            char startSymbol = line[startIndex];
            string token = "";

            for (int j = startIndex + 1; j < line.Length; j++)
            {
                if (line[j] == '\\')
                {
                    j++;
                    token += line[j];
                    continue;
                }

                if (line[j] == startSymbol)
                {
                    break;
                }

                token += line[j];
            }

            return token;
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            while (line[startIndex] != '\"' && line[startIndex] != '\'')
                startIndex++;
            
            string token = NewToken(line, startIndex);
           
            int i = line[startIndex] == '\"' ? CalculatingLengthToken(line, startIndex, '\"') :
                CalculatingLengthToken(line, startIndex, '\'');
            
            return new Token(token, startIndex, i);
        }
    }
}
