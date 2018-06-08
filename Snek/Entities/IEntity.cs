/*
 * ------------------------------
 * Project:     Snek
 * Name:        IEntity.cs
 * Type:        Interface
 * Date:        2018-05-04
 * ------------------------------
 */

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
        Listard<Position> Positions();
        
        /// <summary>
        /// Update method called on every game tick.
        /// </summary>
        void Update();
    }
}