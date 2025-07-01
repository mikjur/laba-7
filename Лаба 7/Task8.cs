using System;
using System.Collections.Generic;

public class Task8 : ILabTask
{
    public void Execute()
    {
        var menu = new List<Dish>
        {
            new Dish { Name = "Суп" },
            new Dish { Name = "Салат" },
            new Dish { Name = "Чай" },
            new Dish { Name = "Кофе" },
            new Dish { Name = "Пирог" }
        };

        Console.WriteLine("Меню кафе:");
        for (int i = 0; i < menu.Count; i++)
            Console.WriteLine($"{i + 1}. {menu[i].Name}");

        Console.Write("Введите количество посетителей: ");
        if (!int.TryParse(Console.ReadLine(), out int visitorCount) || visitorCount < 1)
        {
            Console.WriteLine("Неверное количество посетителей");
            return;
        }

        var orders = new List<(string Visitor, List<int> DishIndexes)>();

        for (int i = 0; i < visitorCount; i++)
        {
            Console.Write($"Введите имя посетителя #{i + 1}: ");
            var visitor = Console.ReadLine()?.Trim() ?? "";

            List<int> dishIndexes = null;
            while (true)
            {
                Console.WriteLine($"Введите номера блюд, заказанных {visitor}, через запятую (например: 1,3):");
                var input = Console.ReadLine() ?? "";

                var parts = input
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                bool allValid = true;
                dishIndexes = new List<int>();

                foreach (var part in parts)
                {
                    if (int.TryParse(part, out int idx))
                    {
                        idx -= 1;
                        if (idx >= 0 && idx < menu.Count)
                            dishIndexes.Add(idx);
                        else
                        {
                            Console.WriteLine($"Ошибка: номер блюда {idx + 1} вне диапазона меню.");
                            allValid = false;
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка: '{part}' — не число.");
                        allValid = false;
                        break;
                    }
                }

                if (allValid)
                    break;
                else
                    Console.WriteLine("Повторите ввод блюд.");
            }

            orders.Add((visitor, dishIndexes));
        }

        foreach (var order in orders)
            foreach (var dishIdx in order.DishIndexes)
                menu[dishIdx].OrderedBy.Add(order.Visitor);

        Console.WriteLine("\nКто что заказал:");
        foreach (var dish in menu)
        {
            if (dish.OrderedBy.Count == 0)
                Console.WriteLine($"{dish.Name}: никто не заказывал");
            else
                Console.WriteLine($"{dish.Name}: заказали {dish.OrderedBy.Count} человек — {string.Join(", ", dish.OrderedBy)}");
        }

        Console.WriteLine("\nКлассификация блюд по количеству посетителей:");
        foreach (var dish in menu)
        {
            if (dish.OrderedBy.Count == visitorCount)
                Console.WriteLine($"{dish.Name}: заказали все посетители");
            else if (dish.OrderedBy.Count > 0)
                Console.WriteLine($"{dish.Name}: заказали некоторые посетители");
            else
                Console.WriteLine($"{dish.Name}: никто не заказал");
        }
    }
}
