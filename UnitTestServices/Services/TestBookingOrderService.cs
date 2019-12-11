using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.DomainServices;
using VrBooking.Core.Entity;

namespace TestServices.Services
{
    [TestClass]
    public class TestBookingOrderService
    {
        private Mock<IRepository<BookingOrder>> _mockRepo;
        private IBookingOrderService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IRepository<BookingOrder>>();
            _service = new BookingOrderService(_mockRepo.Object);
        }

        #region Test BookingOrderService.Create()

        [DataRow(1, 1, 3, 4, 5, 1, 1)]
        [DataRow(1, 1, 3, 3, 5, 1, 1)]
        [DataRow(1, 1, 2, 1, 2, 2, 1)]
        [TestMethod]
        public void TestBookingOrderServiceCreate(long id, int addStart, int addEnd, int oldStart, int oldEnd, int prodid, int prod2id)
        {
            DateTime start = DateTime.Now.AddHours(addStart);
            DateTime end = DateTime.Now.AddHours(addEnd);
            UserInfo user = new UserInfo();
            Product product = new Product()
            {
                Id = prodid
            };
            Product product2 = new Product()
            {
                Id = prod2id
            };

            IEnumerable<BookingOrder> bookingOrders = new List<BookingOrder>()
            {
                 new BookingOrder()
                 {
                    Id = id + 1,
                    User = user,
                    Product = product2,
                    StartTimeOfBooking = DateTime.Now.AddHours(oldStart),
                    EndTimeOfBooking = DateTime.Now.AddHours(oldEnd)
                 }

            };

            BookingOrder boBeforeCreated = new BookingOrder()
            {
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };

            BookingOrder boAfterCreated = new BookingOrder()
            {
                Id = id,
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };
            _mockRepo.Setup(repo => repo.ReadAll()).Returns(bookingOrders);
            _mockRepo.Setup(x => x.Create(boBeforeCreated)).Returns(boAfterCreated);
            Assert.IsTrue(_service.Create(boBeforeCreated) != null);
            _mockRepo.Verify(x => x.Create(It.IsAny<BookingOrder>()), Times.Once);
        }

        [DataRow(1, 2, 3, 2, 3)]
        [DataRow(1, 2, 3, 1, 2)]
        [TestMethod]
        public void TestBookingOrderServiceCreateInvalidDataExceptionBookingTimeCollision(long id, int addStart, int addEnd, int oldStart, int oldEnd)
        {
            DateTime start = DateTime.Now.AddHours(addStart);
            DateTime end = DateTime.Now.AddHours(addEnd);
            UserInfo user = new UserInfo();
            Product product = new Product();

            IEnumerable<BookingOrder> bookingOrders = new List<BookingOrder>()
            {
                 new BookingOrder()
                 {
                    Id = id+1,
                    User = user,
                    Product = product,
                    StartTimeOfBooking = DateTime.Now.AddHours(oldStart),
                    EndTimeOfBooking = DateTime.Now.AddHours(oldEnd)
                 }

            };
            BookingOrder boBeforeCreated = new BookingOrder()
            {
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };

            BookingOrder boAfterCreated = new BookingOrder()
            {
                Id = id,
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };
            _mockRepo.Setup(repo => repo.ReadAll()).Returns(bookingOrders);
            _mockRepo.Setup(x => x.Create(boBeforeCreated)).Returns(boAfterCreated);
            Assert.ThrowsException<InvalidDataException>(() => _service.Create(boBeforeCreated));
            _mockRepo.Verify(x => x.Create(It.IsAny<BookingOrder>()), Times.Never);
        }

        [DataRow(0)]
        [DataRow(-1)]
        [TestMethod]
        public void TestBookingOrderServiceCreateInvalidOperationExceptionID(long id)
        {
            DateTime start = DateTime.Now.AddHours(1);
            DateTime end = DateTime.Now.AddHours(3);
            UserInfo user = new UserInfo();
            Product product = new Product();

            BookingOrder boBeforeCreated = new BookingOrder()
            {
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };

            BookingOrder boAfterCreated = new BookingOrder()
            {
                Id = id,
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };

            _mockRepo.Setup(x => x.Create(boBeforeCreated)).Returns(boAfterCreated);
            Assert.ThrowsException<InvalidOperationException>(() => _service.Create(boBeforeCreated));
            _mockRepo.Verify(x => x.Create(It.IsAny<BookingOrder>()), Times.Once);
        }

