using System;
using System.Collections.Generic;
using System.Text;

namespace VrBooking.Core.ApplicationServices
{
    public class BookingOrderService : IBookingOrderService
    {
        private readonly IRepository<BookingOrder> _repo;
        public BookingOrderService(IRepository<BookingOrder> repo) 
        {
            _repo = repo
        }

        public BookingOrder Create(BookingOrder bo)
        {
            return null;
        }

        public BookingOrder Read(long id)
        {
            return null;
        }
        public List<BookingOrder> ReadAll()
        {
            return null;
        }
        public BookingOrder Update(BookingOrder bo)
        {
            return null;
        }
        public BookingOrder Delete(long id)
        {
            return null;
        }
    }
}
