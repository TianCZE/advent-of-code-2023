using System.Numerics;

namespace _3;


public static class Day3
{
    // public static List<int?> Numbers = new List<int?>();
    public static Dictionary<Vector2, int> Numbers = new Dictionary<Vector2, int>();
    public static Dictionary<Vector2, bool> Symbols = new Dictionary<Vector2, bool>();
    
    public static async Task Main()
    {
        var input = new StreamReader(await TaskInput.FetchAndSave(3));
        // var input = new StringReader(TaskInput.GetExample(""));

        var line = input.ReadToEnd();

        var tokens = line.Split(".");

        foreach (var token in tokens)
        {
            Console.Write($"[{token}] ");
        }
    }
    
    public static void StoreNumbers(string line, int y)
    {
        var numbers = line
            .Zip(Enumerable.Range(0, line.Length))
            .Where(tuple => char.IsDigit(tuple.First));
        
        foreach ((char first, int x) in numbers)
        {
            int number = Convert.ToInt16(first);
            Numbers[new Vector2(x, y)] = number;
        }
    }
    
    public static void StoreSymbols(string line, int y)
    {
        var symbols = line
            .Zip(Enumerable.Range(0, line.Length))
            .Where(tuple => !char.IsDigit(tuple.First) && tuple.First != '.');
        
        foreach ((_, int x) in symbols)
        {
            Symbols[new Vector2(x, y)] = true;
        }
    }

    public static void CheckAround(Vector2 position)
    {
        float x = position.X;
        float y = position.Y;
        
        for (int i = -1; i < 4; i++)
        {
            for (int j = -1; j < 4; j++)
            {
                var newPosition = new Vector2(x + i, y + j);
                int number;
                if (Numbers.TryGetValue(newPosition, out number))
                {
                    
                }
            }
        }
    }


    public static void CheckHorizontal(Vector2 position)
    {
        
    }
}


