using System;
using System.Collections.Generic;
using System.Threading;

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

        private static double Unix()
        {
            return DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        public static void Main(string[] args)
        {
            Console.Title = "( ͡° ͜ʖ ͡°)";

            Console.Clear();

            const int width = 32;
            const int height = 32;

            var direction = Direction.Right;
            var x = width / 2;
            var y = height / 2;

            var time = Unix();

            new Thread(() =>
            {
                while (true)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(' ');
                    Console.SetCursorPosition(width, height);

                    if (Unix() - time > 500)
                    {
                        time = Unix();

                        switch (direction)
                        {
                            case Direction.Right:
                                x++;
                                break;
                            case Direction.Left:
                                x--;
                                break;
                            case Direction.Up:
                                y--;
                                break;
                            case Direction.Down:
                                y++;
                                break;
                        }

                        if (x < 0)
                            x = x + width;
                        else if (x >= width)
                            x = x - width;

                        if (y < 0)
                            y = y + height;
                        else if (y >= height)
                            y = y - height;
                    }

                    Console.SetCursorPosition(x, y);
                    Console.Write('■');
                    Console.SetCursorPosition(width, height);

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
                if (KeyToDirection.ContainsKey(key.Key))
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

        private enum Direction
        {
            Right,
            Left,
            Up,
            Down
        }
    }
}