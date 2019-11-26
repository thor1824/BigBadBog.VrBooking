using System.Collections.Generic;

namespace VrBooking.Core.ApplicationServices
{
    public interface IBookingOrderService
    {
        BookingOrder Create(BookingOrder bo);
        BookingOrder Delete(long id);
        BookingOrder Read(long id);
        List<BookingOrder> ReadAll();
        BookingOrder Update(BookingOrder bo);
    }
}