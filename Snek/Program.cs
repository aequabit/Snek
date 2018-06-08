﻿/*
 * ------------------------------
 * Project:     Snek
 * Name:        Program.cs
 * Type:        MainClass
 * Date:        2018-05-04
 * ------------------------------
 */

using System;
using Snek.Core;

namespace Snek
{
    internal static class MainClass
    {
        public static void Main(string[] args)
        {
            Console.Title = "( ͡° ͜ʖ ͡°)";
            new Game().Start();
        }
    }
}