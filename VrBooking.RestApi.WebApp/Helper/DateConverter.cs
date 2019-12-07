using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VrBooking.RestApi.WebApp.Helper
{
    public static class DateConverter
    {
        public static double FromDatetimeToUTCEpoch(DateTime date) { 
            return date.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        public static DateTime FromUTCEpochToDatetime(long msSinceEpoch)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc) + new TimeSpan(msSinceEpoch * 10000);
        }

    }
}
