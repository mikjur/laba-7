using System;
using System.Collections.Generic;
public class Task9 : ILabTask
{
    
    

    public void Execute()
    {
        const string fileName = "task9.txt";
        Console.WriteLine("Введите строки для записи в файл (пустая строка — завершение ввода):");

        var lines = new List<string>();
        while (true)
        {
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) break;
            lines.Add(input);
        }

        File.WriteAllLines(fileName, lines);

        var result = AnalyzeFile(fileName);
        if (result.Count > 0)
        {
            Console.WriteLine("\nСогласные, встречающиеся только в одном слове:");
            Console.WriteLine(string.Join(", ", result));
        }
        else
        {
            Console.WriteLine("\nНет согласных, встречающихся только в одном слове.");
        }
    }

    private List<char> AnalyzeFile(string filename)
    {
        // Согласные буквы русского алфавита
        var consonants = new HashSet<char>("бвгджзйклмнпрстфхцчшщ");
        var count = new Dictionary<char, int>();

        foreach (var line in File.ReadAllLines(filename))
        {
            var words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                var used = new HashSet<char>();

                foreach (var ch in word)
                {
                    var lower = char.ToLower(ch);
                    if (consonants.Contains(lower) && used.Add(lower))
                    {
                        if (count.ContainsKey(lower))
                            count[lower]++;
                        else
                            count[lower] = 1;
                    }
                }
            }
        }

        // Оставляем только те, что встречаются ровно в одном слове
        var result = count
            .Where(kv => kv.Value == 1)
            .Select(kv => kv.Key)
            .OrderBy(c => c)
            .ToList();

        return result;
    }
}
