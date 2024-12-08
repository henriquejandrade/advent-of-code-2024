namespace advent_of_code_2024
{
    public class Tree
    {
        public long Value { get; set; }
        public Tree[] Children { get; set; }
    }

    public static class Day7
    {
        public static void Run()
        {
            string input = File.ReadAllText(@"Inputs/day7.txt");
            long[] results = input.Split('\n').Select(line => long.Parse(line.Split(':')[0])).ToArray();
            List<List<long>> factors = input.Split('\n').Select(line => line.Split(':')[1].Trim().Split(' ').Select(value => long.Parse(value)).ToList()).ToList();

            Tree tree = Make(factors[0][0], factors[0]);

            /*
             * PART 1
             */
            long sum = results.Where((result, i) => Search(result, Make(factors[i][0], factors[i]))).Sum();

            Console.WriteLine("- Day 7:");
            Console.WriteLine("Part 1:");
            Console.WriteLine($"Sum of finds: {sum}");

            /*
             * PART 2
             */
            sum = results.Where((result, i) => Search(result, Make(factors[i][0], factors[i], true))).Sum();

            Console.WriteLine();
            Console.WriteLine("Part 2:");
            Console.WriteLine($"Sum of finds: {sum}");
        }

        private static Tree Make(long value, List<long> values, bool hasConcat = false)
        {
            return values.Count == 1 ?
                new Tree()
                {
                    Value = value
                } :
                new Tree()
                {
                    Value = value,
                    Children = !hasConcat ?
                    new Tree[]
                    {
                        Make(value + values[1], values.Skip(1).ToList()),
                        Make(value * values[1], values.Skip(1).ToList()),
                    } :
                    new Tree[]
                    {
                        Make(value + values[1], values.Skip(1).ToList(),true),
                        Make(value * values[1], values.Skip(1).ToList(),true),
                        Make(long.Parse($"{value}{values[1]}"), values.Skip(1).ToList(), true),
                    }
                };
        }

        private static bool Search(long value, Tree tree)
        {
            if (tree.Children == null && tree.Value == value) return true;
            if (tree.Children == null) return false;
            return tree.Children.Any(leaf => Search(value, leaf));
        }
    }
}