using System;

namespace VrBooking.RestApi.WebApp.Helper
{
    public static class DateConverter
    {
        public static double FromDatetimeToUTCEpoch(DateTime date)
        {
            return date.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
        }

        public static DateTime FromUTCEpochToDatetime(long msSinceEpoch)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0) + new TimeSpan(msSinceEpoch * 10000);
        }

    }
}
