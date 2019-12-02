using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VrBooking.Core.DomainServices;
using VrBooking.Core.Entity;

namespace VrBooking.Core.ApplicationServices
{
    public class BookingOrderService : IBookingOrderService
    {
        private readonly IRepository<BookingOrder> _repo;
        public BookingOrderService(IRepository<BookingOrder> repo)
        {
            _repo = repo;
        }

        #region C.R.U.D
        public BookingOrder Create(BookingOrder booking)
        {
            BookingOrder createdBooking;
            try
            {
                if (HasNullValue(booking))
                {
                    throw new InvalidDataException("Booking contains null");
                }
                if (booking.StartTimeOfBooking > booking.EndTimeOfBooking)
                {
                    throw new InvalidDataException("Start time is after end time");
                }
                if (booking.StartTimeOfBooking.Ticks < DateTime.Now.AddSeconds(-10).Ticks)
                {
                    throw new InvalidDataException("The booking period has already startet");
                }
                if (booking.EndTimeOfBooking.Ticks < DateTime.Now.AddSeconds(-10).Ticks)
                {
                    throw new InvalidDataException("The booking period has already ended");
                }
                if (IsBookingTimeColiding(booking))
                {
                    throw new InvalidDataException("Time Collision detected");
                }
                createdBooking = _repo.Create(booking);
                if (IsIdValid(createdBooking))
                {
                    throw new InvalidOperationException("Id not assigned probably");
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return createdBooking;

        }



        public BookingOrder Read(long id)
        {
            BookingOrder booking;
            try
            {
                booking = _repo.Read(id);
                if (booking != null)
                {
                    if (booking.Id != id)
                    {
                        throw new InvalidOperationException("returns the wrong entity");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return booking;
        }

        public List<BookingOrder> ReadAll()
        {
            List<BookingOrder> bookings;
            try
            {
                bookings = _repo.ReadAll().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return bookings;
        }

        public BookingOrder Update(BookingOrder booking)
        {
            BookingOrder updatedBooking;
            try
            {
                if (HasNullValue(booking))
                {
                    throw new InvalidDataException("Booking contains null");
                }
                if (booking.StartTimeOfBooking > booking.EndTimeOfBooking)
                {
                    throw new InvalidDataException("Start time is after end time");
                }
                if (booking.StartTimeOfBooking.Ticks < DateTime.Now.AddSeconds(-10).Ticks)
                {
                    throw new InvalidDataException("The booking period has already startet");
                }
                if (booking.EndTimeOfBooking.Ticks < DateTime.Now.AddSeconds(-10).Ticks)
                {
                    throw new InvalidDataException("The booking period has already ended");
                }
                if (IsBookingTimeColiding(booking))
                {
                    throw new InvalidDataException("is coliding");
                }
                updatedBooking = _repo.Update(booking);
                if (!updatedBooking.Equals(Read(updatedBooking.Id)))
                {
                    throw new InvalidOperationException("entity was not updated");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return updatedBooking;
        }

        public BookingOrder Delete(long id)
        {
            BookingOrder deletedBooking;
            try
            {
                deletedBooking = _repo.Delete(Read(id));
                if (deletedBooking == null)
                {
                    throw new InvalidOperationException("Deleted Booking returned Null");
                }
                if (_repo.Read(id) != null)
                {
                    throw new InvalidOperationException("Was not deleted");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return deletedBooking;
        }
        #endregion

        #region checks
        private bool HasNullValue(BookingOrder bo)
        {
            return bo == null || bo.Product == null || bo.User == null;
        }

        private bool IsIdValid(BookingOrder createdBooking)
        {
            return createdBooking.Id <= 0;
        }

        private bool IsBookingTimeColiding(BookingOrder booking)
        {
            IEnumerable<BookingOrder> bookings = _repo.ReadAll();

            return bookings.Where(bo => bo.Id != booking.Id).Where(bo => bo.Product.Id == booking.Product.Id)
                .Where(bo =>
               (bo.StartTimeOfBooking.Ticks + 1 <= booking.StartTimeOfBooking.Ticks + 1 && bo.EndTimeOfBooking.Ticks - 1 >= booking.StartTimeOfBooking.Ticks + 1) ||
               (bo.StartTimeOfBooking.Ticks + 1 <= booking.EndTimeOfBooking.Ticks - 1 && bo.EndTimeOfBooking.Ticks - 1 >= booking.EndTimeOfBooking.Ticks - 1) ||
               (booking.StartTimeOfBooking.Ticks + 1 <= bo.StartTimeOfBooking.Ticks + 1 && booking.EndTimeOfBooking.Ticks - 1 >= bo.EndTimeOfBooking.Ticks - 1))
                .FirstOrDefault() != null;
        }

        #endregion
    }
}
