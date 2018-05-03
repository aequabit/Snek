using Snek.Rendering;
using Snek.Types;

namespace Snek.UI
{
    public class TitleBar : IComponent
    {
        /// <summary>
        /// Title bar text.
        /// </summary>
        private string _text = "";

        /// <inheritdoc cref="IComponent.Location"/>
        public Location Location() => Types.Location.Top;

        /// <inheritdoc cref="IComponent.Size"/>
        public int Size() => 1;

        /// <inheritdoc cref="IComponent.Update"/>
        public void Update()
        {
            // Status bar template
            _text = "Snek Development Build";
        }

        /// <inheritdoc cref="IRenderable.RenderMap"/>
        public RenderMap RenderMap(bool compatibility = false)
        {
            var map = new RenderMap();

            // Calculate the location of the title bar
            for (var i = 0; i < _text.Length; i++)
                map.Add(new Position(i, 0), _text[i]);

            return map;
        }
    }
}
