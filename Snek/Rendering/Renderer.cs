using System;
using System.Collections.Generic;
using Listard;

namespace Snek.Rendering
{
    public class Renderer
    {
        /// <summary>
        /// Rendering cache.
        /// </summary>
        private Dictionary<IRenderable, RenderMap> _cache = new Dictionary<IRenderable, RenderMap>();

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
            var renderMap = entity.GetRenderMap();

            if (_cache.ContainsKey(entity))
            {
                // TODO: improve
                var cachedMap = _cache[entity];

                foreach (var location in renderMap.GetLocations())
                {
                    var lookup = renderMap.Lookup(location);

                    if (cachedMap.HasLocation(location) && lookup == cachedMap.Lookup(location))
                        continue;

                    Console.SetCursorPosition(location.X, location.Y);
                    Console.Write(lookup);
                }

                foreach (var location in cachedMap.GetLocations())
                {
                    if (renderMap.HasLocation(location)) continue;
                    
                    Console.SetCursorPosition(location.X, location.Y);
                    Console.Write(' ');
                }

                _cache[entity] = renderMap;
            }
            else
            {
                _cache.Add(entity, renderMap);
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