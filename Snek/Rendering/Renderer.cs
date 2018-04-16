using System;
using System.Collections.Generic;
using System.Diagnostics;
using Listard;
using Snek.Core;
using Snek.Entities;

namespace Snek.Rendering
{
    public class Renderer
    {
        /// <inheritdoc cref="_compatibility"/>
        public bool Compatibility
        {
            get => _compatibility;
            set => _compatibility = value;
        }

        /// <summary>
        /// Rendering cache.
        /// </summary>
        private readonly Dictionary<IRenderable, RenderMap> _cache = new Dictionary<IRenderable, RenderMap>();

        /// <summary>
        /// Compatibility rendering mode.
        /// </summary>
        private bool _compatibility;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Renderer()
        {
            Console.CursorVisible = false;
            Console.Clear();
        }

        /// <summary>
        /// Renders an entity.
        /// </summary>
        /// <param name="entity">Entity to render.</param>
        public void Render(IRenderable entity)
        {
            // TODO: improve
            // status bar rendering offset
            var yOffset = entity is StatusBar ? 0 : 1;

            var renderMap = entity.GetRenderMap(_compatibility);

            if (_cache.ContainsKey(entity))
            {
                // TODO: improve
                var cachedMap = _cache[entity];

                foreach (var location in renderMap.GetLocations())
                {
                    var lookup = renderMap.Lookup(location);

                    if (cachedMap.HasLocation(location) && lookup == cachedMap.Lookup(location))
                        continue;

                    Console.SetCursorPosition(location.X, location.Y + yOffset);
                    Console.Write(lookup);
                }

                foreach (var location in cachedMap.GetLocations())
                {
                    if (renderMap.HasLocation(location)) continue;

                    Console.SetCursorPosition(location.X, location.Y + yOffset);
                    Console.Write(' ');
                }

                _cache[entity] = renderMap;
            }
            else
            {
                _cache.Add(entity, renderMap);

                foreach (var location in renderMap.GetLocations())
                {
                    var lookup = renderMap.Lookup(location);

                    Console.SetCursorPosition(location.X, location.Y + yOffset);
                    Console.Write(lookup);
                }
            }
        }

        /// <summary>
        /// Flushes the rendering cache.
        /// </summary>
        /// <returns>The old rendering cache.</returns>
        public Dictionary<IRenderable, RenderMap> Flush()
        {
            var cache = _cache;

            _cache.Clear();

            return cache;
        }
    }
}