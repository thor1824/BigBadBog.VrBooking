using System;
using System.Collections.Generic;
using VrBooking.Core.Entity;

namespace VrBooking.Core.ApplicationServices
{
    public interface IBookingOrderService
    {
        BookingOrder Create(BookingOrder bo);
        BookingOrder Delete(long id);
        BookingOrder Read(long id);
        List<BookingOrder> ReadAll();
        BookingOrder Update(BookingOrder bo);
        List<BookingOrder> ReadByWeek(DateTime weekStart, DateTime weekEnd, long productId);
    }
}