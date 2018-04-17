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
            // TODO: Make the status bar independent fron the entity rendering queue
            var yOffset = entity is StatusBar ? 0 : 1;

            var renderMap = entity.GetRenderMap(_compatibility);

            // If the entity already existed in the previous cycle and needs re-rendering
            if (_cache.ContainsKey(entity))
            {
                // TODO: Improve caching and re-rendering
                var cachedMap = _cache[entity];

                foreach (var location in renderMap.GetLocations())
                {
                    var lookup = renderMap.Lookup(location);

                    // Don't re-render the location if it doesn't differ from the cache
                    if (cachedMap.HasLocation(location) && lookup == cachedMap.Lookup(location))
                        continue;

                    // Render the location
                    Console.SetCursorPosition(location.X, location.Y + yOffset);
                    Console.Write(lookup);
                }

                foreach (var cachedLocation in cachedMap.GetLocations())
                {
                    if (renderMap.HasLocation(cachedLocation)) continue;

                    // Clear the location if the cached location doesn't exist current map
                    Console.SetCursorPosition(cachedLocation.X, cachedLocation.Y + yOffset);
                    Console.Write(' ');
                }

                // Update the rendering cache
                _cache[entity] = renderMap;
            }
            else // The entity wasn't rendered before and will be rendered the first time
            {
                // Store the entity's render map in the cache
                _cache.Add(entity, renderMap);

                foreach (var location in renderMap.GetLocations())
                {
                    var lookup = renderMap.Lookup(location);

                    // Render the location
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