using Listard;
using Snek.Entities;
using Snek.Rendering;
using Snek.Types;
using System;
using System.Linq;

namespace Snek.Core
{
    public class Game
    {
        /// <inheritdoc cref="_entities"/>
        public Listard<IEntity> Entities => _entities;

        /// <inheritdoc cref="_size"/>
        public Size Size => _size;

        /// <inheritdoc cref="_paused"/>
        public bool Paused => _paused;

        /// <summary>
        /// Gets the snake from the entity list.
        /// </summary>
        public Snake Snake
        {
            get
            {
                var entities = _entities.Where(e => e.GetType() == typeof(Snake));
                return entities.Any() ? entities.First() as Snake : null;
            }
        }

        /// <summary>
        /// The renderer instance.
        /// </summary>
        private readonly Renderer _renderer = new Renderer();

        /// <summary>
        /// List of entities in the game.
        /// </summary>
        private readonly Listard<IEntity> _entities = new Listard<IEntity>();

        /// <summary>
        /// Game logic worker.
        /// </summary>
        private readonly Worker _gameWorker;

        /// <summary>
        /// Input worker.
        /// </summary>
        private readonly Worker _inputWorker;

        /// <summary>
        /// Size of the game.
        /// </summary>
        private readonly Size _size;

        /// <summary>
        /// Game pause state.
        /// </summary>
        private bool _paused;

        /// <summary>
        /// Time the last food was spawned at.
        /// </summary>
        private DateTime _foodSpawned;

        /// <summary>
        /// Random generator.
        /// </summary>
        private readonly Random _random = new Random();

        /// <summary>
        /// Constructor.
        /// </summary>
        public Game(int width, int height)
        {
            _size = new Size(width, height);
            _gameWorker = new Worker(_gameLoop);
            _inputWorker = new Worker(_inputLoop);

            var snake = new Snake(new Location {X = _size.Width / 2, Y = _size.Height / 2}, Direction.Right, 5, this);
            snake.OnEntityCollision += snake_OnEntityCollision;

            _entities.Add(snake);
            _entities.Add(new StatusBar(this));
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
            // TODO: shutdown workers gracefully

            Console.Clear();
            Console.WriteLine(message);

            // TODO: proper shutdown
            Console.ReadKey();
            Environment.Exit(1);
        }

        public void SpawnFood()
        {
            var location = new Location
            {
                X = _random.Next(0, _size.Width - 1),
                Y = _random.Next(0, _size.Height - 1)
            };

            var entities = _entities.Where(e => e.GetType() == typeof(Snake));

            if (!entities.Any() || _paused) return;

            if (Snake == null || !Snake.GetRenderMap().HasLocation(location))
                _entities.Add(new Food(location));
        }

        /// <summary>
        /// Game logic loop.
        /// </summary>
        private void _gameLoop()
        {
            if (Console.BufferWidth - 1 < _size.Width || Console.BufferHeight - 1 < _size.Height)
                throw new Exception("Game window too small"); // TODO: custom exception

            if (_foodSpawned == default(DateTime) ||
                DateTime.Now - _foodSpawned > TimeSpan.FromSeconds(_random.Next(4, 12)))
            {
                SpawnFood();
                _foodSpawned = DateTime.Now;
            }

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

            if (_paused) return;

            switch (key.Key)
            {
                case ConsoleKey.D1:
                    Snake.CycleDelay -= 10;
                    return;
                case ConsoleKey.D2:
                    Snake.CycleDelay += 10;
                    return;
                case ConsoleKey.D3:
                    Snake.Length -= 1;
                    return;
                case ConsoleKey.D4:
                    Snake.Length += 1;
                    return;
                case ConsoleKey.D5:
                    _renderer.Compatibility = !_renderer.Compatibility;
                    return;
            }

            Snake?.SendInput(key);
        }

        private void snake_OnEntityCollision(IEntity entity, IEntity collided)
        {
            switch (collided)
            {
                case Snake _:
                    Stop("You ran into yourself");
                    break;
                case Food _:
                {
                    Snake.Length++;

                    _entities.RemoveAt(_entities.ToList().IndexOf(collided));

                    if (Snake.CycleDelay >= 60)
                        Snake.CycleDelay -= 10;
                }
                    break;
            }
        }
    }
}