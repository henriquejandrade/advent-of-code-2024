namespace advent_of_code_2024
{
    public class Day2
    {
        public static void Run()
        {
            string input = File.ReadAllText(@"Inputs/Day2.txt");

            IEnumerable<IEnumerable<int>> reports = input
                .Split('\n')
                .Select(line => line.Trim().Split(' '))
                .Select(levels => levels.Select(level => int.Parse(level)));

            /*
             * PART 1
             */
            IEnumerable<bool> checks = reports.Select(levels => Verify(levels));

            Console.WriteLine("- Day 2");
            Console.WriteLine("Part 1:");
            Console.WriteLine($"Report count: {checks.Count()}");
            Console.WriteLine($"Total safe: {checks.Count(check => check)}");


            /*
             * PART 2
             */
            IEnumerable<bool> allReports = reports
                .Select(levels => Enumerable.Range(0, levels.Count())
                    .Select(j => levels.Where((level, i) => i != j))
                    .Any(level => Verify(level)));

            Console.WriteLine();
            Console.WriteLine("Part 2:");
            Console.WriteLine($"Report count: {allReports.Count()}");
            Console.WriteLine($"Total safe: {allReports.Count(check => check)}");
        }

        private static bool Verify(IEnumerable<int> list)
        {
            return (Enumerable.SequenceEqual(list.OrderBy(item => item), list) ||
                    Enumerable.SequenceEqual(list.OrderByDescending(item => item), list)) &&
                    list.Zip(list.Skip(1), (a, b) => Math.Abs(a - b) >= 1 && Math.Abs(a - b) <= 3).All(item => item);
        }
    }
}
