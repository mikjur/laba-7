using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public static class Lab7
{
	// Задание 1: Разность max и min элементов
	public static int Task1(string filename)
	{
		int[] numbers = ReadIntFile(filename);
		if (numbers.Length == 0) return 0;

		int max = numbers[0], min = numbers[0];
		foreach (int num in numbers)
		{
			if (num > max) max = num;
			if (num < min) min = num;
		}
		return max - min;
	}

	// Задание 2: Минимальный элемент
	public static int Task2(string filename)
	{
		int[] numbers = ReadMultilineIntFile(filename);
		if (numbers.Length == 0) throw new InvalidOperationException("Файл пуст");

		int min = numbers[0];
		foreach (int num in numbers)
			if (num < min) min = num;
		return min;
	}

	// Задание 3: Фильтрация строк
	public static void Task3(string input, string output, char startChar)
	{
		List<string> result = new List<string>();
		foreach (string line in File.ReadAllLines(input))
		{
			if (line.Length > 0 && line[0] == startChar)
				result.Add(line);
		}
		File.WriteAllLines(output, result);
	}


	// Задание 4: Обработка бинарного файла
	public static void Task4(string input, string output, int m, int n)
	{
		byte[] data = File.ReadAllBytes(input);
		List<byte> result = new List<byte>();

		foreach (byte b in data)
			if (b % m == 0 && b % n != 0)
				result.Add(b);

		File.WriteAllBytes(output, result.ToArray());
	}

	//Задание 5: Стирелизация:
	[XmlRoot("Passengers")]
	public class PassengerList
	{
		[XmlElement("Passenger")]
		public List<PassengerData> Passengers { get; set; }
	}

	public class PassengerData
	{
		[XmlElement("Luggage")]
		public List<LuggageItem> Luggage { get; set; }
	}

	public class LuggageItem
	{
		[XmlAttribute]
		public string Name { get; set; }

		[XmlAttribute]
		public double Weight { get; set; }
	}

	// Метод для заполнения бинарного файла через XML-сериализацию
	public static void FillPassengersFile(string filename)
	{
		var passengers = new PassengerList
		{
			Passengers = new List<PassengerData>
		{
			new PassengerData
			{
				Luggage = new List<LuggageItem>
				{
					new LuggageItem { Name = "Чемодан", Weight = 20.5 },
					new LuggageItem { Name = "Сумка", Weight = 5.3 }
				}
			},
			new PassengerData
			{
				Luggage = new List<LuggageItem>
				{
					new LuggageItem { Name = "Рюкзак", Weight = 8.7 },
					new LuggageItem { Name = "Коробка", Weight = 3.2 },
					new LuggageItem { Name = "Саквояж", Weight = 4.1 }
				}
			},
			new PassengerData
			{
				Luggage = new List<LuggageItem>
				{
					new LuggageItem { Name = "Сумка", Weight = 2.8 }
				}
			},
            // Новые пассажиры
            new PassengerData
			{
				Luggage = new List<LuggageItem>
				{
					new LuggageItem { Name = "Чемодан", Weight = 15.0 },
					new LuggageItem { Name = "Рюкзак", Weight = 6.1 },
					new LuggageItem { Name = "Портфель", Weight = 2.5 },
					new LuggageItem { Name = "Кейс", Weight = 1.8 }
				}
			},
			new PassengerData
			{
				Luggage = new List<LuggageItem>
				{
					new LuggageItem { Name = "Дорожная сумка", Weight = 9.4 }
				}
			},
			new PassengerData
			{
				Luggage = new List<LuggageItem>
				{
					new LuggageItem { Name = "Спортивная сумка", Weight = 7.2 },
					new LuggageItem { Name = "Чемодан", Weight = 12.3 },
					new LuggageItem { Name = "Пакет", Weight = 0.9 }
				}
			}
		}
		};

		var serializer = new XmlSerializer(typeof(PassengerList));
		using (var writer = new StreamWriter(filename))
		{
			serializer.Serialize(writer, passengers);
		}
	}

	// Основной метод анализа багажа
	public static (int, int) AnalyzeLuggage(string filename)
	{
		if (!File.Exists(filename))
			throw new FileNotFoundException("Файл не найден");

		// Десериализация данных
		var serializer = new XmlSerializer(typeof(PassengerList));
		PassengerList passengers;
		using (var reader = new StreamReader(filename))
		{
			passengers = (PassengerList)serializer.Deserialize(reader);
		}

		// Подсчет основных метрик
		int totalPassengers = passengers.Passengers.Count;
		double totalItems = 0;
		int overTwoCount = 0;
		List<int> itemCounts = new List<int>();

		foreach (var passenger in passengers.Passengers)
		{
			int count = passenger.Luggage.Count;
			totalItems += count;
			itemCounts.Add(count);

			if (count > 2)
				overTwoCount++;
		}

		double averageItems = totalItems / totalPassengers;
		int overAverageCount = 0;
		foreach (var count in itemCounts)
		{
			if (count > averageItems)
				overAverageCount++;
		}

		return (overTwoCount, overAverageCount);
	}

	// Задание 6: Слияние упорядоченных списков
	public static List<int> Task6(List<int> l1, List<int> l2)
	{
		List<int> result = new List<int>();
		int i = 0, j = 0;
		while (i < l1.Count && j < l2.Count)
		{
			if (l1[i] < l2[j])
				result.Add(l1[i++]);
			else
				result.Add(l2[j++]);
		}
		while (i < l1.Count) result.Add(l1[i++]);
		while (j < l2.Count) result.Add(l2[j++]);
		return result;
	}

	// Задание 7: Подсчет элементов с равными соседями
	public static int Task7(List<int> list)
	{
		int count = 0;
		for (int i = 1; i < list.Count - 1; i++)
		{
			if (list[i - 1] == list[i + 1])
				count++;
		}
		return count;
	}

	// Задание 8: Анализ заказов в кафе
	public class Dish
	{
		public string Name;
		public HashSet<string> OrderedBy = new HashSet<string>();
	}

	public static void Task8(List<Dish> menu, List<(string Visitor, List<string> Dishes)> orders)
	{
		foreach (var order in orders)
		{
			foreach (var dish in order.Dishes)
			{
				foreach (var menuDish in menu)
				{
					if (menuDish.Name == dish)
						menuDish.OrderedBy.Add(order.Visitor);
				}
			}
		}
	}

	// Задание 9: Анализ согласных букв
	public static List<char> Task9(string filename)
	{
		HashSet<char> consonants = new HashSet<char>("бвгджзйклмнпрстфхцчшщ".ToCharArray());
		Dictionary<char, int> counts = new Dictionary<char, int>();

		foreach (var line in File.ReadAllLines(filename))
		{
			foreach (var word in line.Split(' '))
			{
				if (string.IsNullOrWhiteSpace(word)) continue;

				string lowerWord = word.ToLower();
				HashSet<char> usedInWord = new HashSet<char>(); // Для уникальности в рамках слова

				foreach (char c in lowerWord)
				{
					if (consonants.Contains(c) && !usedInWord.Contains(c))
					{
						if (!counts.ContainsKey(c))
							counts[c] = 0;
						counts[c]++;
						usedInWord.Add(c); // Чтобы не учитывать повторы в одном слове
					}
				}
			}
		}

		List<char> result = new List<char>();
		foreach (var kvp in counts)
		{
			if (kvp.Value == 1) // Согласная встречается только в одном слове
				result.Add(kvp.Key);
		}
		result.Sort();
		return result;
	}
	// Задание 10: Топ участников с баллами
	public static List<string> Task10()
	{
		string filename = "scores.txt";
		if (!File.Exists(filename))
			throw new FileNotFoundException("Файл scores.txt не найден");

		var participants = new Dictionary<string, int>();
		foreach (var line in File.ReadAllLines(filename))
		{
			var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length != 6) continue; // Требуем: Фамилия Имя + 4 оценки

			string name = $"{parts[0]} {parts[1]}";
			int sum = 0;
			bool valid = true;

			for (int i = 2; i < 6; i++) // Проверяем 4 оценки
			{
				if (!int.TryParse(parts[i], out int score) || score < 0 || score > 10)
				{
					valid = false;
					break;
				}
				sum += score;
			}

			if (valid) participants[name] = sum;
		}

		if (participants.Count < 3)
			throw new InvalidOperationException("Недостаточно участников (минимум 3)");

		var sorted = new List<KeyValuePair<string, int>>(participants);
		sorted.Sort((a, b) => b.Value.CompareTo(a.Value));

		int threshold = sorted[2].Value;
		var result = new List<string>();
		foreach (var kvp in sorted)
		{
			if (kvp.Value >= threshold)
				result.Add($"{kvp.Key} - {kvp.Value} баллов");
			else
				break;
		}
		return result;
	}

	// Вспомогательные методы
	private static int[] ReadIntFile(string filename)
	{
		var lines = File.ReadAllLines(filename);
		int[] numbers = new int[lines.Length];
		for (int i = 0; i < lines.Length; i++)
			numbers[i] = int.Parse(lines[i]);
		return numbers;
	}

	private static int[] ReadMultilineIntFile(string filename)
	{
		List<int> numbers = new List<int>();
		foreach (var line in File.ReadAllLines(filename))
		{
			foreach (var part in line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries))
				numbers.Add(int.Parse(part));
		}
		return numbers.ToArray();
	}

	// Методы заполнения файлов
	public static void FillFileWithUserInput(string filename, int count)
	{
		using (StreamWriter writer = new StreamWriter(filename))
		{
			Console.WriteLine($"Введите {count} чисел:");
			for (int i = 0; i < count; i++)
			{
				string input;
				do
				{
					Console.Write($"Число {i + 1}: ");
					input = Console.ReadLine();
				} while (!int.TryParse(input, out int num));

				writer.WriteLine(input);
			}
		}
	}

	public static void FillBinaryFileWithUserInput(string filename, int count)
	{
		using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
		{
			Console.WriteLine($"Введите {count} чисел от 0 до 255:");
			for (int i = 0; i < count; i++)
			{
				byte num;
				do
				{
					Console.Write($"Число {i + 1}: ");
					string input = Console.ReadLine();
					if (byte.TryParse(input, out num) && num <= 255)
						break;
					Console.WriteLine("Ошибка: введите число от 0 до 255");
				} while (true);
				writer.Write(num);
			}
		}
	}

	public static void FillMultilineFileWithUserInput(string filename, int lineCount)
	{
		using (StreamWriter writer = new StreamWriter(filename))
		{
			Console.WriteLine($"Введите {lineCount} строк чисел (числа разделены пробелами):");
			for (int i = 0; i < lineCount; i++)
			{
				string line;
				List<int> nums;
				do
				{
					Console.Write($"Строка {i + 1}: ");
					line = Console.ReadLine();
					var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					nums = new List<int>();
					foreach (var part in parts)
					{
						if (int.TryParse(part, out int num))
							nums.Add(num);
						else
						{
							Console.WriteLine("Ошибка: некорректные данные. Повторите ввод.");
							nums.Clear();
							break;
						}
					}
				} while (nums.Count == 0);

				writer.WriteLine(string.Join(" ", nums));
			}
		}
	}

	static void Main()
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("=== Лабораторная работа ===");
			Console.WriteLine("1. Задание 1 - Разность max/min");
			Console.WriteLine("2. Задание 2 - Минимальный элемент");
			Console.WriteLine("3. Задание 3 - Фильтрация строк");
			Console.WriteLine("4. Задание 4 - Обработка бинарного файла");
			Console.WriteLine("5. Задание 5 - XML-сериализация");
			Console.WriteLine("6. Задание 6 - Слияние списков");
			Console.WriteLine("7. Задание 7 - Соседние элементы");
			Console.WriteLine("8. Задание 8 - Анализ заказов");
			Console.WriteLine("9. Задание 9 - Анализ текста");
			Console.WriteLine("10. Задание 10 - Лучшие участники");
			Console.WriteLine("0. Выход");
			Console.Write("Выберите задание: ");

			string choice = Console.ReadLine();
			Console.WriteLine();

			try
			{
				switch (choice)
				{
					case "1":
						RunTask1();
						break;
					case "2":
						RunTask2();
						break;
					case "3":
						RunTask3();
						break;
					case "4":
						RunTask4();
						break;
					case "5":
						RunTask5();
						break;
					case "6":
						RunTask6();
						break;
					case "7":
						RunTask7();
						break;
					case "8":
						RunTask8();
						break;
					case "9":
						RunTask9();
						break;
					case "10":
						RunTask10();
						break;
					case "0":
						return;
					default:
						Console.WriteLine("Неверный выбор!");
						break;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Ошибка: {ex.Message}");
			}

			Console.WriteLine("\nНажмите любую клавишу...");
			Console.ReadKey();
		}
	}

	// Методы для демонстрации заданий
	private static void RunTask1()
	{
		Console.Write("Количество чисел: ");
		int count = int.Parse(Console.ReadLine());
		FillFileWithUserInput("task1.txt", count);
		Console.WriteLine($"Результат: {Task1("task1.txt")}");
	}

	private static void RunTask2()
	{
		Console.Write("Количество строк: ");
		int lines = int.Parse(Console.ReadLine());
		FillMultilineFileWithUserInput("task2.txt", lines);
		Console.WriteLine($"Минимум: {Task2("task2.txt")}");
	}

	private static void RunTask3()
	{
		string input = "task_3_1.txt";   // Фиксированное имя входного файла
		string output = "task_3_2.txt";  // Фиксированное имя выходного файла

		Console.Write("Введите начальный символ: ");
		char c = Console.ReadKey().KeyChar;
		Console.WriteLine();

		Task3(input, output, c);
		Console.WriteLine($"Результат сохранен в {output}");
	}

	private static void RunTask4()
	{
		Console.Write("Делитель m: ");
		int m = int.Parse(Console.ReadLine());
		Console.Write("Неделимое n: ");
		int n = int.Parse(Console.ReadLine());

		// Создаем входной файл
		FillBinaryFileWithUserInput("binary_in.bin", 5);

		// Выполняем обработку
		Task4("binary_in.bin", "binary_out.bin", m, n);

		// Читаем и выводим результат
		byte[] result = File.ReadAllBytes("binary_out.bin");
		Console.WriteLine($"Результат ({result.Length} элементов):");

		if (result.Length == 0)
		{
			Console.WriteLine("Нет подходящих элементов");
		}
		else
		{
			foreach (byte b in result)
			{
				Console.WriteLine($"- {b}");
			}
		}
	}

	private static void RunTask5()
	{
		string filename = "passengers.xml";

		// Заполнение файла тестовыми данными
		FillPassengersFile(filename);
		Console.WriteLine($"Файл {filename} создан");

		// Анализ данных
		var (overTwo, overAverage) = AnalyzeLuggage(filename);

		// Вывод результатов
		Console.WriteLine($"Пассажиров с >2 единиц багажа: {overTwo}");
		Console.WriteLine($"Пассажиров с багажом выше среднего: {overAverage}");
	}

	private static void RunTask6()
	{
		List<int> l1 = new List<int> { 1, 3, 5, 7 };
		List<int> l2 = new List<int> { 2, 4, 6, 8 };
		var merged = Task6(l1, l2);
		Console.WriteLine($"Слияние списков: {string.Join(", ", merged)}");
	}

	private static void RunTask7()
	{
		List<int> list = new List<int> { 1, 3, 3, 5, 3, 7 };
		int count = Task7(list);
		Console.WriteLine($"Элементов с равными соседями: {count}");
	}

	private static void RunTask8()
	{
		List<Dish> menu = new List<Dish>
	{
		new Dish { Name = "Суп" },
		new Dish { Name = "Салат" },
		new Dish { Name = "Десерт" }
	};

		List<(string, List<string>)> orders = new List<(string, List<string>)>
	{
		("Иван", new List<string> { "Суп", "Салат" }),
		("Мария", new List<string> { "Суп", "Десерт" })
	};

		Task8(menu, orders);
		foreach (var dish in menu)
		{
			Console.WriteLine($"{dish.Name}: заказывали {dish.OrderedBy.Count} посетителей");
		}
	}

	private static void RunTask9()
	{
		string file = "tasc_9.txt"; // Фиксированное имя файла
		var consonants = Task9(file);
		Console.WriteLine($"Согласные, встречающиеся в одном слове: {string.Join(", ", consonants)}");
	}

	private static void RunTask10()
	{
		var leaders = Task10();
		Console.WriteLine("Топ участники (баллы):");
		foreach (var entry in leaders)
			Console.WriteLine(entry);
	}
}