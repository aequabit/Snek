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
            _gameWorker = new Worker(GameLoop);
            _inputWorker = new Worker(InputLoop);

            var location = new Location(
                _random.Next(0, _size.Width - 1),
                _random.Next(0, _size.Height - 1)
            );
            var snake = new Snake(location, Direction.Right, 5, this);
            snake.OnEntityCollision += (entity, collided) =>
            {
                switch (collided)
                {
                    case Snake _:
                        Stop("You ran into yourself");
                        break;
                    case Food _:
                    {
                        Snake.Length++;

                        _entities.Remove(collided);

                        if (Snake.CycleDelay >= 60)
                            Snake.CycleDelay -= 10;
                    }
                        break;
                }
            };

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
            Console.Clear();
            Console.WriteLine(message);

            // TODO: Shutdown workers properly
            Console.ReadKey();
            Environment.Exit(1);
        }

        /// <summary>
        /// Spawns food at a random location.
        /// </summary>
        public void SpawnFood()
        {
            if (Snake == null || _paused) return;

            // Get a random location to spawn the food at
            var location = new Location(
                _random.Next(0, _size.Width - 1),
                _random.Next(0, _size.Height - 1)
            );

            // Spawn the food if there is no other entity at the location
            if (EntityAt(location) == null)
                _entities.Add(new Food(location));
        }

        /// <summary>
        /// Searches for an entity at a given location.
        /// </summary>
        /// <param name="location">Location to search at.</param>
        /// <returns>The entity or null if no entity was found at the location.</returns>
        public IEntity EntityAt(Location location)
        {
            foreach (var entity in _entities)
            {
                foreach (var entityLocation in entity.Locations())
                {
                    if (entityLocation != location) continue;

                    return entity;
                }
            }

            return null;
        }

        /// <summary>
        /// Game logic loop.
        /// </summary>
        private void GameLoop()
        {
            // Exit the game if the console buffer is smaller than the game size
            if (Console.BufferWidth - 1 < _size.Width || Console.BufferHeight - 1 < _size.Height)
                throw new Exception("Game window too small");

            // Spawn food with a random delay
            if (_foodSpawned == default(DateTime) ||
                DateTime.Now - _foodSpawned > TimeSpan.FromSeconds(_random.Next(2, 8)))
            {
                SpawnFood();
                _foodSpawned = DateTime.Now;
            }

            foreach (var entity in _entities)
            {
                // Update the entity
                entity.Update();

                // Render the entity if it implements IRenderable
                if (entity is IRenderable renderable)
                    _renderer.Render(renderable);
            }
        }

        /// <summary>
        /// Game input loop.
        /// </summary>
        private void InputLoop()
        {
            // TODO: Proper key bindings

            var key = Console.ReadKey(true);

            // Register the pause toggle
            if (key.Key == ConsoleKey.Spacebar)
                if (_paused)
                    Resume();
                else
                    Pause();

            // Drop input if the game is paused
            if (_paused) return;

            // Game controls
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

            // Redirect all other input to the snake's input handler
            Snake?.SendInput(key);
        }
    }
}