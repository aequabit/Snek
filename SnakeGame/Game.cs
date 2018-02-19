using System.Threading;
using SnakeGame.Core;
using SnakeGame.Entities;
using SnakeGame.Rendering;
using Listard;

namespace SnakeGame
{
    public class Game
    {
        /// <summary>
        /// Width of the game.
        /// </summary>
        private int Width;

        /// <summary>
        /// Height of the game.
        /// </summary>
        private int Height;

        /// <summary>
        /// Delay between game update cylces.
        /// </summary>
        private int CycleDelay;

        /// <summary>
        /// Game board.
        /// </summary>
        private Board Board;

        /// <summary>
        /// The snake.
        /// </summary>
        private Snake Snake;

        /// <summary>
        /// Entities in the game.
        /// </summary>
        private Listard<IEntity> Entities;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="width">Width of the game.</param>
        /// <param name="height">Height of the game.</param>
        public Game(int width, int height)
        {
            Width = width;
            Height = height;
            CycleDelay = 500;
        }

        /// <summary>
        /// Cosntructor.
        /// </summary>
        /// <param name="width">Width of the game.</param>
        /// <param name="height">Height of the game.</param>
        /// <param name="cycleDelay">Delay between game update cycles.</param>
        public Game(int width, int height, int cycleDelay)
        {
            Width = width;
            Height = height;
            CycleDelay = cycleDelay;
        }

        /// <summary>
        /// Entity collision event handler delegate.
        /// </summary>
        /// <param name="entity">Entity that collided with another entity.</param>
        /// <param name="collided">Entity <c>entity</c> collided with.</param>
        public delegate void EntityCollisionHandler(IEntity entity, IEntity collided);

        /// <summary>
        /// Entity collision event handler.
        /// </summary>
        public event EntityCollisionHandler OnEntityCollision;

        /// <summary>
        /// Border collision event handler delegate.
        /// </summary>
        /// <param name="entity">Entity that collided with the border.</param>
        /// <param name="x">X coordinate of the collision.</param>
        /// <param name="y">Y coordinate of the collision.</param>
        public delegate void BorderCollisionHandler(IEntity entity, int x, int y);

        /// <summary>
        /// Border collision event handler.
        /// </summary>
        public event BorderCollisionHandler OnBoarderCollision;

        /// <summary>
        /// Runs the game loop.
        /// </summary>
        public void Run()
        {
            Board = new Board(Width, Height);
            Snake = new Snake();

            while (true)
            {
                Cycle();
                Thread.Sleep(CycleDelay);
            }
        }

        /// <summary>
        /// Runs a game update cycle.
        /// </summary>
        private void Cycle()
        {
            Renderer.Clear();
            Renderer.Render(Board);
            Renderer.Render(Snake);
        }
    }
}