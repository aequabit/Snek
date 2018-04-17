namespace Snek.Types
{
    public struct Location
    {
        public int X;

        public int Y;

        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Location l, Location r)
        {
            return l.X == r.X && l.Y == l.Y;
        }

        public static bool operator !=(Location l, Location r)
        {
            return !(l == r);
        }
    }
}