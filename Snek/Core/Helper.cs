using System;

namespace Snek.Core
{
    public class Helper
    {
        public static long UnixTime()
        {
            return (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}