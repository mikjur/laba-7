public class Task6 : ILabTask
{
    
    public void Execute()
    {
        Console.WriteLine("Введите элементы первого списка через пробел :");
        var l1 = ParseInputToDoubleList(Console.ReadLine());

        Console.WriteLine("Введите элементы второго списка через пробел :");
        var l2 = ParseInputToDoubleList(Console.ReadLine());

        l1.Sort();
        l2.Sort();

        Task6InsertInPlace(l1, l2);

        Console.WriteLine($"Результат: {string.Join(": ", l1)}");
    }

    private List<double> ParseInputToDoubleList(string input)
    {
        var list = new List<double>();
        if (!string.IsNullOrWhiteSpace(input))
        {
            var parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                if (double.TryParse(part, out double num))
                    list.Add(num);
                else
                    Console.WriteLine($"\"{part}\" не является числом и пропущено.");
            }
        }
        return list;
    }

    private void Task6InsertInPlace(List<double> L1, List<double> L2)
    {
        int i = 0, j = 0;
        var result = new List<double>();

        while (i < L1.Count && j < L2.Count)
        {
            if (L1[i] <= L2[j])
                result.Add(L1[i++]);
            else
                result.Add(L2[j++]);
        }
        while (i < L1.Count) result.Add(L1[i++]);
        while (j < L2.Count) result.Add(L2[j++]);

        L1.Clear();
        L1.AddRange(result);
    }
}
