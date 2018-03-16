using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Reflection;
using Listard;
using Snek.Core;

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

            const int width = 32;
            const int height = 32;
            var waypoints = new Listard<Waypoint>();

            var current = new Waypoint()
            {
                X = width / 2,
                Y = height / 2,
                Direction = Direction.Right
            };

            waypoints.Add(new Waypoint()
            {
                X = current.X,
                Y = current.Y
            });

            var time = Unix();

            for (var i = 0; i < height; i++)
            {
                Console.SetCursorPosition(width, i);
                Console.Write('|');
            }

            new Thread(() =>
            {
                while (true)
                {
                    foreach (Waypoint waypoint in waypoints)
                    {
                        Console.SetCursorPosition(waypoint.X, waypoint.Y);
                        Console.Write('x');
                    }

                    Console.SetCursorPosition(current.X, current.Y);
                    Console.Write(' ');

                    if (Unix() - time > 500)
                    {
                        time = Unix();

                        switch (current.Direction)
                        {
                            case Direction.Left:
                                current.X--;
                                break;
                            case Direction.Right:
                                current.X++;
                                break;
                            case Direction.Up:
                                current.Y--;
                                break;
                            case Direction.Down:
                                current.Y++;
                                break;
                        }

                        if (current.X < 0)
                            current.X = current.X + width;
                        else if (current.X >= width)
                            current.X = current.X - width;

                        if (current.Y < 0)
                            current.Y = current.Y + height;
                        else if (current.Y >= height)
                            current.Y = current.Y - height;
                    }

                    // TODO: draw difference when direction wasn't changed yet
                    // TODO: limit length
                    // TODO: disallow direction change to opposite direction
                    // TODO: add clearing of empty fields
                    // TODO: draw through borders
                    // TODO: collision events

                    var locations = new Listard<Location>();

                    for (var i = waypoints.Count - 1; i >= 0; i--)
                    {
                        var waypoint = waypoints[i];

                        var nextPoint = (i < waypoints.Count - 1) ? waypoints[i + 1] : current;

                        locations.Add(new Location() {X = waypoint.X, Y = waypoint.Y});

                        switch (waypoint.Direction)
                        {
                            case Direction.Left:
                                for (var j = nextPoint.X; j < waypoint.X; j++)
                                    locations.Add(new Location() {X = j, Y = waypoint.Y});
                                break;
                            case Direction.Right:
                                for (var j = nextPoint.X; j > waypoint.X; j--)
                                    locations.Add(new Location() {X = j, Y = waypoint.Y});
                                break;
                            case Direction.Up:
                                for (var j = nextPoint.Y; j < waypoint.Y; j++)
                                    locations.Add(new Location() {X = waypoint.X, Y = j});
                                break;
                            case Direction.Down:
                                for (var j = nextPoint.Y; j > waypoint.Y; j--)
                                    locations.Add(new Location() {X = waypoint.X, Y = j});
                                break;
                        }
                    }

                    foreach (Location location in locations)
                    {
                        if (location.X == current.X && location.Y == current.Y)
                            continue;

                        Console.SetCursorPosition(location.X, location.Y);
                        Console.Write('#');
                    }

                    Console.SetCursorPosition(current.X, current.Y);
                    Console.Write('■');

                    // for (var i = 0; i <= height; i++)
                    // {
                    //     for (var j = 0; j <= width; j++)
                    //     {
                    //         if (i == y && j == x)
                    //             Console.Write('■');
                    //         else
                    //             Console.Write("  ");
                    //     }

                    //     Console.Write('\n');
                    // }

                    Thread.Sleep(5);
                }
            }).Start();

            while (true)
            {
                var key = Console.ReadKey(true);

                if (!KeyToDirection.ContainsKey(key.Key)) continue;

                current.Direction = KeyToDirection[key.Key];

                waypoints.Add(new Waypoint()
                {
                    X = current.X,
                    Y = current.Y,
                    Direction = current.Direction
                });
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

        private enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }
    }
}