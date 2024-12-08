namespace advent_of_code_2024
{
    internal class Day6
    {
        public static void Run()
        {
            char[][] map = File.ReadAllText(@"Inputs/Day6.txt")
                .Split('\n')
                .Select(line => line.Replace("\r", "").ToCharArray())
                .ToArray();

            // Initial position
            char[] initialLine = map.First(line => line.Contains('^'));
            Position initialPosition = new(Array.IndexOf(initialLine, '^'), Array.IndexOf(map, initialLine));

            /*
             * PART 1
             */
            Tuple<bool, List<Position>> simulation1 = Play(map, initialPosition, 0);

            Console.WriteLine("- Day 6:");
            Console.WriteLine("Part 1:");
            Console.WriteLine($"Unique visited positions: {simulation1.Item2.Distinct().Count()}");

            /*
             * PART 2
             */
            List<Position> visited = simulation1.Item2.Distinct().ToList();
            int loops = 0;
            foreach (Position visit in visited.Skip(1)) // avoid initial position
            {
                char[][] simMap = map.Select(c => c.ToArray()).ToArray();
                simMap[visit.Y][visit.X] = '#';
                Tuple<bool, List<Position>> simResult = Play(simMap, initialPosition, 0);
                if (simResult.Item1) loops++;
            }

            Console.WriteLine();
            Console.WriteLine("Part 2:");
            Console.WriteLine($"Possible loops count: {loops}");
        }

        public static Tuple<bool, List<Position>> Play(char[][] map, Position position, int orientation)
        {
            List<Tuple<Position, int>> visited = new() { new(position, orientation) };
            Position guard = position;

            bool loop = false;
            while (true)
            {
                Position next = Next(guard, orientation);

                if (!Utils.IsWithinBounds(map, next)) break;

                if (map[next.Y][next.X] == '#') orientation = orientation >= 3 ? 0 : orientation + 1;
                else guard = next;

                if (visited.Any(visit => visit.Item1.X == guard.X && visit.Item1.Y == guard.Y && visit.Item2 == orientation))
                {
                    loop = true;
                    break;
                }

                visited.Add(new(guard, orientation));
            }

            return new(loop, visited.Select(visit => visit.Item1).ToList());
        }

        private static Position Next(Position position, int orientation)
        {
            switch (orientation)
            {
                case 0: return new(position.X, position.Y - 1);
                case 1: return new(position.X + 1, position.Y);
                case 2: return new(position.X, position.Y + 1);
                case 3: default: return new(position.X - 1, position.Y);
            }
        }
    }
}
