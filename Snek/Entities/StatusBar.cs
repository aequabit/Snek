using Listard;
using Snek.Core;
using Snek.Rendering;
using Snek.Types;
using System;

namespace Snek.Entities
{
    public class StatusBar : IEntity, IRenderable
    {
        /// <inheritdoc cref="_locations"/>
        public Listard<Location> Locations() => _locations;

        /// <inheritdoc cref="IEntity.Locations"/>
        private readonly Listard<Location> _locations = new Listard<Location>();
        
        /// <summary>
        /// Game to display info for.
        /// </summary>
        private readonly Game _game; 

        /// <summary>
        /// Status bar title.
        /// </summary>
        private string _statusTitle = "Snek Debug Build";

        /// <summary>
        /// Status bar.
        /// </summary>
        private string _statusBar = "";

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">Game to display info for.</param>
        public StatusBar(Game game)
        {
            _game = game;
        }

        /// <inheritdoc cref="IEntity.Update"/>
        public void Update()
        {
            _locations.Clear();

            var snakeLocation = _game.Snake.Locations().Last();

            _statusBar = $"X: {snakeLocation.X} | Y: {snakeLocation.Y} | Length: {_game.Snake.Length} | Cycle: {_game.Snake.CycleDelay}";

            for (var i = 0; i < _statusTitle.Length; i++)
                _locations.Add(new Location {X = i, Y = 0});

            for (var i = Console.BufferWidth - _statusBar.Length; i < Console.BufferWidth; i++)
                _locations.Add(new Location {X = i, Y = 0});
        }

        /// <inheritdoc cref="IRenderable.GetRenderMap"/>
        public RenderMap GetRenderMap(bool compatibility = false)
        {
            var map = new RenderMap();

            foreach (var location in _locations)
            {
                if (location.X < _statusTitle.Length)
                    map.Add(location, _statusTitle[location.X]);
                else if (location.X < Console.BufferWidth)
                    map.Add(location, _statusBar[location.X - Console.BufferWidth + _statusBar.Length]);
            }

            return map;
        }
    }
}