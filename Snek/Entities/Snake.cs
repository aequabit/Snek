using Listard;
using System;
using Snek.Core;
using Snek.Rendering;
using Snek.Types;

namespace Snek.Entities
{
    public class Snake : IEntity, IRenderable
    {
        /// <inheritdoc cref="Direction"/>
        public Direction Direction { get; private set; }

        /// <inheritdoc cref="Length"/>
        public int Length
        {
            get => _length;
            set
            {
                if (_positions.Count() > 1)
                    _length = value;
            }
        }

        /// <inheritdoc cref="Game"/>
        public Game Game { get; }

        /// <inheritdoc cref="_cycleDelay"/>
        public int CycleDelay
        {
            get => _cycleDelay;
            set => _cycleDelay = value;
        }

        /// <inheritdoc cref="IEntity.Positions"/>
        public Listard<Position> Positions() => _positions;

        /// <summary>
        /// Entity collision event handler delegate.
        /// </summary>
        /// <param name="entity">Entity that collided with another entity.</param>
        /// <param name="collided">Entity <c>entity</c> collided with.</param>
        public delegate void EntityCollisionHandler(IEntity entity, IEntity collided);

        /// <summary>
        /// Entity collision event.
        /// </summary>
        public event EntityCollisionHandler OnEntityCollision;

        /// <summary>
        /// The snake's positions.
        /// </summary>
        private readonly Listard<Position> _positions = new Listard<Position>();

        /// <summary>
        /// Timestamp of the last cycle.
        /// </summary>
        private long _lastCycle = Helper.UnixTime();

        /// <summary>
        /// Delay between cycles.
        /// </summary>
        private int _cycleDelay = 250;

        /// <summary>
        /// Length of the snake.
        /// </summary>
        private int _length = 1;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="position">Initial position of the snake.</param>
        /// <param name="direction">Initial direction of the snake.</param>
        /// <param name="length">Initial length of the snake.</param>
        /// <param name="game">Game the snake is in.</param>
        public Snake(Position position, Direction direction, int length, Game game)
        {
            _positions.Add(position);
            Direction = direction;
            Length = length;
            Game = game;
        }

        /// <inheritdoc cref="IEntity.Update"/>
        public void Update()
        {
            // TODO: Improve vertical/horizontal scaling
            // Increase the cycle delay by 25% if the snake is moving vertically
            var cycleDelay = Controls.IsVertical(Direction)
                ? _cycleDelay * 1.25
                : _cycleDelay;

            // Enforce the delay between update cycles
            if (Helper.UnixTime() - _lastCycle <= cycleDelay) return;
            _lastCycle = Helper.UnixTime();

            // Copy the last position to modify it for the next tick
            var newPosition = _positions.Last();

            // Detemine the new position of the snake
            switch (Direction)
            {
                case Direction.Left:
                    newPosition.X--;
                    break;
                case Direction.Right:
                    newPosition.X++;
                    break;
                case Direction.Up:
                    newPosition.Y--;
                    break;
                case Direction.Down:
                    newPosition.Y++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // TODO: simplify

            // Reposition the snake if it left the board horizontally
            if (newPosition.X < 0)
                newPosition.X = newPosition.X + Game.Size.Width + 1;
            else if (newPosition.X > Game.Size.Width)
                newPosition.X = newPosition.X - Game.Size.Width - 1;

            // TODO: Automatically limit bounds of UI
            // Reposition the snake if it left the board verticall
            //if (newPosition.Y < 0) newPosition.Y = newPosition.Y + Game.Size.Height + 1;
            if (newPosition.Y < 1) newPosition.Y = newPosition.Y + Game.Size.Height;
            else if (newPosition.Y > Game.Size.Height)
                //    newPosition.Y = newPosition.Y - Game.Size.Height - 1;
                newPosition.Y = newPosition.Y - Game.Size.Height;

            // Trim the snake to it's length
            if (_positions.Count() > _length)
                for (var i = _positions.Count() - Length - 1; i >= 0; i--)
                    _positions.RemoveAt(i);

            // Snake collided with itself
            if (_positions.Any(l => l.X == newPosition.X && l.Y == newPosition.Y))
                OnEntityCollision?.Invoke(this, this);

            _positions.Add(newPosition);

            // Snake collided with another entity
            var entity = Game.EntityAt(newPosition);
            if (entity != null && !(entity is Snake))
            {
                OnEntityCollision?.Invoke(this, entity);
            }
        }

        /// <summary>
        /// Checks if the snake is at a position.
        /// </summary>
        /// <param name="position">Position to check.</param>
        /// <returns>True if the snake is at the position.</returns>
        public bool AtPosition(Position position)
        {
            return _positions.Any(e => e == position);
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

            if (!Controls.ValidMove(Direction, direction))
                return;

            Direction = direction;
        }

        /// <inheritdoc cref="IRenderable.RenderMap"/>
        public RenderMap RenderMap(bool compatibility = false)
        {
            var map = new RenderMap();

            foreach (var position in _positions)
                map.Add(position, compatibility ? '#' : '█');

// Concept of different heads depending on the moving direction
//            var last = _locations.Last();
//            foreach (var location in _locations)
//            {
//                map.Add(location, compatibility ? '#' : '█');
//                if (location.X == last.X && location.Y == last.Y)
//                    switch (_direction)
//                    {
//                        case Direction.Down:
//                            map.Add(location, '▼');
//                            break;
//                        case Direction.Left:
//                            map.Add(location, '◀');
//                            break;
//                        case Direction.Right:
//                            map.Add(location, '▶');
//                            break;
//                        case Direction.Up:
//                            map.Add(location, '▲');
//                            break;
//                    }
//                else
//                    map.Add(location, '█');
//            }

            return map;
        }
    }
}