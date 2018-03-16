using System;

namespace Snek
{
    public class OsInfo
    {
        /// <summary>
        ///     Determines if the application is running on Windows
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