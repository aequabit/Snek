using Listard;
using Snek.Types;

namespace Snek.Entities
{
    public interface IEntity
    {
        /// <summary>
        /// Locations of the entity.
        /// </summary>
        /// <returns>The locations of the entity.</returns>
        Listard<Location> Locations();
        
        /// <summary>
        /// Update method called on every game tick.
        /// </summary>
        void Update();
    }
}