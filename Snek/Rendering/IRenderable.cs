using Snek.Core;

namespace Snek.Rendering
{
    public interface IRenderable
    {
        /// <summary>
        /// Gets a list the entity's render map.
        /// </summary>
        /// <param name="compatibility">Compatibility mode for rendering.</param>
        /// <returns>Render map.</returns>
        RenderMap GetRenderMap(bool compatibility = false);
    }
}