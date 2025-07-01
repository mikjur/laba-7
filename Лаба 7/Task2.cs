/*using System;
using System.IO;

public class Task2 : ILabTask
{
    public void Execute()
    {
        Console.Write("Количество строк: ");
        int ln = int.Parse(Console.ReadLine());
        HelperMethods.FillMultilineFileWithUserInput("task2.txt", ln);
        int min = Task2Logic("task2.txt");
        Console.WriteLine($"Минимум: {min}");
    }

    private int Task2Logic(string filename)
    {
        var lines = File.ReadAllLines(filename);
        int min = int.MaxValue;
        foreach (var l in lines)
            foreach (var p in l.Split(' ', '\t'))
                if (int.TryParse(p, out int v) && v < min)
                    min = v;
        return min;
    }
}
*/