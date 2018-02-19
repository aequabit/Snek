using System;
using SnakeGame.Core;
using SnakeGame.Entities;
using Listard;

namespace SnakeGame.Rendering
{
    public static class Renderer
    {
        /// <summary>
        /// The current render board.
        /// </summary>
        private static Board Board;

        /// <summary>
        /// Renders a board.
        /// </summary>
        /// <param name="board">Board to render.</param>
        public static void Render(Board board)
        {
            Board = board;

            if (OSInfo.IsWindows())
            {
                Console.WindowWidth = board.Width;
                Console.WindowHeight = board.Height;
            }

            Console.BackgroundColor = ConsoleColor.Yellow;
        }

        /// <summary>
        /// Renderes a snake.
        /// </summary>
        /// <param name="snake">Snake to render.</param>
        public static void Render(Snake snake)
        {
            if (Board == null)
                throw new Exception("Board not set"); // TODO: custom exception

            var location = snake.GetLocation();
            var renderLocation = RenderLocation(snake);

            for (var i = 0; i < Board.Height; i++)
            {
                for (var j = 0; j < Board.Width; j++)
                {
                    foreach (Location loc in renderLocation)
                    {
                        if (loc.X == j && loc.Y == i)
                            Console.Write('■');
                        else
                            Console.Write(' ');
                    }
                }

                Console.Write('\n');
            }

            // for (var i = 0; i < location.Length; i++)
            // {
            //     for (var j = 0; j < location[i].Length; j++)
            //     {
            //         if (location[i][j] == 1)
            //             Console.Write('■');
            //         else
            //             Console.Write(' ');
            //     }

            //     Console.Write('\n');
            // }
        }

        /// <summary>
        /// Clears the already rendered output.
        /// </summary>
        public static void Clear()
        {
            Console.Clear();
        }

        private static Listard<Location> RenderLocation(Snake snake)
        {
            var location = snake.GetLocation();
            var waypoints = snake.GetWaypoints();

            var locations = new Listard<Location>();

            for (var i = waypoints.Count - 1; i >= 0; i--)
            {
                var waypoint = waypoints[i];

                locations.Add(new Location() { X = waypoint.X, Y = waypoint.Y });

                if (waypoint.Direction == Direction.Left)
                {
                    for (var j = location.X; j < waypoint.X; j++)
                        locations.Add(new Location() { X = j, Y = waypoint.Y });
                }
                if (waypoint.Direction == Direction.Right)
                {
                    for (var j = location.X; j > waypoint.X; j--)
                        locations.Add(new Location() { X = j, Y = waypoint.Y });
                }
                if (waypoint.Direction == Direction.Down)
                {
                    for (var j = location.Y; j > waypoint.Y; j--)
                        locations.Add(new Location() { X = waypoint.X, Y = j });
                }
                if (waypoint.Direction == Direction.Up)
                {
                    for (var j = location.Y; j < waypoint.Y; j++)
                        locations.Add(new Location() { X = waypoint.X, Y = j });
                }
            }

            return locations;
        }
    }
}