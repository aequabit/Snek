using Snek.Core;

namespace Snek.Rendering
{
    public interface IRenderable
    {
        /// <summary>
        /// Gets a list the entity's render map.
        /// </summary>
        /// <returns>Render map.</returns>
        RenderMap GetRenderMap();
    }
}