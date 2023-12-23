namespace _1;

public interface IAlgorithm
{
    int Execute(string line);
}

public static class Parser
{
    public static int ParseDigits(IEnumerable<char> digits)
    {
        return int.Parse($"{digits.First()}{digits.Last()}");
    }
}

public class SolveNumbersOnly : IAlgorithm
{
    public int Execute(string line)
    {
        var digits = line.Where(char.IsDigit);
        return Parser.ParseDigits(digits);
    }
}

public class SolveSpelledNumbers : IAlgorithm
{
    public static Dictionary<string, int> SpelledNumbers { get; } = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 }
    };

    public int Execute(string line)
    {
        line = SpelledNumbers.Aggregate(line, (current, entry) => InsertNumber(current, entry)!);
        return Parser.ParseDigits(line!.Where(char.IsDigit));
    }

    public static string? InsertNumber(string? line, KeyValuePair<string, int> entry)
    {
        int index;
        while ((index = line?.IndexOf(entry.Key) ?? -1) > -1) line = line!.Insert(index + 1, entry.Value.ToString());

        return line;
    }
}

public class Day1
{
    public static async Task Main()
    {
        var input = new StreamReader(await TaskInput.GetInput(1));

        // var ads = "1abc2\npqr3stu8vwx\na1b2c3d4e5f\ntreb7uchet";
        // var ads = 
        // "two1nine\n" +
        // "eightwothree\n" +
        // "abcone2threexyz\n" +
        // "xtwone3four\n" +
        // "4nineeightseven2\n" +
        // "zoneight234\n" +
        // "7pqrstsixteen\n";

        // var input = new StringReader(ads);

        var sum = 0;

        IAlgorithm algorithm = new SolveSpelledNumbers();

        while (await input.ReadLineAsync() is { } line)
            sum += algorithm.Execute(line);

        Console.WriteLine(sum);
    }
}
