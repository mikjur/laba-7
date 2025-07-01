/*using System;
using System.IO;
using System.Globalization;

public class Task1 : ILabTask
{
    public void Execute()
    {
        Console.Write("Количество чисел: ");
        int cnt = int.Parse(Console.ReadLine());

        Console.Write("Вводить вещественные числа? (y/n): ");
        bool isDouble = Console.ReadLine().Trim().ToLower() == "y";

        string fileName = "task1.txt";
        if (isDouble)
            FillFileWithDoubles(fileName, cnt);
        else
            HelperMethods.FillFileWithUserInput(fileName, cnt);

        double result = Task1Logic(fileName, isDouble);
        Console.WriteLine($"Разность max - min: {result}");
    }

    private void FillFileWithDoubles(string filename, int count)
    {
        using var sw = new StreamWriter(filename);
        for (int i = 0; i < count; i++)
        {
            Console.Write($"[{i + 1}] > ");
            string input = Console.ReadLine();
            if (double.TryParse(input.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double val))
                sw.WriteLine(val.ToString(CultureInfo.InvariantCulture));
            else
                i--; // повтор ввода
        }
    }

    private double Task1Logic(string filename, bool isDouble)
    {
        var lines = File.ReadAllLines(filename);
        double max = double.MinValue, min = double.MaxValue;

        foreach (var l in lines)
        {
            if (double.TryParse(l.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double v))
            {
                if (v > max) max = v;
                if (v < min) min = v;
            }
        }

        return max - min;
    }
}*/
