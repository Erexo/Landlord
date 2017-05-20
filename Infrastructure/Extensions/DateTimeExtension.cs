using System;

namespace Infrastructure.Extensions
{
    public static class DateTimeExtension
    {
        public static int ToEpoch(this DateTime value)
        {
            return (int)(value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
