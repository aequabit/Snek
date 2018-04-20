using Listard;
using Snek.Core;
using Snek.Rendering;
using Snek.Types;
using System;

namespace Snek.UI
{
    public class StatusBar : IComponent
    {
        /// <summary>
        /// Game to display info for.
        /// </summary>
        private Game _game;

        /// <summary>
        /// Status bar text.
        /// </summary>
        private string _text = "";

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">Game to display info for.</param>
        public StatusBar(Game game)
        {
            _game = game;
        }

        /// <inheritdoc cref="IComponent.Location"/>
        public Location Location() => Types.Location.Bottom;

        /// <inheritdoc cref="IComponent.Size"/>
        public int Size() => 1;

        /// <inheritdoc cref="IComponent.Update"/>
        public void Update()
        {
            var snake = _game.Snake;

            // Get the snake's head
            var position = _game.Snake.Positions().Last();

            // Status bar template
            _text = $"X: {position.X} | Y: {position.Y} | Length: {snake.Length} | Cycle: {snake.CycleDelay}";
        }

        /// <inheritdoc cref="IRenderable.RenderMap"/>
        public RenderMap RenderMap(bool compatibility = false)
        {
            var map = new RenderMap();

            // Calculate the location of the status bar
            for (var i = Console.BufferWidth - _text.Length; i < Console.BufferWidth; i++)
                map.Add(new Position(i, Console.BufferHeight - 1), _text[i - Console.BufferWidth + _text.Length]);

            return map;
        }
    }
}