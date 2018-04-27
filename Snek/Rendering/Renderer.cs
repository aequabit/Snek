using System;
using System.Collections.Generic;
using Snek.Types;

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
        /// Renders a renderable object.
        /// </summary>
        /// <param name="renderable">Object to render.</param>
        public void Render(IRenderable renderable)
        {
            var renderMap = renderable.RenderMap(_compatibility);

            // If the object already existed in the previous cycle and needs re-rendering
            if (_cache.ContainsKey(renderable))
            {
                // TODO: Improve caching and re-rendering
                var cachedMap = _cache[renderable];

                foreach (var position in renderMap.GetPositions())
                {
                    var lookup = renderMap.Lookup(position);

                    // Don't re-render the position if it doesn't differ from the cache
                    if (cachedMap.HasPosition(position) && lookup == cachedMap.Lookup(position))
                        continue;

                    Draw(renderable, position, lookup);
                }

                // Clear the cached positions that don't exist anymore
                foreach (var cachedPosition in cachedMap.GetPositions())
                {
                    if (renderMap.HasPosition(cachedPosition)) continue;

                    Draw(renderable, cachedPosition, ' ');
                }

                // Update the rendering cache
                _cache[renderable] = renderMap;
            }
            else // The object wasn't rendered before and will be rendered the first time
            {
                // Store the object's render map in the cache
                _cache.Add(renderable, renderMap);

                // Render all positions on the render map
                foreach (var position in renderMap.GetPositions())
                    Draw(renderable, position, renderMap.Lookup(position));
            }
        }

        /// <summary>
        /// Draws on a position.
        /// </summary>
        /// <param name="position">Position to draw on.</param>
        /// <param name="renderChar">Character to draw.</param>
        private void Draw(IRenderable renderable/*remove*/, Position position, char renderChar)
        {
            if (position.X < 0 || position.X > Console.BufferWidth || position.Y < 0 ||
                position.Y > Console.BufferHeight)
                return;

            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(renderChar);
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