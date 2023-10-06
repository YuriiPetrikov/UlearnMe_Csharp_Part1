using System.Collections.Generic;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        // Скопируйте сюда метод с тестами из предыдущей задачи.
        //простые поля
        [TestCase("text", new[] { "text" })]
        [TestCase("hello world", new[] { "hello", "world" })]
        [TestCase("hello   world", new[] { "hello", "world" })]
        [TestCase(" text ", new[] { "text" })]

        //поля в кавычках
        [TestCase("''", new[] { "" })]
        [TestCase("\"  \"", new[] { "  " })]
        [TestCase("\"a 'b' d\"", new[] { "a 'b' d" })]
        [TestCase("\'a \"b\" d\'", new[] { "a \"b\" d" })]
        [TestCase("a\"b c d e\"", new[] { "a", "b c d e" })]
        [TestCase("\"b c d e\"f", new[] { "b c d e", "f" })]
        [TestCase("\"def g h", new[] { "def g h" })]
        [TestCase("\"\\\\\"", new[] { "\\" })]
        [TestCase("", new string[0])]
        [TestCase("\"def g h ", new[] { "def g h " })]
        //[TestCase(@"'\''", new[] { @"'" })]

        // Вставляйте сюда свои тесты
        public static void RunTests(string input, string[] expectedOutput)
        {
            // Тело метода изменять не нужно
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        // При решении этой задаче постарайтесь избежать создания методов, длиннее 10 строк.
        // Подумайте как можно использовать ReadQuotedField и Token в этой задаче.
        public static List<Token> ParseLine(string line)
        {
            int startIndex = 0;//индекс строки, с которого надо искать следующий токен
            var listToken = new List<Token>();
           
            while (startIndex < line.Length)
            {
                //простой Токен
                if (line[startIndex] != '\"' || line[startIndex] != '\'')
                    startIndex = SimpleToken(listToken, startIndex, line);
                
                if (startIndex == line.Length)
                    break;

                //токен в кавычках
                if (line[startIndex] == '\"' || line[startIndex] == '\'')
                { 
                    if(ReadQuotedField(line, startIndex).Length > 0)
                        listToken.Add(ReadQuotedField(line, startIndex));
                    startIndex = ReadQuotedField(line, startIndex).GetIndexNextToToken();
                }
            }

            return listToken;
        }
        
        private static Token ReadField(string line, int startIndex)
        {
            return new Token(line, 0, line.Length);
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }

        public static int SimpleToken(List<Token> listToken, int startIndex, string line)
        {
            int pos = startIndex;
            bool flSpace = false;
            string buf = "";
            
            while (line[startIndex] != '\"' && line[startIndex] != '\'')
            {
                if (line[startIndex] == ' ')
                {
                    flSpace = true;

                    if (buf.Length > 0) 
                        listToken.Add(new Token(buf, pos, buf.Length));
                    
                    buf = "";
                }
                else
                {
                    if (flSpace) 
                        pos = startIndex;
                    buf += line[startIndex];
                    flSpace = false;
                }
                
                startIndex++;
                if (startIndex == line.Length) 
                    break;
            }

            if (buf.Length > 0) 
                listToken.Add(new Token(buf, pos, buf.Length));
           
            return startIndex;
        }
    }
}
