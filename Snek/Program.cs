using System;
using System.Collections.Generic;
using System.Threading;
using Listard;
using Snek.Entities;

namespace Snek
{
    internal static class MainClass
    {
        private static readonly Dictionary<ConsoleKey, Direction> KeyToDirection = new Dictionary<ConsoleKey, Direction>
        {
            {ConsoleKey.RightArrow, Direction.Right},
            {ConsoleKey.LeftArrow, Direction.Left},
            {ConsoleKey.UpArrow, Direction.Up},
            {ConsoleKey.DownArrow, Direction.Down}
        };

        public static double Unix()
        {
            return DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        public static void Main(string[] args)
        {
            Console.Title = "( ͡° ͜ʖ ͡°)";
            Console.CursorVisible = false;

            Console.Clear();

            int width = Console.BufferWidth - 1;
            int height = Console.BufferHeight - 1;

            var locations = new Listard<Location>();
            var direction = Direction.Right;
            var length = 8;

            locations.Add(new Location
            {
                X = width / 2,
                Y = height / 2
            });

            var time = Unix();

            new Thread(() =>
            {
                while (true)
                {
                    if (Console.BufferWidth - 1 != width || Console.BufferHeight - 1 != height)
                    {
                        Console.Clear();
                        Console.WriteLine("Abort: Window resized");
                        return;
                    }

                    if (Unix() - time > 500)
                    {
                        time = Unix();

                        var newLocation = locations.Last();

                        switch (direction)
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
                            newLocation.X = newLocation.X + width;
                        else if (newLocation.X >= width)
                            newLocation.X = newLocation.X - width;

                        if (newLocation.Y < 0)
                            newLocation.Y = newLocation.Y + height;
                        else if (newLocation.Y >= height)
                            newLocation.Y = newLocation.Y - height;

                        locations.Add(newLocation);
                    }

                    // TODO: re rendering
                    // TODO: disallow direction change to opposite direction
                    // TODO: add clearing of empty fields
                    // TODO: collision events

                    if (locations.Count > length)
                    {
                        Console.SetCursorPosition(locations[0].X, locations[0].Y);
                        Console.Write(' ');

                        locations.RemoveAt(0);
                    }

                    var last = locations.Last();

                    Console.SetCursorPosition(last.X, last.Y);
                    Console.Write('█');

                    Thread.Sleep(5);
                }
            }).Start();

            while (true)
            {
                var key = Console.ReadKey(true);

                if (!KeyToDirection.ContainsKey(key.Key)) continue;

                direction = KeyToDirection[key.Key];
            }
            // var waypoints = new Listard<Waypoint>();

            // Waypoint w1;
            // w1.Direction = Direction.Up;
            // w1.X = 10;
            // w1.Y = 10;

            // Waypoint w2;
            // w2.Direction = Direction.Right;
            // w2.X = 10;
            // w2.Y = 1;

            // waypoints.Add(w1, w2);

            // var location = new Waypoint() { Direction = Direction.Down, X = 19, Y = 1 };

            // waypoints.Add(location);

            // var locations = new Listard<Location>();

            // for (var i = waypoints.Count - 1; i >= 0; i--)
            // {
            //     var waypoint = waypoints[i];

            //     locations.Add(new Location() { X = waypoint.X, Y = waypoint.Y });

            //     if (waypoint.Direction == Direction.Left)
            //     {
            //         for (var j = location.X; j < waypoint.X; j++)
            //             locations.Add(new Location() { X = j, Y = waypoint.Y });
            //     }
            //     if (waypoint.Direction == Direction.Right)
            //     {
            //         for (var j = location.X; j > waypoint.X; j--)
            //             locations.Add(new Location() { X = j, Y = waypoint.Y });
            //     }
            //     if (waypoint.Direction == Direction.Down)
            //     {
            //         for (var j = location.Y; j > waypoint.Y; j--)
            //             locations.Add(new Location() { X = waypoint.X, Y = j });
            //     }
            //     if (waypoint.Direction == Direction.Up)
            //     {
            //         for (var j = location.Y; j < waypoint.Y; j++)
            //             locations.Add(new Location() { X = waypoint.X, Y = j });
            //     }
            // }

            // Console.WriteLine("0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9");
            // for (var i = 0; i < 20; i++)
            // {
            //     for (var j = 0; j < 20; j++)
            //     {
            //         var l = locations.FindElement(new Location() { X = j, Y = i });
            //         if (l.Equals(default(Location)))
            //             Console.Write("  ");
            //         else
            //             Console.Write("■ ");


            // foreach (Location l in locations)
            // {
            //     if (l.X == j && l.Y == i)
            //         Console.Write('■');

            // if (w.Direction == Direction.X && j == x + w.Offset && i == y)
            //     Console.Write("x");
            // else if (w.Direction == Direction.Y && i == y + w.Offset && j == x)
            //     Console.Write("x");
            // else
            //     Console.Write(" ");
            // }
            // }

            // Console.Write(i + "\n");
            //     }

            // var game = new Game(800, 600);
            // game.Run();
        }

        private struct Waypoint
        {
            public Direction Direction;

            public int X;

            public int Y;
        }

        private struct Location
        {
            public int X;

            public int Y;
        }

        private enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }
    }
}