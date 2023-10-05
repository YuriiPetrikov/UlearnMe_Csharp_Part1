public static double Calculate(string userInput)
{
    string[] str = userInput.Split();
    var sum     = Double.Parse(str[0]);
    var percent = Double.Parse(str[1]);
    var month   = Double.Parse(str[2]);

	return sum* Math.Pow((1.0 + percent / 100/12), month);
}
