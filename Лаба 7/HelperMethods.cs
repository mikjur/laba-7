using System;
using System.IO;

public static class HelperMethods
{

    public static void FillFileWithUserInput(string filename, int count)
    {
        using var writer = new StreamWriter(filename);
        Console.WriteLine($"Введите {count} целых чисел:");
        for (int i = 0; i < count; i++)
        {
            int num;
            while (!int.TryParse(Console.ReadLine(), out num))
                Console.WriteLine("Ошибка! Введите снова:");
            writer.WriteLine(num);
        }
    }

    public static void FillMultilineFileWithUserInput(string filename, int linesCount)
    {
        using var writer = new StreamWriter(filename);
        Console.WriteLine($"Введите {linesCount} строк чисел (через пробел):");
        for (int i = 0; i < linesCount; i++)
        {
            string line;
            bool ok;
            do
            {
                Console.Write($"Строка {i + 1}: ");
                line = Console.ReadLine();
                ok = true;
                foreach (var part in line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    if (!int.TryParse(part, out _))
                    {
                        ok = false;
                        Console.WriteLine("Некорректный ввод, повторите строку.");
                        break;
                    }
            } while (!ok);
            writer.WriteLine(line);
        }
    }

    public static void FillBinaryFileWithUserInput(string filename, int count)
    {
        using var writer = new BinaryWriter(File.Open(filename, FileMode.Create));
        Console.WriteLine($"Введите {count} байт (0–255):");
        for (int i = 0; i < count; i++)
        {
            byte b;
            while (!byte.TryParse(Console.ReadLine(), out b))
                Console.WriteLine("Ошибка! Введите число от 0 до 255:");
            writer.Write(b);
        }
    }
}
