using Listard;
using Snek.Core;
using Snek.Rendering;

namespace Snek.Entities
{
    public class Snake : IRenderable
    {
        /// <summary>
        /// The snake's locations.
        /// </summary>
        private Listard<Location> _locations;

        /// <inheritdoc cref="IRenderable.RenderChar"/>
        public char RenderChar(Location location)
        {
            return 'd';
        }

        /// <inheritdoc cref="IRenderable.Locations"/>
        public Listard<Location> Locations()
        {
            return new Listard<Location>();
        }
    }
}