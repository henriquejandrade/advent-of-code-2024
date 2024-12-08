namespace advent_of_code_2024
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position() { }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object? obj) => (X == ((Position)obj).X) && (Y == ((Position)obj).Y);

        public override int GetHashCode() => HashCode.Combine(X, Y);
    }

    public static class Utils
    {
        public static bool IsWithinBounds(char[][] map, Position position) => position.X >= 0 && position.X < map.Length && position.Y >= 0 && position.Y < map[0].Length;
    }
}
