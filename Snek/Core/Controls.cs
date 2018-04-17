using Snek.Types;
using System;
using System.Collections.Generic;

namespace Snek.Core
{
    public static class Controls
    {
        /// <summary>
        /// Key to direction map.
        /// </summary>
        public static readonly Dictionary<ConsoleKey, Direction> KeyToDirection = new Dictionary<ConsoleKey, Direction>
        {
            {ConsoleKey.RightArrow, Direction.Right},
            {ConsoleKey.LeftArrow, Direction.Left},
            {ConsoleKey.UpArrow, Direction.Up},
            {ConsoleKey.DownArrow, Direction.Down}
        };

        /// <summary>
        /// Checks if a key is a direction key.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if the key is a direction key, false otherwise.</returns>
        public static bool DirectionKey(ConsoleKey key) => KeyToDirection.ContainsKey(key);

        /// <summary>
        /// Gets the opposite direction.
        /// </summary>
        /// <param name="direction">Direction to get the opposite of.</param>
        /// <returns>The opposite direction.</returns>
        public static Direction Opposite(Direction direction)
        {
            if (IsHorizontal(direction))
                return direction == Direction.Left ? Direction.Right : Direction.Left;

            return direction == Direction.Up ? Direction.Down : Direction.Up;
        }

        /// <summary>
        /// Checks if a direction is horizontal.
        /// </summary>
        /// <param name="direction">Direction to check.</param>
        /// <returns>True if the direction is horizontal, false otherwise.</returns>
        public static bool IsHorizontal(Direction direction)
        {
            return direction == Direction.Left || direction == Direction.Right;
        }

        /// <summary>
        /// Checks if a direction is vertical.
        /// </summary>
        /// <param name="direction">Direction to check.</param>
        /// <returns>True if the direction is vertical, false otherwise.</returns>
        public static bool IsVertical(Direction direction)
        {
            return direction == Direction.Up || direction == Direction.Down;
        }

        /// <summary>
        /// Checks if a move is valid.
        /// </summary>
        /// <param name="currentDirection">Current direction.</param>
        /// <param name="direction">Direction to check.</param>
        /// <returns>True if the move is valid, false otherwise.</returns>
        public static bool ValidMove(Direction currentDirection, Direction direction)
        {
            return IsHorizontal(currentDirection) ? IsVertical(direction) : IsHorizontal(direction);
        }
    }
}