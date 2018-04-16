using System;
using System.Collections.Generic;
using System.Threading;
using Listard;
using Snek.Core;
using Snek.Entities;

namespace Snek
{
    internal static class MainClass
    {
        // TODO: re rendering
        // TODO: increase speed after time

        public static void Main(string[] args)
        {
            Console.Title = "( ͡° ͜ʖ ͡°)";

            var game = new Game(Console.BufferWidth - 1, Console.BufferHeight - 2);
            game.Start();

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
    }
}