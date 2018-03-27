using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Listard;
using Snek.Core;
using Snek.Rendering;

namespace Snek.Entities
{
    public class Snake : IEntity, IRenderable
    {
        /// <inheritdoc cref="_direction"/>
        public Direction Direction
        {
            get => _direction;
        }

        /// <inheritdoc cref="_length"/>
        public int Length
        {
            get => _length;
        }

        /// <inheritdoc cref="_gameSize"/>
        public Size GameSize
        {
            get => _gameSize;
        }

        /// <summary>
        /// Entity collision event handler delegate.
        /// </summary>
        /// <param name="entity">Entity that collided with another entity.</param>
        /// <param name="collided">Entity <c>entity</c> collided with.</param>
        public delegate void EntityCollisionHandler(IEntity entity, IEntity collided);

        /// <summary>
        /// Entity colision event.
        /// </summary>
        public event EntityCollisionHandler OnEntityCollision;

        /// <summary>
        /// The snake's locations.
        /// </summary>
        private Listard<Location> _locations = new Listard<Location>();

        /// <summary>
        /// The snake's direction.
        /// </summary>
        private Direction _direction;

        /// <summary>
        /// The snake's length.
        /// </summary>
        private int _length;

        /// <summary>
        /// Size of the game.
        /// </summary>
        private Size _gameSize;

        /// <summary>
        /// Timestamp of the last cycle.
        /// </summary>
        private long _lastCycle = Helper.UnixTime();

        /// <summary>
        /// Delay between cycles.
        /// </summary>
        private int _cycleDelay = 250;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="location">Initial location of the snake.</param>
        /// <param name="direction">Initial direction of the snake.</param>
        /// <param name="length">Initial length of the snake.</param>
        /// <param name="size">Size of the board.</param>
        public Snake(Location location, Direction direction, int length, Size gameSize)
        {
            _locations.Add(location);
            _direction = direction;
            _length = length;
            _gameSize = gameSize;
        }

        /// <summary>
        /// Updates the snakes's state.
        /// </summary>
        public void Update()
        {
            if (Helper.UnixTime() - _lastCycle <= _cycleDelay) return;

            _lastCycle = Helper.UnixTime();

            var newLocation = _locations.Last();

            switch (_direction)
            {
                case Direction.Left:
                    newLocation.X--;
                    break;
                case Direction.Right:
                    newLocation.X++;
                    break;
                case Direction.Up:
                    newLocation.Y--;
                    break;
                case Direction.Down:
                    newLocation.Y++;
                    break;
            }

            // TODO: simplify
            if (newLocation.X < 0)
                newLocation.X = newLocation.X + _gameSize.Width;
            else if (newLocation.X >= _gameSize.Width)
                newLocation.X = newLocation.X - _gameSize.Width;

            if (newLocation.Y < 0)
                newLocation.Y = newLocation.Y + _gameSize.Height;
            else if (newLocation.Y >= _gameSize.Height)
                newLocation.Y = newLocation.Y - _gameSize.Height;

            if (_locations.Count > _length)
                _locations.RemoveAt(0);

            if (_locations.Any(l => l.X == newLocation.X && l.Y == newLocation.Y) && OnEntityCollision != null)
                OnEntityCollision(this, this);
            else
                _locations.Add(newLocation);
        }

        /// <summary>
        /// Sends keyboard input to the snake entity.
        /// </summary>
        /// <param name="key">The key to send.</param>
        public void SendInput(ConsoleKeyInfo key)
        {
            if (!Controls.DirectionKey(key.Key))
                return;

            var direction = Controls.KeyToDirection[key.Key];

            if (!Controls.ValidDirection(_direction, direction))
                return;

            _direction = direction;
        }

        /// <inheritdoc cref="IRenderable.GetRenderMap"/>
        public RenderMap GetRenderMap()
        {
            var map = new RenderMap();

            foreach (var location in _locations)
                map.Add(location, '█');

            return map;
        }
    }
}