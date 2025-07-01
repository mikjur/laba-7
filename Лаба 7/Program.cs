using System;
using System.Collections.Generic;

class Program
{
    static readonly Dictionary<string, Action> tasks = new()
    {
        { "1", LabTasks.Task1 },
        { "2", LabTasks.Task2 },
        { "3", LabTasks.Task3 },
        { "6", new Task6().Execute },
        { "7", new Task7().Execute },
        { "8", new Task8().Execute },
        { "9", new Task9().Execute },
    };

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Лабораторная работа ===");
            for (int i = 1; i <= 10; i++)
                Console.WriteLine($"{i}. Задание {i}");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите задание: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "0") return;

            if (tasks.TryGetValue(choice, out Action task))
            {
                try { task(); }
                catch (Exception ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
            }
            else
            {
                Console.WriteLine("Неверный выбор!");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}
