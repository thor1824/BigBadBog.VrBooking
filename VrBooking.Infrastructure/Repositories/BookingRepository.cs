using System.Collections.Generic;
using System.Linq;
using VrBooking.Core.DomainServices;
using VrBooking.Core.Entity;

namespace VrBooking.Infrastructure.Repositories
{
    public class BookingRepository : IRepository<BookingOrder>
    {

        private readonly VrBookingContext _contex;
        public BookingRepository(VrBookingContext contex)
        {
            _contex = contex;
        }

        public BookingOrder Create(BookingOrder entity)
        {
            BookingOrder createdBooking = _contex.Add<BookingOrder>(entity).Entity;
            _contex.SaveChanges();
            return createdBooking;
        }

        public BookingOrder Delete(BookingOrder entity)
        {
            BookingOrder booking = _contex.Remove<BookingOrder>(entity).Entity;
            _contex.SaveChanges();
            return booking;
        }

        public BookingOrder Read(long id)
        {
            return _contex.BookingOrders.FirstOrDefault(prod => prod.Id == id);
        }

        public IEnumerable<BookingOrder> ReadAll()
        {
            return _contex.BookingOrders;
        }

        public BookingOrder Update(BookingOrder entity)
        {
            BookingOrder updatedBooking = _contex.Update<BookingOrder>(entity).Entity;
            _contex.SaveChanges();
            return updatedBooking;

        }
    }
}
