namespace Snek.Types
{
    public struct Position
    {
        public int X;

        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator == (Position l, Position r)
        {
            return l.X == r.X && l.Y == r.Y;
        }

        public static bool operator != (Position l, Position r)
        {
            return !(l == r);
        }
    }
}