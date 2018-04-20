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

        /// <summary>
        /// Determines if the application is running on Windows
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows()
        {
            var pid = Environment.OSVersion.Platform;
            return pid == PlatformID.Win32NT || pid == PlatformID.Win32S ||
                   pid == PlatformID.Win32Windows || pid == PlatformID.WinCE;
        }
    }
}