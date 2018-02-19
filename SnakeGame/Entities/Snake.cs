using System;
using SnakeGame.Core;
using Listard;

namespace SnakeGame.Entities
{
    public class Snake : IEntity
    {
        /// <summary>
        /// The snake's location.
        /// </summary>
        private Location Location;

        /// <summary>
        /// Waypoints of the snake.
        /// </summary>
        private Listard<Waypoint> Waypoints;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Snake()
        {
            Location = new Location() { X = 10, Y = 10 };
            Waypoints = new Listard<Waypoint>();
        }

        /// <summary>
        /// Gets the snake's location.
        /// </summary>
        /// <returns>The snake's location.</returns>
        public Location GetLocation() => Location;

        /// <summary>
        /// Gets the snake's waypoints.
        /// </summary>
        /// <returns>The snake's waypoints.</returns>
        public Listard<Waypoint> GetWaypoints() => Waypoints;

        /// <summary>
        /// Increases the snake's length by one.
        /// </summary>
        public void Grow() => Grow(1);

        /// <summary>
        /// Increases the snake's length by <c>length</c>.
        /// </summary>
        /// <param name="length"></param>
        public void Grow(int length)
        {
            throw new NotImplementedException("Snake->Grow(int): Not implemented");
        }
    }
}