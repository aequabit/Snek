using Listard;
using System;
using System.Linq;
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
        public int Length { get; set; }

        /// <inheritdoc cref="Game"/>
        public Game Game { get; }

        /// <inheritdoc cref="_cycleDelay"/>
        public int CycleDelay
        {
            get => _cycleDelay;
            set => _cycleDelay = value;
        }

        /// <inheritdoc cref="IEntity.Locations"/>
        public Listard<Location> Locations() => _locations;

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
        /// The snake's locations.
        /// </summary>
        private readonly Listard<Location> _locations = new Listard<Location>();

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
        /// <param name="game">Game the snake is in.</param>
        public Snake(Location location, Direction direction, int length, Game game)
        {
            _locations.Add(location);
            Direction = direction;
            Length = length;
            Game = game;
        }

        /// <inheritdoc cref="IEntity.Update"/>
        public void Update()
        {
            // TODO: improve
            // increase the cycle delay by 25% if moving vertically
            var cycleDelay = Controls.IsVertical(Direction)
                ? _cycleDelay * 1.25
                : _cycleDelay;

            if (Helper.UnixTime() - _lastCycle <= cycleDelay) return;

            _lastCycle = Helper.UnixTime();

            var newLocation = _locations.Last();

            switch (Direction)
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
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // TODO: simplify
            if (newLocation.X < 0)
                newLocation.X = newLocation.X + Game.Size.Width;
            else if (newLocation.X >= Game.Size.Width)
                newLocation.X = newLocation.X - Game.Size.Width;

            if (newLocation.Y < 0)
                newLocation.Y = newLocation.Y + Game.Size.Height;
            else if (newLocation.Y >= Game.Size.Height)
                newLocation.Y = newLocation.Y - Game.Size.Height;

            if (_locations.Count > Length)
                _locations.RemoveAt(0);

            if (_locations.Any(l => l.X == newLocation.X && l.Y == newLocation.Y) && OnEntityCollision != null)
                OnEntityCollision(this, this);
            else
                _locations.Add(newLocation);

            // TODO: improve
            var entity = Game.Entities.Where(e =>
                e.Locations().First().X == newLocation.X && e.Locations().First().Y == newLocation.Y && !(e is Snake));

            if (entity.Any()) OnEntityCollision(this, entity.First());
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

            if (!Controls.ValidDirection(Direction, direction))
                return;

            Direction = direction;
        }

        /// <inheritdoc cref="IRenderable.GetRenderMap"/>
        public RenderMap GetRenderMap(bool compatibility = false)
        {
            var map = new RenderMap();

            foreach (var location in _locations)
                map.Add(location, compatibility ? '#' : '█');

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