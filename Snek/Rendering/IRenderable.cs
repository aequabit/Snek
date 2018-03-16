using Listard;
using Snek.Core;

namespace Snek.Rendering
{
    public interface IRenderable
    {
        /// <summary>
        /// Character to render for the given location.
        /// </summary>
        /// <param name="location">Location to get the character for.</param>
        /// <returns>Character to render at the given location.</returns>
        char RenderChar(Location location);

        /// <summary>
        /// Gets a list of locations to render the entity at.
        /// </summary>
        /// <returns>List of locations.</returns>
        Listard<Location> Locations();
    }
}