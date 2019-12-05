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
    public class TestUserInfoService
    {
        private Mock<IRepository<UserInfo>> _mockRepo;
        private IUserInfoService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IRepository<UserInfo>>();
            _service = new UserInfoService(_mockRepo.Object);
        }

        #region Tests UsesService.Create(User)
        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "LastName", 1)]
        [TestMethod]
        public void TestUserServiceCreate(string address, string phoneNumber, string email, string firstName, string lastName, long id)

        {
            UserInfo userBeforeCreate = new UserInfo()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };
            UserInfo userAfterCreate = new UserInfo()
            {
                Id = id,
                Email = phoneNumber,
                PhoneNumber = email,
                Address = address,
                FirstName = firstName,
                LastName = lastName
            };

            _mockRepo.Setup(repo => repo.Create(userBeforeCreate)).Returns(userAfterCreate);

            Assert.IsTrue(_service.Create(userBeforeCreate) != null);
            _mockRepo.Verify(x => x.Create(It.IsAny<UserInfo>()), Times.Once);
        }

        [DataRow("Addresse", "12345678", "email@easv365.dk", null, "LastName", 1)]
        [DataRow("Addresse", "12345678", "email@easv365.dk", "", "LastName", 1)]
        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", null, 1)]
        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "", 1)]
        [TestMethod]
        public void TestUserServiceCreateInvalidDataExceptionName(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo userBeforeCreate = new UserInfo()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };
            UserInfo userAfterCreate = new UserInfo()
            {
                Id = id,
                Email = phoneNumber,
                PhoneNumber = email,
                Address = address,
                FirstName = firstName,
                LastName = lastName
            };

            _mockRepo.Setup(repo => repo.Create(userBeforeCreate)).Returns(userAfterCreate);

            Assert.ThrowsException<InvalidDataException>(() => _service.Create(userBeforeCreate));
            _mockRepo.Verify(x => x.Create(It.IsAny<UserInfo>()), Times.Never);
        }

        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "LastName", 0)]
        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "LastName", -1)]
        [TestMethod]
        public void TestUserServiceCreateInvalidOperationExceptionAssignedInvalidID(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo userBeforeCreate = new UserInfo()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };
            UserInfo userAfterCreate = new UserInfo()
            {
                Id = id,
                Email = phoneNumber,
                PhoneNumber = email,
                Address = address,
                FirstName = firstName,
                LastName = lastName
            };

            _mockRepo.Setup(repo => repo.Create(userBeforeCreate)).Returns(userAfterCreate);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Create(userBeforeCreate));
            _mockRepo.Verify(x => x.Create(It.IsAny<UserInfo>()), Times.Once);
        }



        [DataRow("Addresse", "12345678", "emaileasv365.dk", "FirstName", "LastName", 1)]
        [DataRow("Addresse", "12345678", "email@easv36", "FirstName", "LastName", 1)]
        [DataRow("Addresse", "12345678", "", "FirstName", "LastName", 1)]
        [DataRow("Addresse", "12345678", null, "FirstName", "LastName", 1)]
        [TestMethod]
        public void TestUserServiceCreateInvalidDataExceptionEmail(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo userBeforeCreate = new UserInfo()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };
            UserInfo userAfterCreate = new UserInfo()
            {
                Id = id,
                Email = phoneNumber,
                PhoneNumber = email,
                Address = address,
                FirstName = firstName,
                LastName = lastName
            };

            _mockRepo.Setup(repo => repo.Create(userBeforeCreate)).Returns(userAfterCreate);

            Assert.ThrowsException<InvalidDataException>(() => _service.Create(userBeforeCreate));
            _mockRepo.Verify(x => x.Create(It.IsAny<UserInfo>()), Times.Never);
        }

        [DataRow("Addresse", null, "email@easv365.dk", "FirstName", "LastName", 1)]
        [DataRow("Addresse", "123456", "email@easv365.dk", "FirstName", "LastName", 1)]
        [DataRow("Addresse", "ABCDEFGH", "email@easv365.dk", "FirstName", "LastName", 1)]
        [DataRow("Addresse", "123abc56", "email@easv365.dk", "FirstName", "LastName", 1)]
        [DataRow("Addresse", "123456789", "email@easv365.dk", "FirstName", "LastName", 1)]
        [TestMethod]
        public void TestUserServiceCreateInvalidDataExceptionPhoneNumber(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo userBeforeCreate = new UserInfo()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };
            UserInfo userAfterCreate = new UserInfo()
            {
                Id = id,
                Email = phoneNumber,
                PhoneNumber = email,
                Address = address,
                FirstName = firstName,
                LastName = lastName
            };

            _mockRepo.Setup(repo => repo.Create(userBeforeCreate)).Returns(userAfterCreate);

            Assert.ThrowsException<InvalidDataException>(() => _service.Create(userBeforeCreate));
            _mockRepo.Verify(x => x.Create(It.IsAny<UserInfo>()), Times.Never);
        }
        #endregion

        #region test UserService.Read(int)
        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "LastName", 1)]
        [TestMethod]
        public void TestUserServiceRead(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo rightUser = new UserInfo()
            {
                Id = id,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            //Testest if UserService.Read(int) works as entended
            _mockRepo.Setup(repo => repo.Read(id)).Returns(rightUser);

            Assert.IsTrue(_service.Read(id).Id == id);
            _mockRepo.Verify(x => x.Read(It.IsAny<long>()), Times.Once);
        }

        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "LastName", 1)]
        [TestMethod]
        public void TestUserServiceReadInvalidOperationExceptionReturnsWrongEntity(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo rightUser = new UserInfo()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };
            UserInfo wrongUser = new UserInfo()
            {
                Id = id++,
                Email = phoneNumber + "wrong",
                PhoneNumber = email + "wrong",
                Address = address + "wrong",
                FirstName = firstName + "wrong",
                LastName = lastName + "wrong"
            };

            //Testest if UserService.Read(int) Throws exception if returning worng entity
            _mockRepo.Setup(repo => repo.Read(id)).Returns(wrongUser);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Read(id));
            _mockRepo.Verify(x => x.Read(It.IsAny<long>()), Times.Once);
        }

        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "LastName", 1)]
        [TestMethod]
        public void TestUserServiceReadInvalidDataExceptionEntityNotFound(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo rightUser = new UserInfo()
            {
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };
            UserInfo userNull = null;

            //Testest if UserService.Read(int) Throws exception if entity does nopt exist
            _mockRepo.Setup(repo => repo.Read(id)).Returns(userNull);

            Assert.ThrowsException<InvalidDataException>(() => _service.Read(id));
            _mockRepo.Verify(x => x.Read(It.IsAny<long>()), Times.Once);
        }
        #endregion

        #region test UserService.ReadAll()
        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "LastName", 1)]
        [TestMethod]
        public void TestUserServiceReadAll(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            IEnumerable<UserInfo> users = new List<UserInfo>()
            {
                 new UserInfo()
                 {
                    Id = id,
                    Email = phoneNumber,
                    PhoneNumber = email,
                    Address = address,
                    FirstName = firstName,
                    LastName = lastName
                 }

            };
            _mockRepo.Setup(repo => repo.ReadAll()).Returns(users);

            _service.ReadAll();
            _mockRepo.Verify(x => x.ReadAll(), Times.Once);
        }
        #endregion

        #region test UserService.Update(User)
        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "LastName", 1)]
        [TestMethod]
        public void TestUserServiceUpdate(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo oldUser = new UserInfo()
            {
                Id = id,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };
            UserInfo updatedUser = new UserInfo()
            {
                Id = id,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                FirstName = firstName + "Updated",
                LastName = lastName
            };

            //Testest if UserService.Update(User) works as entended
            _mockRepo.Setup(x => x.Update(updatedUser)).Returns(updatedUser);
            _mockRepo.Setup(x => x.Read(id)).Returns(new Queue<UserInfo>(new[] { oldUser, updatedUser }).Dequeue);

            Assert.IsFalse(_service.Update(updatedUser).Equals(oldUser));

            _mockRepo.Verify(x => x.Update(It.IsAny<UserInfo>()), Times.Once);
            _mockRepo.Verify(x => x.Read(It.IsAny<long>()), Times.Exactly(2));
        }

        [DataRow("Addresse", "12345678", "email@easv365.dk", null, "LastName", 1)]
        [DataRow("Addresse", "12345678", "email@easv365.dk", "", "LastName", 1)]
        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", null, 1)]
        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "", 1)]
        [TestMethod]
        public void TestUserServiceUpdateInvalidDataExceptionName(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo oldUser = new UserInfo()
            {
                Id = id,
                Email = phoneNumber + "old",
                PhoneNumber = email + "old",
                Address = address + "old",
                FirstName = firstName + "old",
                LastName = lastName + "old"
            };
            UserInfo updatedUser = new UserInfo()
            {

                Id = id,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            _mockRepo.Setup(repo => repo.Update(updatedUser)).Returns(updatedUser);

            Assert.ThrowsException<InvalidDataException>(() => _service.Update(updatedUser));
            _mockRepo.Verify(x => x.Update(It.IsAny<UserInfo>()), Times.Never);
        }

        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "LastName", 0)]
        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "LastName", -1)]
        [TestMethod]
        public void TestUserServiceUpdateInvalidOperationExceptionAssignedInvalidID(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo oldUser = new UserInfo()
            {
                Id = id,
                Email = phoneNumber + "old",
                PhoneNumber = email + "old",
                Address = address + "old",
                FirstName = firstName + "old",
                LastName = lastName + "old"
            };
            UserInfo updatedUser = new UserInfo()
            {

                Id = id,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            _mockRepo.Setup(repo => repo.Update(updatedUser)).Returns(updatedUser);

            Assert.ThrowsException<InvalidDataException>(() => _service.Update(updatedUser));
            _mockRepo.Verify(x => x.Update(It.IsAny<UserInfo>()), Times.Never);
        }

        [DataRow("Addresse", "12345678", "emaileasv365.dk", "FirstName", "LastName", 1)]
        [DataRow("Addresse", "12345678", "email@easv36", "FirstName", "LastName", 1)]
        [DataRow("Addresse", "12345678", "", "FirstName", "LastName", 1)]
        [DataRow("Addresse", "12345678", null, "FirstName", "LastName", 1)]
        [TestMethod]
        public void TestUserServiceUpdateInvalidDataExceptionEmail(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo oldUser = new UserInfo()
            {
                Id = id,
                Email = email + "old",
                PhoneNumber = phoneNumber + "old",
                Address = address + "old",
                FirstName = firstName + "old",
                LastName = lastName + "old"
            };
            UserInfo updatedUser = new UserInfo()
            {

                Id = id,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            _mockRepo.Setup(repo => repo.Update(updatedUser)).Returns(updatedUser);

            Assert.ThrowsException<InvalidDataException>(() => _service.Update(updatedUser));
            _mockRepo.Verify(x => x.Update(It.IsAny<UserInfo>()), Times.Never);
        }

        [DataRow("Addresse", null, "email@easv365.dk", "FirstName", "LastName", 1)]
        [DataRow("Addresse", "123456", "email@easv365.dk", "FirstName", "LastName", 1)]
        [DataRow("Addresse", "ABCDEFGH", "email@easv365.dk", "FirstName", "LastName", 1)]
        [DataRow("Addresse", "123abc56", "email@easv365.dk", "FirstName", "LastName", 1)]
        [DataRow("Addresse", "123456789", "email@easv365.dk", "FirstName", "LastName", 1)]
        [TestMethod]
        public void TestUserServiceUpdateInvalidDataExceptionPhoneNumber(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo oldUser = new UserInfo()
            {
                Id = id,
                Email = email + "old",
                PhoneNumber = phoneNumber + "old",
                Address = address + "old",
                FirstName = firstName + "old",
                LastName = lastName + "old"
            };
            UserInfo updatedUser = new UserInfo()
            {

                Id = id,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            _mockRepo.Setup(repo => repo.Update(updatedUser)).Returns(updatedUser);

            Assert.ThrowsException<InvalidDataException>(() => _service.Update(updatedUser));
            _mockRepo.Verify(x => x.Update(It.IsAny<UserInfo>()), Times.Never);
        }

        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "LastName", 1)]
        [TestMethod]
        public void TestUserServiceUpdateInvalidDataExceptionID(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo updatedUser = new UserInfo()
            {
                Id = id,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                FirstName = firstName + "Updated",
                LastName = lastName
            };

            UserInfo userNull = null;

            //Testest if UserService.Update(User) throws exception if the user to be updated does not exist
            _mockRepo.Setup(x => x.Update(updatedUser)).Returns(updatedUser);
            _mockRepo.Setup(x => x.Read(id)).Returns(userNull);

            Assert.ThrowsException<InvalidDataException>(() => _service.Update(updatedUser));
            _mockRepo.Verify(x => x.Update(It.IsAny<UserInfo>()), Times.Never);
        }

        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "LastName", 1)]
        [TestMethod]
        public void TestUserServiceUpdateInvalidOperationExceptionReturnsNull(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo oldUser = new UserInfo()
            {
                Id = id,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };
            UserInfo updatedUser = new UserInfo()
            {
                Id = id,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                FirstName = firstName + "Updated",
                LastName = lastName
            };
            UserInfo userNull = null;

            //Testest if UserService.Update(User) throws exception if not works as entended
            _mockRepo.Setup(x => x.Update(updatedUser)).Returns(userNull);
            _mockRepo.Setup(x => x.Read(id)).Returns(new Queue<UserInfo>(new[] { oldUser, updatedUser }).Dequeue);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Update(updatedUser));
            _mockRepo.Verify(x => x.Update(It.IsAny<UserInfo>()), Times.Once);
        }

        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "LastName", 1)]
        [TestMethod]
        public void TestUserServiceUpdateInvalidOperationExceptionDoesNotUpdate(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo oldUser = new UserInfo()
            {
                Id = id,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };
            UserInfo updatedUser = new UserInfo()
            {
                Id = id,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                FirstName = firstName + "Updated",
                LastName = lastName
            };

            //Testest if UserService.Update(User) throws exception if not works as entended
            _mockRepo.Setup(x => x.Update(updatedUser)).Returns(updatedUser);
            _mockRepo.Setup(x => x.Read(id)).Returns(new Queue<UserInfo>(new[] { oldUser, oldUser }).Dequeue);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Update(updatedUser));
            _mockRepo.Verify(x => x.Update(It.IsAny<UserInfo>()), Times.Once);
        }
        #endregion

        #region test UserService.Delete(User)
        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "LastName", 1)]
        [TestMethod]
        public void TestUserServiceDelete(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo user = new UserInfo
            {
                Id = id,
                Email = phoneNumber,
                PhoneNumber = email,
                Address = address,
                FirstName = firstName,
                LastName = lastName
            };


            // testest if UserService.Delete(id) works as entended
            _mockRepo.Setup(x => x.Delete(user)).Returns(user);
            _mockRepo.Setup(x => x.Read(user.Id)).Returns(new Queue<UserInfo>(new[] { user, null }).Dequeue);

            Assert.IsTrue(_service.Delete(user.Id).Equals(user));

            _mockRepo.Verify(x => x.Delete(user), Times.Once);
            _mockRepo.Verify(x => x.Read(user.Id), Times.Exactly(2));
        }

        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "LastName", 1)]
        [TestMethod]
        public void TestUserServiceDeleteInvalidOperationExceptionReturnsNull(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo user = new UserInfo
            {
                Id = id,
                Email = phoneNumber,
                PhoneNumber = email,
                Address = address,
                FirstName = firstName,
                LastName = lastName
            };

            UserInfo userNull = null;

            // testest if UserService.Delete(id) thorws exception if return null
            _mockRepo.Setup(x => x.Delete(user)).Returns(userNull);
            _mockRepo.Setup(x => x.Read(user.Id)).Returns(new Queue<UserInfo>(new[] { user, null }).Dequeue);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Delete(user.Id));
            _mockRepo.Verify(x => x.Delete(It.IsAny<UserInfo>()), Times.Once);
        }

        [DataRow("Addresse", "12345678", "email@easv365.dk", "FirstName", "LastName", 1)]
        [TestMethod]
        public void TestUserServiceDeleteInvalidOperationExceptionDoesNotDelete(string address, string phoneNumber, string email, string firstName, string lastName, long id)
        {
            UserInfo user = new UserInfo
            {
                Id = id,
                Email = phoneNumber,
                PhoneNumber = email,
                Address = address,
                FirstName = firstName,
                LastName = lastName
            };

            // testest if UserService.Delete(id) throws exception when not working as entended
            _mockRepo.Setup(x => x.Delete(user)).Returns(user);
            _mockRepo.Setup(x => x.Read(user.Id)).Returns(new Queue<UserInfo>(new[] { user, user }).Dequeue);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Delete(user.Id));
            _mockRepo.Verify(x => x.Delete(It.IsAny<UserInfo>()), Times.Once);
        }

        #endregion
    }
}