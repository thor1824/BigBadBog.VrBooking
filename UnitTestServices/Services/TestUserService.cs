using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VrBooking.Core;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.Entity;

namespace UnitTest.Services
{
    [TestClass]
    public class TestUserService
    {
        Mock<IRepository<User>> mockRepo;
        IUserService service;

        [TestInitialize]
        public void Setup()
        {
            mockRepo = new Mock<IRepository<User>>();
            service = new UserService(mockRepo.Object);
        }

        #region Tests UsesService.Create(User)

        [TestMethod]
        public void TestUserServiceCreate()
        {
            User userBeforeCreate = new User()
            {
                Address = "TomatVej 42",
                PhoneNumber = "12345678",
                Name = "Johhni",
                SchoolMail = "Joohni@easv365.dk"
            };

            User userAfterCreate = new User()
            {
                Address = userBeforeCreate.Address,
                PhoneNumber = userBeforeCreate.PhoneNumber,
                SchoolMail = userBeforeCreate.SchoolMail,
                Name = userBeforeCreate.Name,
                Id = 1
            };

            mockRepo.Setup(repo => repo.Create(userBeforeCreate)).Returns(userAfterCreate);

            Assert.IsTrue(service.Create(userBeforeCreate) != null);
            mockRepo.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
        }


        [DataRow("nej", "12345678", "jegErDum@easv365.dk", null)]
        [DataRow("nej", "12345678", "jegErDum@easv365.dk", "")]
        [TestMethod]
        public void TestUserServiceCreateInvalidDataExceptionName(string address, string phoneNumber, string email, string name)
        {
            User userBeforeCreate = new User()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                SchoolMail = email,
                Name = name
            };
            User userAfterCreate = new User
            {
                Id = 1,
                SchoolMail = phoneNumber,
                PhoneNumber = email,
                Address = address,
                Name = name
            };

            mockRepo.Setup(repo => repo.Create(userBeforeCreate)).Returns(userAfterCreate);

            Assert.ThrowsException<InvalidDataException>(() => service.Create(userBeforeCreate));
            mockRepo.Verify(x => x.Create(It.IsAny<User>()), Times.Never);


        }

        [DataRow("nej", "12345678", "jegErDum@easv365.dk", "ole", null)]
        [DataRow("nej", "12345678", "jegErDum@easv365.dk", "ole", -2)]
        [TestMethod]
        public void TestUserServiceCreateInvalidOperationExceptionAssignedInvalidID(string address, string phoneNumber, string email, string name, long id)
        {
            User userBeforeCreate = new User()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                SchoolMail = email,
                Name = name
            };

