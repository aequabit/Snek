namespace Snek.Core
{
    public class Board
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="width">Width of the board.</param>
        /// <param name="height">Height of the board.</param>
        public Board(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        ///     Width of the board.
        /// </summary>
        public int Width { get; }

        /// <summary>
        ///     Height of the board.
        /// </summary>
        public int Height { get; }
    }
}