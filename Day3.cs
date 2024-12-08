using System.Text.RegularExpressions;

namespace advent_of_code_2024
{
    internal class Day3
    {
        public static void Run()
        {
            string input = File.ReadAllText(@"Inputs/Day3.txt");

            /*
             * PART 1
             */
            int sumPart1 = Regex.Matches(input.Replace(" ", ""), @"mul\([0-9]+,[0-9]+\)")
                .Select(match => match.Value.Split(','))
                .Sum(match => int.Parse(Regex.Match(match[0], @"[0-9]+").Value) * int.Parse(Regex.Match(match[1], @"[0-9]+").Value));

            Console.WriteLine("- Day 3");
            Console.WriteLine("Part 1:");
            Console.WriteLine($"Sum: {sumPart1}");

            /*
             * PART 2
             */
            IEnumerable<Match> matches = Regex.Matches(input.Replace(" ", ""), @"mul\([0-9]+,[0-9]+\)|do\(\)|don't\(\)");
            bool enabled = true;
            long sumPart2 = 0;
            foreach (Match match in matches)
            {
                switch (match.Value)
                {
                    case "do()": enabled = true; break;
                    case "don't()": enabled = false; break;
                    default:
                        if (enabled)
                        {
                            sumPart2 += match.Value.Split(',')
                                .Select(part => int.Parse(Regex.Match(part, @"[0-9]+").Value))
                                .Aggregate((a, b) => a * b);
                        }
                        break;
                }
            }

            Console.WriteLine("Part 2:");
            Console.WriteLine($"Sum: {sumPart2}");
        }
    }
}
