using Listard;
using Snek.Rendering;
using Snek.Types;
using System.Linq;

namespace Snek.Entities
{
    public class Food : IEntity, IRenderable
    {
        /// <inheritdoc cref="_locations"/>
        public Listard<Location> Locations() => _locations;
        
        /// <inheritdoc cref="IEntity.Locations"/>
        private readonly Listard<Location> _locations = new Listard<Location>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="location">Location of the food.</param>
        public Food(Location location)
        {
            _locations.Add(location);
        }

        /// <inheritdoc cref="IEntity.Update"/>
        public void Update()
        {
        }

        /// <inheritdoc cref="IRenderable.GetRenderMap"/>
        public RenderMap GetRenderMap(bool compatibility = false)
        {
            var map = new RenderMap();

            map.Add(_locations.First(), compatibility ? 'x' : '●');

            return map;
        }
    }
}