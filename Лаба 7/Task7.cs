using System;
using System.Collections.Generic;

public class Task7 : ILabTask
{
    
    public void Execute()
    {
        Console.WriteLine("Введите список чисел:");
        var list = ParseInput(Console.ReadLine());

        int count = 0;
        for (int i = 1; i < list.Count - 1; i++)
            if (list[i - 1] == list[i + 1]) count++;

        Console.WriteLine($"Чисел с одинаковыми соседями: {count}");
    }

    private List<double> ParseInput(string input)
    {
        var list = new List<double>();
        foreach (var s in input.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            if (double.TryParse(s, out double val)) list.Add(val);
        return list;
    }
}
