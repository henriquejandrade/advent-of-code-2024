namespace advent_of_code_2024
{
    public static class Day5
    {
        public static void Run()
        {
            IEnumerable<int[]> parsedRules = File.ReadAllText(@"Inputs/Day5_Rules.txt")
                .Split('\n')
                .Select(rule => rule.Replace("\r", "").Split("|"))
                .Select(rule => new int[2] { int.Parse(rule[0]), int.Parse(rule[1]) });

            List<int[]> parsedInput = File.ReadAllText(@"Inputs/Day5_Pages.txt")
                .Split("\n")
                .Select(page => page.Replace("\r", "").Split(","))
                .Select(values => values.Select(value => int.Parse(value)).ToArray())
                .ToList();

            Console.WriteLine("- Day 5");
            Console.WriteLine($"Total requests: {parsedInput.Count()}");
            Console.WriteLine();

            /*
             * PART 1
             */
            List<int[]> badRules = parsedInput
                .Where(input => input.Skip(1).Select((v, i) => parsedRules.Any(rule => rule[0] == v && rule[1] == input[i])).Any(bad => bad))
                .ToList();

            List<int[]> goodRules = parsedInput.Except(badRules).ToList();

            IEnumerable<int> middleValues = goodRules.Select(order => order[order.Length / 2]);

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Total correct requests: {goodRules.Count}");
            Console.WriteLine($"Sum of middle values: {middleValues.Sum()}");

            /*
             * PART 2
             */
            List<int[]> correctedRules = new List<int[]>();
            foreach (int[] badRule in badRules)
            {
                List<int> newOrder = new List<int>() { badRule[0] };
                for (int i = 1; i < badRule.Length; i++)
                {
                    bool ruleExists = false;
                    for (int j = 0; j < newOrder.Count; j++)
                    {
                        if (badRule[i] == newOrder[j]) continue;

                        if (parsedRules.Any(rule => rule[0] == badRule[i] && rule[1] == newOrder[j]))
                        {
                            ruleExists = true;
                            newOrder.Insert(j, badRule[i]);
                            break;
                        }
                    }

                    if (!ruleExists) newOrder.Add(badRule[i]);
                }

                correctedRules.Add(newOrder.ToArray());
            }

            IEnumerable<int> middleValuesCorrected = correctedRules.Select(order => order[(int)order.Length / 2]);

            Console.WriteLine();
            Console.WriteLine("Part 2:");
            Console.WriteLine($"Total bad requests: {badRules.Count()}");
            Console.WriteLine($"Sum of middle values: {middleValuesCorrected.Sum()}");
        }
    }
}
