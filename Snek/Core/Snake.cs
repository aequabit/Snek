using System;
using Listard;
using Snek.Entities;

namespace Snek.Core
{
    public class Snake : IEntity
    {
        /// <summary>
        ///     The snake's location.
        /// </summary>
        private readonly Location _location;

        /// <summary>
        ///     Waypoints of the snake.
        /// </summary>
        private readonly Listard<Waypoint> _waypoints;

        /// <summary>
        ///     Length of the snake;
        /// </summary>
        private int _length;

        /// <summary>
        ///     Constructor.
        /// </summary>
        public Snake()
        {
            _location = new Location {X = 10, Y = 10};
            _waypoints = new Listard<Waypoint>();
        }

        /// <summary>
        ///     Gets the snake's location.
        /// </summary>
        /// <returns>The snake's location.</returns>
        public Location GetLocation()
        {
            return _location;
        }

        /// <summary>
        ///     Gets the snake's waypoints.
        /// </summary>
        /// <returns>The snake's waypoints.</returns>
        public Listard<Waypoint> GetWaypoints()
        {
            return _waypoints;
        }

        /// <summary>
        ///     Increases the snake's length by <c>length</c>.
        /// </summary>
        /// <param name="length"></param>
        public void Grow(int length = 1)
        {
            _length += length;
            
            throw new NotImplementedException("Snake->Grow(int): Not implemented");
        }
    }
}