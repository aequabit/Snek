using Listard;
using Snek.Types;
using System;
using System.Collections.Generic;

namespace Snek.Rendering
{
    public class RenderMap
    {
        private readonly Dictionary<Position, char> _map = new Dictionary<Position, char>();

        /// <summary>
        /// Adds a char too the render map.
        /// </summary>
        /// <param name="position">The location of the render char.</param>
        /// <param name="renderChar">The char to render.</param>
        public void Add(Position position, char renderChar)
        {
            _map.Add(position, renderChar);
        }

        /// <summary>
        /// Gets a list of positions in the render map.
        /// </summary>
        /// <returns>List of positions in the render map.</returns>
        public Listard<Position> GetPositions()
        {
            var list = new Listard<Position>();

            foreach (var key in _map.Keys)
                list.Add(key);

            return list;
        }

        /// <summary>
        /// Checks if the render map contains a position.
        /// </summary>
        /// <param name="position">Position to check.</param>
        /// <returns>True if the render map contains the position, false otherwise.</returns>
        public bool HasPosition(Position position)
        {
            return _map.ContainsKey(position);
        }

        /// <summary>
        /// Looks up the render char for a position.
        /// </summary>
        /// <param name="position">Position to get the render char for.</param>
        /// <returns>The render char.</returns>
        public char Lookup(Position position)
        {
            if (!_map.ContainsKey(position))
                throw new Exception("Position is not in the render map");

            return _map[position];
        }
    }
}