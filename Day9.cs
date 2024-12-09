namespace advent_of_code_2024
{
    public static class Day9
    {
        public static void Run()
        {
            char[] input = File.ReadAllText(@"Inputs/Day9.txt").ToCharArray();
            string[] longInput = input
                .SelectMany((c, i) =>
                    (i % 2 == 0) ?
                        Repeat($"{i / 2}", int.Parse($"{c}")) :
                        Repeat(".", int.Parse($"{c}")))
                .ToArray();

            for (int i = 0; i < longInput.Length; i++)
            {
                if (longInput[i] == ".")
                {
                    for (int j = longInput.Length - 1; j > i; j--)
                    {
                        if (longInput[j] != ".")
                        {
                            longInput[i] = longInput[j];
                            longInput[j] = ".";
                            break;
                        }
                    }
                }
            }

            long sum = 0;
            for (int i = 0; i < longInput.Length; i++)
            {
                if (longInput[i] == ".") continue;

                sum += long.Parse(longInput[i]) * i;
            }

            Console.WriteLine(sum);
        }

        public static string[] Repeat(string c, int times)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < times; i++)
            {
                result.Add(c);
            }
            return result.ToArray();
        }
    }
}