            User userAfterCreate = new User()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                SchoolMail = email,
                Name = name,
                Id = id
            };

            mockRepo.Setup(repo => repo.Create(userBeforeCreate)).Returns(userAfterCreate);

            Assert.ThrowsException<InvalidOperationException>(() => service.Create(userBeforeCreate));
            mockRepo.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
        }



        [DataRow("nej", "12345678", "jegErDum@easv36", "ole", 2)]
        [DataRow("nej", "12345678", "", "ole", 1)]
        [TestMethod]
        public void TestUserServiceCreateInvalidDataExceptionEmail(string address, string phoneNumber, string email, string name, long id)
        {
            User user = new User()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                SchoolMail = email,
                Name = name,
            };

            User user2 = new User()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                SchoolMail = email,
                Name = name,
                Id = id
            };

            mockRepo.Setup(repo => repo.Create(user)).Returns(user);
            mockRepo.Setup(repo => repo.Create(user2)).Returns(user2);

            Assert.ThrowsException<InvalidDataException>(() => service.Create(user));
            Assert.ThrowsException<InvalidDataException>(() => service.Create(user2));
            mockRepo.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
        }

        [DataRow("nej", null, "jegErDum@easv365.dk", "ole", 2)]
        [DataRow("nej", "123456", "jegErDum@easv365.dk", "ole", 1)]
        [DataRow("nej", "ABCDEFGH", "jegErDum@easv365.dk", "ole", 1)]
        [DataRow("nej", "1234srgsg56", "jegErDum@easv365.dk", "ole", 5)]
        [TestMethod]
        public void TestUserServiceCreateInvalidDataExceptionPhoneNumber(string address, string phoneNumber, string email, string name, long id)
        {
            User user = new User()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                SchoolMail = email,
                Name = name,

            };

            User user2 = new User()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                SchoolMail = email,
                Name = name,
                Id = id
            };

            mockRepo.Setup(repo => repo.Create(user)).Returns(user2);

            Assert.ThrowsException<InvalidDataException>(() => service.Create(user));
            mockRepo.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
        }
        #endregion

        #region test UserService.Read(int)
        [TestMethod]
        public void TestUserServiceRead()
        {
            long id = 1;

            User user = new User()
            {
                Address = "luvh",
                PhoneNumber = "sg",
                SchoolMail = "eg",
                Name = "dfbdfs",
                Id = 1
            };

            User wrongUser = new User()
            {
                Address = "dv",
                PhoneNumber = "rgs",
                SchoolMail = "grs",
                Name = "fh",
                Id = 2
            };
            User userNull = null;

            //Testest if UserService.Read(int) works as entended
            mockRepo.Setup(repo => repo.Read(id)).Returns(user);

            Assert.IsTrue(service.Read(id).Id == id);

            mockRepo.Verify(x => x.Read(id), Times.Once);

            //Testest if UserService.Read(int) Throws exception if returning worng entity
            mockRepo.Setup(repo => repo.Read(id)).Returns(wrongUser);

            Assert.ThrowsException<InvalidOperationException>(() => service.Read(id));

            //Testest if UserService.Read(int) Throws exception if entity does nopt exist
            mockRepo.Setup(repo => repo.Read(id)).Returns(userNull);

            Assert.ThrowsException<InvalidDataException>(() => service.Read(id));
        }
        #endregion

        #region test UserService.ReadAll()
        [TestMethod]
        public void TestUserServiceReadAll()
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
            mockRepo.Setup(repo => repo.ReadAll()).Returns(users);

            service.ReadAll();
            mockRepo.Verify(x => x.ReadAll(), Times.Once);
        }
        #endregion

        #region test UserService.Update(User)
        [TestMethod]
        public void TestUserServiceUpdate()
        {
            User user = new User
            {
                Id = 1,
                Address = "Tomatvej 42",
                Name = "Ole",
                PhoneNumber = "28282828",
                SchoolMail = "Ole28@easv365.dk"
            };
            User updatedUser = new User
            {
                Id = 1,
                Address = "Tomatvej 42",
                Name = "John",
                PhoneNumber = "28282828",
                SchoolMail = "Ole28@easv365.dk"
            };

            //Testest if UserService.Update(User) works as entended
            mockRepo.Setup(x => x.Update(user)).Returns(updatedUser);
            mockRepo.Setup(x => x.Read(user.Id)).Returns(new Queue<User>(new[] { user, updatedUser }).Dequeue);

            Assert.IsTrue(service.Update(user).Equals(updatedUser));

            mockRepo.Verify(x => x.Update(user), Times.Once);
            mockRepo.Verify(x => x.Read(user.Id), Times.Exactly(2));
        }

        [TestMethod]
        public void TestUserServiceUpdateInvalidDataExceptionID()
        {
            User user = new User
            {
                Id = 1,
                Address = "Tomatvej 42",
                Name = "Ole",
                PhoneNumber = "28282828",
                SchoolMail = "Ole28@easv365.dk"
            };
            User updatedUser = new User
            {
                Id = 1,
                Address = "Tomatvej 42",
                Name = "John",
                PhoneNumber = "28282828",
                SchoolMail = "Ole28@easv365.dk"
            };

            User userNull = null;

            //Testest if UserService.Update(User) throws exception if the user to be updated does not exist
            mockRepo.Setup(x => x.Update(user)).Returns(user);
            mockRepo.Setup(x => x.Read(user.Id)).Returns(userNull);

            Assert.ThrowsException<InvalidDataException>(() => service.Update(user));
            mockRepo.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
        }

        [TestMethod]
        public void TestUserServiceUpdateInvalidOperationExceptionReturnsNull()
        {
            User user = new User
            {
                Id = 1,
                Address = "Tomatvej 42",
                Name = "Ole",
                PhoneNumber = "28282828",
                SchoolMail = "Ole28@easv365.dk"
            };
            User updatedUser = new User
            {
                Id = 1,
                Address = "Tomatvej 42",
                Name = "John",
                PhoneNumber = "28282828",
                SchoolMail = "Ole28@easv365.dk"
            };

            User userNull = null;

            //Testest if UserService.Update(User) throws exception if not works as entended
            mockRepo.Setup(x => x.Update(user)).Returns(userNull);
            mockRepo.Setup(x => x.Read(user.Id)).Returns(new Queue<User>(new[] { user, updatedUser }).Dequeue);

            Assert.ThrowsException<InvalidOperationException>(() => service.Update(user));
            mockRepo.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public void TestUserServiceUpdateInvalidOperationExceptionDoesNotUpdate()
        {
            User user = new User
            {
                Id = 1,
                Address = "Tomatvej 42",
                Name = "Ole",
                PhoneNumber = "28282828",
                SchoolMail = "Ole28@easv365.dk"
            };
            User updatedUser = new User
            {
                Id = 1,
                Address = "Tomatvej 42",
                Name = "John",
                PhoneNumber = "28282828",
                SchoolMail = "Ole28@easv365.dk"
            };


            //Testest if UserService.Update(User) throws exception if not works as entended
            mockRepo.Setup(x => x.Update(user)).Returns(user);
            mockRepo.Setup(x => x.Read(user.Id)).Returns(new Queue<User>(new[] { user, user }).Dequeue);

            Assert.ThrowsException<InvalidOperationException>(() => service.Update(user));
            mockRepo.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
        }
        #endregion

        #region test UserService.Delete(User)
        [TestMethod]
        public void TestUserServiceDelete()
        {
            User user = new User()
            {
                Address = "dv",
                PhoneNumber = "rgs",
                SchoolMail = "grs",
                Name = "fh",
                Id = 2
            };

            User userNull = null;

            // testest if UserService.Delete(id) works as entended
            mockRepo.Setup(x => x.Delete(user)).Returns(user);
            mockRepo.Setup(x => x.Read(user.Id)).Returns(new Queue<User>(new[] { user, null }).Dequeue);

            Assert.IsTrue(service.Delete(user.Id).Equals(user));

            mockRepo.Verify(x => x.Delete(user), Times.Once);
            mockRepo.Verify(x => x.Read(user.Id), Times.Exactly(2));
        }

        [TestMethod]
        public void TestUserServiceDeleteInvalidOperationExceptionReturnsNull()
        {
            User user = new User()
            {
                Address = "dv",
                PhoneNumber = "rgs",
                SchoolMail = "grs",
                Name = "fh",
                Id = 2
            };

            User userNull = null;

            // testest if UserService.Delete(id) thorws exception if return null
            mockRepo.Setup(x => x.Delete(user)).Returns(userNull);
            mockRepo.Setup(x => x.Read(user.Id)).Returns(new Queue<User>(new[] { user, null }).Dequeue);

            Assert.ThrowsException<InvalidOperationException>(() => service.Delete(user.Id));
            mockRepo.Verify(x => x.Delete(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public void TestUserServiceDeleteInvalidOperationExceptionDoesNotDelete()
        {
            User user = new User()
            {
                Address = "dv",
                PhoneNumber = "rgs",
                SchoolMail = "grs",
                Name = "fh",
                Id = 2
            };

            User userNull = null;

            // testest if UserService.Delete(id) throws exception when not working as entended
            mockRepo.Setup(x => x.Delete(user)).Returns(user);
            mockRepo.Setup(x => x.Read(user.Id)).Returns(new Queue<User>(new[] { user, user }).Dequeue);

            Assert.ThrowsException<InvalidOperationException>(() => service.Delete(user.Id));
            mockRepo.Verify(x => x.Delete(It.IsAny<User>()), Times.Once);
        }

        #endregion
    }
}