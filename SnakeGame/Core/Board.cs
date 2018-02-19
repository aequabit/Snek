namespace SnakeGame.Core
{
    public class Board
    {
        /// <summary>
        /// Width of the board.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Height of the board.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="width">Width of the board.</param>
        /// <param name="height">Height of the board.</param>
        public Board(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}