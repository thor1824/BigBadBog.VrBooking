using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.Entity;
using VrBooking.Infrastructure;

namespace UnitTest.Services
{
    [TestClass]
    public class TestUserService
    {

        [TestMethod]
        public void TestNoUserNameThrowsException()
        {
            var UserService = new Mock<IUserService>();
           

        }
    }
}