        [DataRow(1, true, false, false)]
        [DataRow(1, false, true, false)]
        [DataRow(1, false, false, true)]
        [DataTestMethod]
        public void TestBookingOrderServiceCreateInvalidOperationExceptionContainsNullUser(long id, bool isUserNull, bool isProductNull, bool isBookingNull)
        {
            DateTime start = DateTime.Now.AddHours(1);
            DateTime end = DateTime.Now.AddHours(3);
            UserInfo user = isUserNull ? null : new UserInfo();
            Product product = isProductNull ? null : new Product();

            BookingOrder boBeforeCreated = isBookingNull ? null : new BookingOrder()
            {
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };

            BookingOrder boAfterCreated = isBookingNull ? null : new BookingOrder()
            {
                Id = id,
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };

            _mockRepo.Setup(x => x.Create(boBeforeCreated)).Returns(boAfterCreated);
            Assert.ThrowsException<InvalidDataException>(() => _service.Create(boBeforeCreated));
            _mockRepo.Verify(x => x.Create(It.IsAny<BookingOrder>()), Times.Never);
        }

        [DataRow(1, 2, 1)]
        [DataRow(1, -2, 1)]
        [DataRow(1, 2, -1)]
        [TestMethod]
        public void TestBookingOrderServiceCreateInvalidDataExceptionDate(long id, int addStart, int addEnd)
        {
            DateTime start = DateTime.Now.AddHours(addStart);
            DateTime end = DateTime.Now.AddHours(addEnd);
            UserInfo user = new UserInfo();
            Product product = new Product();

            BookingOrder boBeforeCreated = new BookingOrder()
            {
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };

            BookingOrder boAfterCreated = new BookingOrder()
            {
                Id = id,
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };

            _mockRepo.Setup(x => x.Create(boBeforeCreated)).Returns(boAfterCreated);
            Assert.ThrowsException<InvalidDataException>(() => _service.Create(boBeforeCreated));
            _mockRepo.Verify(x => x.Create(It.IsAny<BookingOrder>()), Times.Never);
        }
        #endregion

        #region Test BookingOrderService.Read(int)

        [TestMethod]
        public void TestBookingOrderServiceRead() { }

        #endregion

        #region Test BookingOrderService.ReadAll()
        [DataRow(1, 2, 3)]
        [TestMethod]
        public void TestBookingOrderServiceReadAll(long id, int addStart, int addEnd)
        {
            DateTime start = DateTime.Now.AddHours(addStart);
            DateTime end = DateTime.Now.AddHours(addEnd);
            UserInfo user = new UserInfo();
            Product product = new Product();

            IEnumerable<BookingOrder> users = new List<BookingOrder>
            {
                 new BookingOrder()
                 {
                    Id = id,
                    User = user,
                    Product = product,
                    StartTimeOfBooking = start,
                    EndTimeOfBooking = end
                 }

            };
            _mockRepo.Setup(repo => repo.ReadAll()).Returns(users);
            _service.ReadAll();
            _mockRepo.Verify(x => x.ReadAll(), Times.Once);
        }

        #endregion

        #region Test BookingOrderService.Update(BookingOrder)
        [DataRow(1, 2, 3, 3, 4, 1, 1)]
        [DataRow(1, 2, 3, 4, 5, 1, 1)]
        [DataRow(1, 2, 3, 2, 4, 1, 2)]
        [DataRow(1, 2, 3, 2, 3, 1, 2)]
        [DataRow(1, 2, 3, 1, 2, 1, 1)]
        [TestMethod]
        public void TestBookingOrderServiceUpdate(long id, int oldStart, int oldEnd, int newStart, int newEnd, int prod1Id, int prod2Id)
        {

            UserInfo user = new UserInfo();
            Product product = new Product() { Id = prod1Id };
            Product product2 = new Product() { Id = prod2Id };
            DateTime cal = DateTime.Now;
            DateTime start = new DateTime(cal.Year + 1, cal.Month, cal.Day, 10 + oldStart, 0, 0);
            DateTime end = new DateTime(cal.Year + 1, cal.Month, cal.Day, 10 + oldEnd, 0, 0);
            DateTime start2 = new DateTime(cal.Year + 1, cal.Month, cal.Day, 10 + newStart, 0, 0);
            DateTime end2 = new DateTime(cal.Year + 1, cal.Month, cal.Day, 10 + newEnd, 0, 0);

            BookingOrder oldBooking = new BookingOrder()
            {
                Id = id,
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };

            BookingOrder updatedBooking = new BookingOrder()
            {
                Id = id,
                User = user,
                Product = product,
                StartTimeOfBooking = start2,
                EndTimeOfBooking = end2
            };

            IEnumerable<BookingOrder> bookingOrders = new List<BookingOrder>()
            {
                 new BookingOrder()
                 {
                    Id = id + 1,
                    User = user,
                    Product = product2,
                    StartTimeOfBooking = start,
                    EndTimeOfBooking = end
                 },
                 oldBooking

            };

            _mockRepo.Setup(repo => repo.ReadAll()).Returns(bookingOrders);
            _mockRepo.Setup(x => x.Read(id)).Returns(updatedBooking);
            _mockRepo.Setup(x => x.Update(updatedBooking)).Returns(updatedBooking);
            Assert.IsTrue(_service.Update(updatedBooking) != null);
            _mockRepo.Verify(x => x.Update(It.IsAny<BookingOrder>()), Times.Once);
        }

