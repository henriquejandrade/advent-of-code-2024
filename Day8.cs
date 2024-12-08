namespace advent_of_code_2024
{
    public static class Day8
    {
        public static void Run()
        {
            string input = File.ReadAllText(@"Inputs/Day8.txt");
            char[][] map = input.Split('\n').Select(line => line.Replace("\r", "").ToCharArray()).ToArray();
            char[] antennas = input.ToCharArray().Distinct().Where(c => c != '.' && c != '\n').ToArray();

            Dictionary<Position, bool> resultsPart1 = new Dictionary<Position, bool>();
            Dictionary<Position, bool> resultsPart2 = new Dictionary<Position, bool>();
            foreach (char antenna in antennas)
            {
                List<Position> antennaPositions = new List<Position>();
                for (int y = 0; y < map.Length; y++)
                {
                    for (int x = 0; x < map[0].Length; x++)
                    {
                        if (map[y][x] == antenna)
                        {
                            antennaPositions.Add(new(x, y));
                        }
                    }
                }

                if (antennaPositions.Count > 1)
                {
                    for (int i = 0; i < antennaPositions.Count - 1; i++)
                    {
                        for (int j = i + 1; j < antennaPositions.Count; j++)
                        {
                            Position distance = new Position(
                                antennaPositions[j].X - antennaPositions[i].X,
                                antennaPositions[j].Y - antennaPositions[i].Y);

                            /*
                             * PART 1
                             */
                            Position antinodeA = new(antennaPositions[i].X - distance.X, antennaPositions[i].Y - distance.Y);
                            Position antinodeB = new(antennaPositions[j].X + distance.X, antennaPositions[j].Y + distance.Y);

                            if (Utils.IsWithinBounds(map, antinodeA)) resultsPart1.TryAdd(antinodeA, true);
                            if (Utils.IsWithinBounds(map, antinodeB)) resultsPart1.TryAdd(antinodeB, true);

                            /*
                             * PART 2
                             */
                            Position spreadA = antinodeA;
                            while (Utils.IsWithinBounds(map, spreadA))
                            {
                                resultsPart2.TryAdd(spreadA, true);
                                spreadA = new(spreadA.X - distance.X, spreadA.Y - distance.Y);
                            }

                            Position spreadB = antinodeB;
                            while (Utils.IsWithinBounds(map, spreadB))
                            {
                                resultsPart2.TryAdd(spreadB, true);
                                spreadB = new(spreadB.X + distance.X, spreadB.Y + distance.Y);
                            }

                            resultsPart2.TryAdd(antennaPositions[j], true);
                        }

                        resultsPart2.TryAdd(antennaPositions[i], true);
                    }
                }
            }

            Console.WriteLine("- Day 8:");
            Console.WriteLine($"Part 1: {resultsPart1.Count}");
            Console.WriteLine($"Part 2: {resultsPart2.Count}");
        }
    }
}
