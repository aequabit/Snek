﻿/*
 * ------------------------------
 * Project:     Snek
 * Name:        IRenderable.cs
 * Type:        Interface
 * Date:        2018-05-04
 * ------------------------------
 */

namespace Snek.Rendering
{
    public interface IRenderable
    {
        /// <summary>
        /// Gets the entity's render map.
        /// </summary>
        /// <param name="compatibility">Compatibility mode for rendering.</param>
        /// <returns>Render map.</returns>
        RenderMap RenderMap(bool compatibility = false);
    }
}