        [DataRow(1, 2, 3, 3, 4, 3, 4, 1)]
        [DataRow(1, 5, 6, 3, 4, 3, 4, 1)]
        [DataRow(1, 1, 2, 1, 6, 3, 4, 1)]
        [DataRow(1, 1, 2, 1, 5, 4, 7, 1)]
        [DataRow(1, 1, 2, 6, 9, 4, 7, 1)]
        [TestMethod]
        public void TestBookingOrderServiceUpdateInvalidDataExceptionTimeCollision(long id, int oldStart, int oldEnd, int newStart, int newEnd, int bStart3, int bEnd3, int prod1Id)
        {

            UserInfo user = new UserInfo();
            Product product = new Product() { Id = prod1Id };
            DateTime cal = DateTime.Now;
            DateTime start = new DateTime(cal.Year + 1, cal.Month, cal.Day, 10 + oldStart, 0, 0);
            DateTime end = new DateTime(cal.Year + 1, cal.Month, cal.Day, 10 + oldEnd, 0, 0);
            DateTime start2 = new DateTime(cal.Year + 1, cal.Month, cal.Day, 10 + newStart, 0, 0);
            DateTime end2 = new DateTime(cal.Year + 1, cal.Month, cal.Day, 10 + newEnd, 0, 0);
            DateTime start3 = new DateTime(cal.Year + 1, cal.Month, cal.Day, 10 + bStart3, 0, 0);
            DateTime end3 = new DateTime(cal.Year + 1, cal.Month, cal.Day, 10 + bEnd3, 0, 0);

            BookingOrder oldBooking = new BookingOrder()
            {
                Id = id,
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };

            BookingOrder updatedBooking = new BookingOrder()
            {
                Id = id,
                User = user,
                Product = product,
                StartTimeOfBooking = start2,
                EndTimeOfBooking = end2
            };

            IEnumerable<BookingOrder> bookingOrders = new List<BookingOrder>()
            {
                 new BookingOrder()
                 {
                    Id = id + 1,
                    User = user,
                    Product = product,
                    StartTimeOfBooking = start3,
                    EndTimeOfBooking = end3
                 },
                 oldBooking

            };
            _mockRepo.Setup(repo => repo.ReadAll()).Returns(bookingOrders);
            _mockRepo.Setup(x => x.Read(id)).Returns(updatedBooking);
            _mockRepo.Setup(x => x.Update(updatedBooking)).Returns(updatedBooking);
            Assert.ThrowsException<InvalidDataException>(() => _service.Update(updatedBooking));
            _mockRepo.Verify(x => x.Update(It.IsAny<BookingOrder>()), Times.Never);
        }


        [DataRow(1, true, false, false)]
        [DataRow(1, false, true, false)]
        [DataRow(1, false, false, true)]
        [DataTestMethod]
        public void TestBookingOrderServiceUpdateInvalidOperationExceptionContainsNullUser(long id, bool isUserNull, bool isProductNull, bool isBookingNull)
        {
            DateTime start = DateTime.Now.AddHours(1);
            DateTime end = DateTime.Now.AddHours(3);
            UserInfo user = isUserNull ? null : new UserInfo();
            Product product = isProductNull ? null : new Product();

            BookingOrder boBeforeCreated = isBookingNull ? null : new BookingOrder()
            {
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };

            BookingOrder boAfterCreated = isBookingNull ? null : new BookingOrder()
            {
                Id = id,
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };

            _mockRepo.Setup(x => x.Update(boBeforeCreated)).Returns(boAfterCreated);
            Assert.ThrowsException<InvalidDataException>(() => _service.Create(boBeforeCreated));
            _mockRepo.Verify(x => x.Update(It.IsAny<BookingOrder>()), Times.Never);
        }

