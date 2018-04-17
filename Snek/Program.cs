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
        public static void Main(string[] args)
        {
            Console.Title = "( ͡° ͜ʖ ͡°)";

            var game = new Game(Console.BufferWidth - 1, Console.BufferHeight - 2);
            game.Start();
        }
    }
}