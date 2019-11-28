using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VrBooking.Core;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.Entity;

namespace TestServices.Services
{
    [TestClass]
    public class TestLoginUserService
    {
        Mock<IRepository<LoginUser>> mockRepo;
        private ILoginUserService service;

        [TestInitialize]
        public void Setup()
        {
            mockRepo = new Mock<IRepository<LoginUser>>();
            service = new LoginUserService(mockRepo.Object);
        }

        [TestMethod]
        public void TestLoginUserCreate()
        {
            LoginUser userBeforeCreate = new LoginUser()
            {
                Activated = true,
                Admin = false,
                UserName = "perPerson@easv365.dk",
                PasswordHash = new byte[] {1, 2, 3},
                PasswordSalt = new byte[] {1, 2, 3}
            };

            LoginUser userAfterCreate = new LoginUser()
            {
                PasswordHash = userBeforeCreate.PasswordHash,
                PasswordSalt = userBeforeCreate.PasswordSalt,
                UserName = userBeforeCreate.UserName,
                Activated = userBeforeCreate.Activated,
                Admin = userBeforeCreate.Admin,
                Id = 1
            };

            mockRepo.Setup(repo => repo.Create(userBeforeCreate)).Returns(userAfterCreate);
            Assert.IsTrue(service.Create(userBeforeCreate) != null);
            mockRepo.Verify(x => x.Create(It.IsAny<LoginUser>()), Times.Once);
        }



    }
}
