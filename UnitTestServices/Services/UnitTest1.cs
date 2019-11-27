using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VrBooking.Core;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.Entity;
using VrBooking.Infrastructure;

namespace UnitTest.Services
{
    [TestClass]
    public class TestUserService
    {
        [DataRow("nej", "12345678", "jegErDum@easv365.dk", null)]
        [DataRow("nej", "12345678", "jegErDum@easv365.dk", "")]
        [TestMethod]
        public void TestNoUserNameThrowsException(string address, string phoneNumber, string email, string name)
        {
            Mock<IRepository<User>> mockRepo = new Mock<IRepository<User>>();

            var user = new User()
            {
                Address = address, PhoneNumber = phoneNumber, SchoolMail = email, Name = name
            };
            var user1 = new User
            {
                Id = 1, SchoolMail = user.SchoolMail, PhoneNumber = user.PhoneNumber, Address = user.Address,
                Name = name
            };

            mockRepo.Setup(repo => repo.Create(user)).Returns(user1);

            IUserService servise = new UserService(mockRepo.Object);

           
            Assert.ThrowsException<InvalidDataException>(() => servise.Create(user1));
            mockRepo.Verify(x => x.Create(It.IsAny<User>()), Times.Never);


        }

        [DataRow("nej", "12345678", "jegErDum@easv365.dk", "ole", null)]
        [DataRow("nej", "12345678", "jegErDum@easv365.dk", "ole", -2)]
        [TestMethod]
        public void TestForIdIsNullOrNegative(string address, string phoneNumber, string email, string name, long id)
        {
            Mock<IRepository<User>> mockRepo = new Mock<IRepository<User>>();
            var user = new User()
            {
                Address = address, PhoneNumber = phoneNumber, SchoolMail = email, Name = name
            };

            var user2 = new User()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                SchoolMail = email,
                Name = name,
                Id = id
            };

            mockRepo.Setup(repo => repo.Create(user)).Returns(user2);

            IUserService service = new UserService(mockRepo.Object);

            Assert.ThrowsException<InvalidDataException>(() => service.Create(user));
            mockRepo.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
        }

        
        
        [DataRow("nej", "12345678", "jegErDum@easv36", "ole", 2)]
        [DataRow("nej", "12345678", "", "ole", 1)]
        [TestMethod]
        public void TestForEmailIsASchoolMail(string address, string phoneNumber, string email, string name, long id)
        {
            Mock<IRepository<User>> mockRepo = new Mock<IRepository<User>>();
            var user = new User()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                SchoolMail = email,
                Name = name,

            };

