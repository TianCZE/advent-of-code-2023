using System.Text.RegularExpressions;

namespace _2;


public class Bag
{
    private readonly Dictionary<string, List<Cube>> cubes;

    public Bag(int red, int green, int blue)
    {
        this.cubes = new Dictionary<string, List<Cube>>
        {
            ["red"] = Enumerable.Range(0, red).Select(_ => new Cube { Color = Cube.Colors.Red }).ToList(),
            ["green"] = Enumerable.Range(0, green).Select(_ => new Cube { Color = Cube.Colors.Green }).ToList(),
            ["blue"] = Enumerable.Range(0, blue).Select(_ => new Cube { Color = Cube.Colors.Blue }).ToList()
        };
    }

    public bool IsDiceAmountPossible(int amount, string color)
    {
        return this.cubes[color].Count >= amount;
    }

    public void UpdateDice(int amount, string color)
    {
        this.cubes[color] = this.cubes[color].Count < amount ? Enumerable.Range(0, amount).Select(_ => new Cube()).ToList() : this.cubes[color];
    }

    public int CalculateProduct()
    {
        return this.cubes.Aggregate(1, (product, list) => list.Value.Count * product);
    }
}

public record struct Cube
{
    public enum Colors : int
    {
        Red,
        Blue,
        Green,
    }
    
    public Colors Color { get; init; }
}


public static partial class Day2
{
    public static async Task Main()
    {
        var input = new StreamReader(await TaskInput.GetInput(2));

        // const string example = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green\nGame 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue\nGame 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red\nGame 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red\nGame 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";
        // var exampleReader = new StringReader(example);

        int idSum = 0;

        while (await input.ReadLineAsync() is { } line)
        {
            // idSum += ValidateGame(line);
            idSum += CalculateMaxPossibleDice(line);
        }

        Console.WriteLine(idSum);
    }

    private static int ValidateGame(string line)
    {
        var bag = new Bag(12, 13, 14);
        
        var match = MyRegex().Match(line).Groups;
        var id = int.Parse(match[1].Value);
        var pulls = match[2].Value.Trim().Split(';');

        return GetDice(pulls).Any(dice => !bag.IsDiceAmountPossible(dice.Item1, dice.Item2)) ? 0 : id;
    }

    private static int CalculateMaxPossibleDice(string line)
    {
        var bag = new Bag(0, 0, 0);
        
        var pulls = MyRegex().Match(line).Groups[2].Value.Trim().Split(';');
        var dices = GetDice(pulls);

        foreach (var dice in dices)
        {
            bag.UpdateDice(dice.Item1, dice.Item2);
        }
        return bag.CalculateProduct();
    }
    
    private static List<Tuple<int, string>> GetDice(string[] pulls)
    {
        var diceList = new List<Tuple<int, string>>();
        foreach (var pull in pulls)
        {
            var dices = pull.Split(',');
            foreach (var dice in dices)
            {
                var tokens = dice.Trim().Split(' ');
                diceList.Add(new Tuple<int, string>(int.Parse(tokens[0]), tokens[1]));
            }
        }

        return diceList;
    }

    [GeneratedRegex("Game (\\d+):(.*)")]
    private static partial Regex MyRegex();
}
