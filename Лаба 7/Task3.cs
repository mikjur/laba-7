/*using System;
using System.Collections.Generic;
using System.IO;

public class Task3 : ILabTask
{
    public void Execute()
    {
        const string inF = "task_3_1.txt";
        const string outF = "task_3_2.txt";
        Console.Write("Начальный символ: ");
        char c = Console.ReadKey().KeyChar;
        Console.WriteLine();
        Task3Logic(inF, outF, c);
        Console.WriteLine($"Результат записан в файл {outF}");
    }

    private void Task3Logic(string inF, string outF, char start)
    {
        if (!File.Exists(inF))
            File.WriteAllLines(inF, new[] { "apple", "banana", "avocado", "berry" });

        var result = new List<string>();
        foreach (var l in File.ReadAllLines(inF))
            if (!string.IsNullOrEmpty(l) && l[0] == start)
                result.Add(l);

        File.WriteAllLines(outF, result);
    }
}
*/