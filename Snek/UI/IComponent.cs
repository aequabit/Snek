using Snek.Rendering;
using Snek.Types;

namespace Snek.UI
{
    public interface IComponent : IRenderable
    {
        /// <summary>
        /// Location of the component.
        /// </summary>
        /// <returns>The location of the component.</returns>
        Location Location();

        /// <summary>
        /// The width or height of the component.
        /// </summary>
        /// <returns>Width or height of the component.</returns>
        int Size();
        
        /// <summary>
        /// Update method called on every game tick.
        /// </summary>
        void Update();
    }
}