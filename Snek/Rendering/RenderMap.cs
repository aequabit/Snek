using Listard;
using Snek.Types;
using System;
using System.Collections.Generic;

namespace Snek.Rendering
{
    public class RenderMap
    {
        private readonly Dictionary<Location, char> _map = new Dictionary<Location, char>();

        /// <summary>
        /// Adds a char too the render map.
        /// </summary>
        /// <param name="location">The location of the render char.</param>
        /// <param name="renderChar">The char to render.</param>
        public void Add(Location location, char renderChar)
        {
            _map.Add(location, renderChar);
        }

        /// <summary>
        /// Gets a list of locations in the render map.
        /// </summary>
        /// <returns>List of locations in the render map.</returns>
        public Listard<Location> GetLocations()
        {
            var list = new Listard<Location>();

            foreach (var key in _map.Keys)
                list.Add(key);

            return list;
        }

        /// <summary>
        /// Checks if the render map contains a location.
        /// </summary>
        /// <param name="location">Location to check.</param>
        /// <returns>True if the render map contains the location, false otherwise.</returns>
        public bool HasLocation(Location location)
        {
            return _map.ContainsKey(location);
        }

        /// <summary>
        /// Looks up the render char for a location.
        /// </summary>
        /// <param name="location">Location to get the render char for.</param>
        /// <returns>The render char.</returns>
        public char Lookup(Location location)
        {
            if (!_map.ContainsKey(location))
                throw new Exception("Location is not in the render map");

            return _map[location];
        }
    }
}