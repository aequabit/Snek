using System;

namespace Snek.Core
{
    public static class Helper
    {
        /// <summary>
        /// Gets the current UNIX timestamp.
        /// </summary>
        /// <returns>The current UNIX timestamp.</returns>
        public static long UnixTime()
        {
            return (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}