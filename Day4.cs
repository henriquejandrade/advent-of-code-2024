namespace advent_of_code_2024
{
    internal class Day4
    {
        public static void Run()
        {
            string input = File.ReadAllText(@"Inputs/Day4.txt");

            /* 
             * PART 1
             */
            List<char[,]> filtersPart1 = new List<char[,]>() {
                new char[,] { { 'X', 'M', 'A', 'S' } },
                new char[,]
                {
                    { 'X', ' ', ' ', ' ' },
                    { ' ', 'M', ' ', ' ' },
                    { ' ', ' ', 'A', ' ' },
                    { ' ', ' ', ' ', 'S' },
                },
                new char[,] { { 'X' }, { 'M' }, { 'A' }, { 'S' } },
                new char[,]
                {
                    { ' ', ' ', ' ', 'X' },
                    { ' ', ' ', 'M', ' ' },
                    { ' ', 'A', ' ', ' ' },
                    { 'S', ' ', ' ', ' ' },
                },
                new char[,] { { 'S', 'A', 'M', 'X' } },
                new char[,]
                {
                    { 'S', ' ', ' ', ' ' },
                    { ' ', 'A', ' ', ' ' },
                    { ' ', ' ', 'M', ' ' },
                    { ' ', ' ', ' ', 'X' },
                },
                new char[,] { { 'S' }, { 'A' }, { 'M' }, { 'X' } },
                new char[,]
                {
                    { ' ', ' ', ' ', 'S' },
                    { ' ', ' ', 'A', ' ' },
                    { ' ', 'M', ' ', ' ' },
                    { 'X', ' ', ' ', ' ' },
                }
            };

            char[][] matrix = input.Split('\n').Select(line => line.Replace("\r", "").ToCharArray()).ToArray();
            int countPart1 = filtersPart1.Sum(filter => CountFilter(matrix, filter));

            Console.WriteLine("- Day 4");
            Console.WriteLine("Part 1:");
            Console.WriteLine($"Count: {countPart1}");

            /* 
             * PART 2
             */
            List<char[,]> filtersPart2 = new List<char[,]>()
            {
                new char[,]
                {
                    { 'M', ' ', 'S' },
                    { ' ', 'A', ' ' },
                    { 'M', ' ', 'S' },
                },
                new char[,]
                {
                    { 'M', ' ', 'M' },
                    { ' ', 'A', ' ' },
                    { 'S', ' ', 'S' },
                },
                new char[,]
                {
                    { 'S', ' ', 'M' },
                    { ' ', 'A', ' ' },
                    { 'S', ' ', 'M' },
                },
                new char[,]
                {
                    { 'S', ' ', 'S' },
                    { ' ', 'A', ' ' },
                    { 'M', ' ', 'M' },
                }
            };

            int countPart2 = filtersPart2.Sum(filter => CountFilter(matrix, filter));

            Console.WriteLine();
            Console.WriteLine("Part 2:");
            Console.WriteLine($"Count: {countPart2}");
        }

        private static int CountFilter(char[][] input, char[,] filter)
        {
            int count = 0;
            for (int j = 0; j <= input.Length - filter.GetLength(0); j++)
            {
                for (int i = 0; i <= input[0].Length - filter.GetLength(1); i++)
                {
                    // Filter
                    bool matches = true;
                    for (int x = 0; x < filter.GetLength(0); x++)
                    {
                        for (int y = 0; y < filter.GetLength(1); y++)
                        {
                            if (j + x <= input.Length - filter.GetLength(0) + x && i + y <= input[0].Length - filter.GetLength(1) + y)
                            {
                                if (filter[x, y] == ' ') continue;

                                if (input[j + x][i + y] != filter[x, y])
                                {
                                    matches = false;
                                    break;
                                }
                            }
                            else
                            {
                                matches = false;
                            }
                        }

                        if (!matches) break;
                    }

                    if (matches)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
