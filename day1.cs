namespace advent_of_code_2024
{
    public class Day1
    {
        public static void Run()
        {
            string input = File.ReadAllText(@"Inputs/Day1.txt");

            IEnumerable<int[]> parsed = input
                .Split('\n')
                .Select(line => line.Split("   ").Select(part => int.Parse(part)).ToArray());

            List<int> left = parsed
                .Select(line => line[0])
                .OrderBy(value => value).ToList();

            List<int> right = parsed
                .Select(line => line[1])
                .OrderBy(value => value).ToList();

            /*
             * PART 1
             */
            Console.WriteLine("- Day 1");
            Console.WriteLine($"Total distance: {left.Select((l, i) => Math.Abs(right[i] - l)).Sum()}");

            /*
             * PART 2
             */
            Console.WriteLine($"Similarity: {left.Sum(l => l * right.Count(r => r == l))}");
        }
    }
}
