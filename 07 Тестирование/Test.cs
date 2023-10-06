//простые поля
[TestCase("text", new[] {"text"})]
[TestCase("hello world", new[] {"hello", "world"})]
[TestCase("hello   world", new[] {"hello", "world"})]
[TestCase(" text ", new[] {"text"})]

//поля в кавычках
[TestCase("''", new[] {""})]
[TestCase("\"  \"", new[] {"  "})]
[TestCase("\"a 'b' d\"", new[] {"a 'b' d"})]
[TestCase("\'a \"b\" d\'", new[] {"a \"b\" d"})]
[TestCase("a\"b c d e\"", new[] {"a", "b c d e"})]
[TestCase("\"b c d e\"f", new[] {"b c d e", "f"})]
[TestCase("\"def g h", new[] {"def g h"})]
[TestCase("\"\\\\\"", new[] {"\\"})]
[TestCase("", new string[0])]
[TestCase("\"def g h ", new[] {"def g h "})]
[TestCase(@"""\""", new[] {@""""})]
[TestCase(@"'\''", new[] {@"'"})]

// Вставляйте сюда свои тесты
public static void RunTests(string input, string[] expectedOutput)
{
    // Тело метода изменять не нужно
    Test(input, expectedOutput);
}
