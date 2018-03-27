using Listard;
using Snek.Rendering;
using System;
using System.IO.Ports;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using Snek.Entities;

namespace Snek.Core
{
    public class Game
    {
        /// <inheritdoc cref="_entities"/>
        public Listard<IEntity> Entities
        {
            get => _entities;
        }

        /// <inheritdoc cref="_size"/>
        public Size Size
        {
            get => _size;
        }

        /// <inheritdoc cref="_paused"/>
        public bool Paused
        {
            get => _paused;
        }

        /// <summary>
        /// The renderer instance.
        /// </summary>
        private Renderer _renderer = new Renderer();

        /// <summary>
        /// List of entities in the game.
        /// </summary>
        private Listard<IEntity> _entities = new Listard<IEntity>();

        /// <summary>
        /// Game logic worker.
        /// </summary>
        private Worker _gameWorker;

        /// <summary>
        /// Input worker.
        /// </summary>
        private Worker _inputWorker;

        /// <summary>
        /// Size of the game.
        /// </summary>
        private Size _size;

        /// <summary>
        /// Game pause state.
        /// </summary>
        private bool _paused;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Game(int width, int height)
        {
            _size = new Size(width, height);
            _gameWorker = new Worker(_gameLoop);
            _inputWorker = new Worker(_inputLoop);

            var snake = new Snake(new Location() {X = _size.Width / 2, Y = _size.Height / 2}, Direction.Right, 5,
                _size);
            snake.OnEntityCollision += snake_OnEntityCollision;

            _entities.Add(snake);
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        public void Start()
        {
            _gameWorker.Start();
            _inputWorker.Start();
        }

        /// <summary>
        /// Pauses the game.
        /// </summary>
        public void Pause()
        {
            _paused = true;
            _gameWorker.Pause();
        }

        /// <summary>
        /// Resumes the game.
        /// </summary>
        public void Resume()
        {
            _paused = false;
            _gameWorker.Resume();
        }

        /// <summary>
        /// Stops the game with an exit message.
        /// </summary>
        /// <param name="message">Exit message.</param>
        public void Stop(string message)
        {
            _gameWorker.Stop();
            _inputWorker.Stop();
            
            Console.Clear();
            Console.WriteLine(message);
        }

        /// <summary>
        /// Game logic loop.
        /// </summary>
        private void _gameLoop()
        {
            if (Console.BufferWidth - 1 != _size.Width || Console.BufferHeight - 1 != _size.Height)
                throw new Exception("Game window too small"); // TODO: custom exception

            foreach (var entity in _entities)
            {
                entity.Update();

                if (entity is IRenderable renderable)
                    _renderer.Render(renderable);
            }
        }

        /// <summary>
        /// Game input loop.
        /// </summary>
        private void _inputLoop()
        {
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Spacebar)
            {
                if (_paused)
                    Resume();
                else
                    Pause();
            }

            // TODO: improve
            var entities = _entities.Where(e => e.GetType() == typeof(Snake));
            if (entities.Any() && !_paused)
                (entities.First() as Snake).SendInput(key);
        }

        private void snake_OnEntityCollision(IEntity entity, IEntity collided)
        {
            Stop("Collision");
        }
    }
}