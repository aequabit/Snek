using Listard;
using Snek.Rendering;
using Snek.Types;

namespace Snek.Entities
{
    public class Food : IEntity, IRenderable
    {
        /// <inheritdoc cref="_positions"/>
        public Listard<Position> Positions() => _positions;
        
        /// <inheritdoc cref="IEntity.Positions"/>
        private readonly Listard<Position> _positions = new Listard<Position>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="position">Location of the food.</param>
        public Food(Position position)
        {
            _positions.Add(position);
        }

        /// <inheritdoc cref="IEntity.Update"/>
        public void Update()
        {
        }

        /// <inheritdoc cref="IRenderable.RenderMap"/>
        public RenderMap RenderMap(bool compatibility = false)
        {
            var map = new RenderMap();

            map.Add(_positions[0], compatibility ? 'x' : '●');

            return map;
        }
    }
}