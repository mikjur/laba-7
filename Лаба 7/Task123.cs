using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;

public static class LabTasks
{
    public static void Task1()
    {
        Console.Write("Количество чисел: ");
        int cnt = int.Parse(Console.ReadLine());

        Console.Write("Вводить вещественные числа? (y/n): ");
        bool isDouble = Console.ReadLine().Trim().ToLower() == "y";

        string fileName = "task1.txt";
        if (isDouble)
            FillFileWithDoubles(fileName, cnt);
        else
            FillFileWithIntegers(fileName, cnt);

        double result = Task1Logic(fileName, isDouble);
        Console.WriteLine($"Разность max - min: {result}");
    }

    private static void FillFileWithDoubles(string filename, int count)
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

    private static void FillFileWithIntegers(string filename, int count)
    {
        using var sw = new StreamWriter(filename);
        for (int i = 0; i < count; i++)
        {
            Console.Write($"[{i + 1}] > ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int val))
                sw.WriteLine(val);
            else
                i--; // повтор ввода
        }
    }

    private static double Task1Logic(string filename, bool isDouble)
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

    public static void Task2()
    {
        Console.Write("Количество строк: ");
        int ln = int.Parse(Console.ReadLine());

        FillMultilineFile("task2.txt", ln);
        int min = Task2Logic("task2.txt");
        Console.WriteLine($"Минимум: {min}");
    }

    private static void FillMultilineFile(string filename, int lines)
    {
        using var sw = new StreamWriter(filename);
        for (int i = 0; i < lines; i++)
        {
            Console.Write($"[{i + 1}] > ");
            sw.WriteLine(Console.ReadLine());
        }
    }

    private static int Task2Logic(string filename)
    {
        var lines = File.ReadAllLines(filename);
        int min = int.MaxValue;
        foreach (var l in lines)
            foreach (var p in l.Split(' ', '\t'))
                if (int.TryParse(p, out int v) && v < min)
                    min = v;
        return min;
    }

    public static void Task3()
    {
        const string inF = "task_3_1.txt";
        const string outF = "task_3_2.txt";
        Console.Write("Начальный символ: ");
        char c = Console.ReadKey().KeyChar;
        Console.WriteLine();
        Task3Logic(inF, outF, c);
        Console.WriteLine($"Результат записан в файл {outF}");
    }

    private static void Task3Logic(string inF, string outF, char start)
    {
        if (!File.Exists(inF))
        {
            File.WriteAllLines(inF, new[] { "apple", "banana", "avocado", "berry" });
        }

        var result = new List<string>();
        foreach (var l in File.ReadAllLines(inF))
        {
            if (!string.IsNullOrEmpty(l) && l[0] == start)
                result.Add(l);
        }

        File.WriteAllLines(outF, result);
    }
}