        [DataRow(1, 2, 1)]
        [DataRow(1, -2, 1)]
        [DataRow(1, 2, -1)]
        [TestMethod]
        public void TestBookingOrderServiceUpdateInvalidDataExceptionDate(long id, int addStart, int addEnd)
        {
            DateTime start = DateTime.Now.AddHours(addStart);
            DateTime end = DateTime.Now.AddHours(addEnd);
            UserInfo user = new UserInfo();
            Product product = new Product();

            BookingOrder boBeforeCreated = new BookingOrder()
            {
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };

            BookingOrder boAfterCreated = new BookingOrder()
            {
                Id = id,
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };

            _mockRepo.Setup(x => x.Update(boBeforeCreated)).Returns(boAfterCreated);
            Assert.ThrowsException<InvalidDataException>(() => _service.Update(boBeforeCreated));
            _mockRepo.Verify(x => x.Update(It.IsAny<BookingOrder>()), Times.Never);
        }

        #endregion

        #region Test BookingOrderService.Delete(BookingOrder)

        [DataRow(1, 1, 2)]
        [TestMethod]
        public void TestBookingOrderServiceDelete(long id, int addStart, int addEnd)
        {
            DateTime start = DateTime.Now.AddHours(addStart);
            DateTime end = DateTime.Now.AddHours(addEnd);
            UserInfo user = new UserInfo();
            Product product = new Product();

            BookingOrder boBeforeCreated = new BookingOrder()
            {
                Id = id,
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };
            _mockRepo.Setup(x => x.Read(id)).Returns(new Queue<BookingOrder>(new[] { boBeforeCreated, null }).Dequeue);
            _mockRepo.Setup(x => x.Delete(boBeforeCreated)).Returns(boBeforeCreated);
            Assert.IsTrue(_service.Delete(id) != null);
            _mockRepo.Verify(x => x.Delete(It.IsAny<BookingOrder>()), Times.Once);
        }

        [DataRow(1, 1, 2)]
        [TestMethod]
        public void TestBookingOrderServiceDeleteInvalidOparationExceptionReturnNull(long id, int addStart, int addEnd)
        {
            DateTime start = DateTime.Now.AddHours(addStart);
            DateTime end = DateTime.Now.AddHours(addEnd);
            UserInfo user = new UserInfo();
            Product product = new Product();

            BookingOrder boBeforeCreated = new BookingOrder()
            {
                Id = id,
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };
            _mockRepo.Setup(x => x.Read(id)).Returns(boBeforeCreated);
            _mockRepo.Setup(x => x.Delete(boBeforeCreated)).Returns(new Queue<BookingOrder>(new[] { boBeforeCreated, null }).Dequeue);
            Assert.ThrowsException<InvalidOperationException>(() => _service.Delete(id));
            _mockRepo.Verify(x => x.Delete(It.IsAny<BookingOrder>()), Times.Once);
        }

        [DataRow(1, 1, 2)]
        [TestMethod]
        public void TestBookingOrderServiceDeleteInvalidOparationExceptionNotDeleted(long id, int addStart, int addEnd)
        {
            DateTime start = DateTime.Now.AddHours(addStart);
            DateTime end = DateTime.Now.AddHours(addEnd);
            UserInfo user = new UserInfo();
            Product product = new Product();

            BookingOrder boBeforeCreated = new BookingOrder()
            {
                Id = id,
                User = user,
                Product = product,
                StartTimeOfBooking = start,
                EndTimeOfBooking = end
            };
            _mockRepo.Setup(x => x.Read(id)).Returns(boBeforeCreated);
            _mockRepo.Setup(x => x.Delete(boBeforeCreated)).Returns(boBeforeCreated);
            Assert.ThrowsException<InvalidOperationException>(() => _service.Delete(id));
            _mockRepo.Verify(x => x.Delete(It.IsAny<BookingOrder>()), Times.Once);
        }

        #endregion


    }
}