            var user2 = new User()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                SchoolMail = email,
                Name = name,
                Id = id
            };

            mockRepo.Setup(repo => repo.Create(user)).Returns(user);
            mockRepo.Setup(repo => repo.Create(user2)).Returns(user2);

            IUserService service = new UserService(mockRepo.Object);

            Assert.ThrowsException<InvalidDataException>(() => service.Create(user));
            Assert.ThrowsException<InvalidDataException>(() => service.Create(user2));
            mockRepo.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
        }

        [DataRow("nej", null, "jegErDum@easv365.dk", "ole", 2)]
        [DataRow("nej", "123456", "jegErDum@easv365.dk", "ole", 1)]
        [DataRow("nej", "1234srgsg56", "jegErDum@easv365.dk", "ole", 5)]
        [TestMethod]
        public void TestForPhoneDigits(string address, string phoneNumber, string email, string name, long id)
        {
            Mock<IRepository<User>> mockRepo = new Mock<IRepository<User>>();
            var user = new User()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                SchoolMail = email,
                Name = name,

            };

            var user2 = new User()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                SchoolMail = email,
                Name = name,
                Id = id
            };


            mockRepo.Setup(repo => repo.Create(user)).Returns(user2);

            IUserService service = new UserService(mockRepo.Object);

            Assert.ThrowsException<InvalidDataException>(() => service.Create(user));
            mockRepo.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
        }


        [TestMethod]
        public void TestForValidUser()
        {
            Mock<IRepository<User>> mockRepo = new Mock<IRepository<User>>();
            User user = new User()
            {
                Address = "TomatVej 42",
                PhoneNumber = "12345678",
                Name = "Johhni",
                SchoolMail = "Joohni@easv365.dk"
            };

            var user2 = new User()
            {
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                SchoolMail = user.SchoolMail,
                Name = user.Name,
                Id = 1
            };

            mockRepo.Setup(repo => repo.Create(user)).Returns(user2);

            IUserService service = new UserService(mockRepo.Object);

            Assert.IsTrue(service.Create(user) != null);
            mockRepo.Verify(x => x.Create(It.IsAny<User>()), Times.Once);

        }

        [TestMethod]
        public void CreateUserCallCount()
        {

        }

        [TestMethod]
        public void TestFindUserById1()
        {
            Mock<IRepository<User>> mockRepo = new Mock<IRepository<User>>();
            long id = 1;

            var user = new User()
            {
                Address = "luvh",
                PhoneNumber = "sg",
                SchoolMail = "eg",
                Name = "dfbdfs",
                Id = 1
            };

            
            mockRepo.Setup(repo => repo.Read(id)).Returns(user);

            IUserService service = new UserService(mockRepo.Object);
            
            Assert.IsTrue(service.Read(id) == user);
            mockRepo.Verify(x => x.Read(id), Times.Once);
        }
        [TestMethod]
        public void TestFindUserByIdFals()
        {
            Mock<IRepository<User>> mockRepo = new Mock<IRepository<User>>();


            var user = new User()
            {
                Address = "lfh",
                PhoneNumber = "fskh",
                SchoolMail = "ldfgih",
                Name = "dg",
                Id = 1
            };
            var user2 = new User()
            {
                Address = "dv",
                PhoneNumber = "rgs",
                SchoolMail = "grs",
                Name = "fh",
                Id = 2
            };

            mockRepo.Setup(repo => repo.Create(user)).Returns(user);

            IUserService service = new UserService(mockRepo.Object);

            Assert.IsFalse(service.Read(user.Id) == user2);
            
        }

        [TestMethod]
        public void TestIfReadAllIsCalledMoreThenOnce()
        {
            IEnumerable<User> users = new List<User>()
            {
                new User()
                {
                    Address = "dv",
                    PhoneNumber = "rgs",
                    SchoolMail = "grs",
                    Name = "fh",
                    Id = 2
                }
            };
            Mock<IRepository<User>> mockRepo = new Mock<IRepository<User>>();
            mockRepo.Setup(repo => repo.ReadAll()).Returns(users);
            IUserService service = new UserService(mockRepo.Object);

            service.ReadAll();
            mockRepo.Verify(x => x.ReadAll(), Times.Once);
        }


        [TestMethod]
        public void TestForAnUpdatedUser()
        {
            User entity = new User
            {
                Address = "Tomatvej 42",
                Name = "Ole",
                PhoneNumber = "28282828",
                SchoolMail = "Ole28@easv365.dk"
            };

            Mock<IRepository<User>> mockRepo = new Mock<IRepository<User>>();
            mockRepo.Setup(x => x.Update(entity)).Returns(entity);

            IUserService service = new UserService(mockRepo.Object);
            service.Update(entity);

            mockRepo.Verify(x => x.Update(entity), Times.Once);
        }

        [TestMethod]
        public void TestForDeletedUser()
        {
            User per = new User()
            {
                Address = "dv",
                PhoneNumber = "rgs",
                SchoolMail = "grs",
                Name = "fh",
                Id = 2
            };
            
            Mock<IRepository<User>> mockRepo = new Mock<IRepository<User>>();
            mockRepo.Setup(x => x.Read(2)).Returns(per);

            IUserService service = new UserService(mockRepo.Object);
            service.Delete(per.Id);

            mockRepo.Verify(x => x.Delete(per), Times.Once);
        }
    }



}
