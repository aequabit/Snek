namespace Snek.Types
{
    public struct Size
    {
        public int Width;

        public int Height;

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static bool operator ==(Size l, Size r)
        {
            return l.Width == r.Width && l.Height == r.Height;
        }

        public static bool operator !=(Size l, Size r)
        {
            return !(l == r);
        }
    }
}