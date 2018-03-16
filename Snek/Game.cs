using System.Threading;
using Listard;
using Snek.Core;
using Snek.Entities;
using Snek.Rendering;

namespace Snek
{
    public class Game
    {
        /// <summary>
        ///     Border collision event handler delegate.
        /// </summary>
        /// <param name="entity">Entity that collided with the border.</param>
        /// <param name="x">X coordinate of the collision.</param>
        /// <param name="y">Y coordinate of the collision.</param>
        public delegate void BorderCollisionHandler(IEntity entity, int x, int y);

        /// <summary>
        ///     Entity collision event handler delegate.
        /// </summary>
        /// <param name="entity">Entity that collided with another entity.</param>
        /// <param name="collided">Entity <c>entity</c> collided with.</param>
        public delegate void EntityCollisionHandler(IEntity entity, IEntity collided);

        /// <summary>
        ///     Game board.
        /// </summary>
        private Board _board;

        /// <summary>
        ///     Delay between game update cylces.
        /// </summary>
        private readonly int _cycleDelay;

        /// <summary>
        ///     Entities in the game.
        /// </summary>
        private Listard<IEntity> _entities;

        /// <summary>
        ///     Height of the game.
        /// </summary>
        private readonly int _height;

        /// <summary>
        ///     The snake.
        /// </summary>
        private Snake _snake;

        /// <summary>
        ///     Width of the game.
        /// </summary>
        private readonly int _width;

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="width">Width of the game.</param>
        /// <param name="height">Height of the game.</param>
        public Game(int width, int height)
        {
            _width = width;
            _height = height;
            _cycleDelay = 500;
        }

        /// <summary>
        ///     Cosntructor.
        /// </summary>
        /// <param name="width">Width of the game.</param>
        /// <param name="height">Height of the game.</param>
        /// <param name="cycleDelay">Delay between game update cycles.</param>
        public Game(int width, int height, int cycleDelay)
        {
            _width = width;
            _height = height;
            _cycleDelay = cycleDelay;
        }

        /// <summary>
        ///     Entity collision event handler.
        /// </summary>
        public event EntityCollisionHandler OnEntityCollision;

        /// <summary>
        ///     Border collision event handler.
        /// </summary>
        public event BorderCollisionHandler OnBoarderCollision;

        /// <summary>
        ///     Runs the game loop.
        /// </summary>
        public void Run()
        {
            _board = new Board(_width, _height);
            _snake = new Snake();

            while (true)
            {
                Cycle();
                Thread.Sleep(_cycleDelay);
            }
        }

        /// <summary>
        ///     Runs a game update cycle.
        /// </summary>
        private void Cycle()
        {
            Renderer.Clear();
            Renderer.Render(_board);
            Renderer.Render(_snake);
        }
    }
}