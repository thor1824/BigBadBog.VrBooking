using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VrBooking.Core.DomainServices;
using VrBooking.Core.Entity;

namespace VrBooking.Infrastructure.Repositories
{
    public class BookingRepository : IRepository<BookingOrder>
    {

        private readonly VrBookingContext _ctx;
        public BookingRepository(VrBookingContext contex)
        {
            _ctx = contex;
        }

        public BookingOrder Create(BookingOrder entity)
        {
            BookingOrder createdBooking = _ctx.Add<BookingOrder>(entity).Entity;
            _ctx.SaveChanges();
            return createdBooking;
        }

        public BookingOrder Delete(BookingOrder entity)
        {
            BookingOrder booking = _ctx.Remove<BookingOrder>(entity).Entity;
            _ctx.SaveChanges();
            return booking;
        }

        public BookingOrder Read(long id)
        {
            return _ctx.BookingOrders
                .Include(booking => booking.User).Include(booking => booking.Product)
                .FirstOrDefault(booking => booking.Id == id);
        }

        public IEnumerable<BookingOrder> ReadAll()
        {
            return _ctx.BookingOrders
                .Include(booking => booking.User).Include(booking => booking.Product);
        }

        public BookingOrder Update(BookingOrder entity)
        {
            BookingOrder OldBooking = _ctx.BookingOrders.First(booking => booking.Id == entity.Id);
            OldBooking.Product = entity.Product;
            OldBooking.User = entity.User;
            OldBooking.StartTimeOfBooking = entity.StartTimeOfBooking;
            OldBooking.EndTimeOfBooking = entity.EndTimeOfBooking;
            _ctx.SaveChanges();
            return entity;

        }
    }
}
