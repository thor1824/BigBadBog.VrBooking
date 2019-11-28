using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core;

using VrBooking.Core.Entity;

namespace TestServices.Services
{
    [TestClass]
    class TestBookingOrderService
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

        [TestMethod]
        public void TestBookingOrderServiceCreate() 
        {
            BookingOrder bo = new BookingOrder() 
            { 
                
            };
            _mockRepo.Setup(x => x.Create(null)).Returns(null);
        }

        #endregion

        #region Test BookingOrderService.Read(int)

        [TestMethod]
        public void TestBookingOrderServiceRead() { }

        #endregion

        #region Test BookingOrderService.ReadAll()

        [TestMethod]
        public void TestBookingOrderServiceReadAll() { }

        #endregion

        #region Test BookingOrderService.Update(BookingOrder)

        [TestMethod]
        public void TestBookingOrderServiceUpdate() { }

        #endregion

        #region Test BookingOrderService.Delete(BookingOrder)

        [TestMethod]
        public void TestBookingOrderServiceDelete() { }

        #endregion
    }
}
