using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage.Internal;
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
        private Mock<IRepository<LoginUser>> _mockRepo;
        private ILoginUserService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IRepository<LoginUser>>();
            _service = new LoginUserService(_mockRepo.Object);
        }

        [TestMethod]
        public void TestLoginUserCreate()
        {
            LoginUser userBeforeCreate = new LoginUser()
            {
                Activated = true,
                Admin = false,
                UserName = "perPerson335@easv365.dk",
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

            _mockRepo.Setup(repo => repo.Create(userBeforeCreate)).Returns(userAfterCreate);
            Assert.IsTrue(_service.Create(userBeforeCreate) != null);
            _mockRepo.Verify(x => x.Create(It.IsAny<LoginUser>()), Times.Once);
        }

        #region TestLoginUserService.Create(LoginUser)





        [DataRow("ole", new byte[] {1, 2, 3}, new byte[] {1, 2, 3}, false, false)]
        [DataRow("ole€&@easv365.dk", new byte[] {1, 2, 3}, new byte[] {1, 2, 3}, false, false)]
        [DataRow("", new byte[] {1, 2, 3}, new byte[] {1, 2, 3}, false, false)]
        [DataRow("ole@e365.dk", new byte[] {1, 2, 3}, new byte[] {1, 2, 3}, false, false)]
        [DataRow(null, new byte[] {1, 2, 3}, new byte[] {1, 2, 3}, false, false)]
        [TestMethod]
        public void TestForUsername(string userName, byte[] passwordHash, byte[] passWordSalt, bool admin,
            bool activated)
        {
            LoginUser user = new LoginUser()
            {
                UserName = userName,
                Activated = activated,
                Admin = admin,
                PasswordHash = passwordHash,
                PasswordSalt = passwordHash
            };

            LoginUser user2 = new LoginUser()
            {
                UserName = userName,
                Activated = activated,
                Admin = admin,
                PasswordHash = passwordHash,
                PasswordSalt = passwordHash,
                Id = 1
            };

            _mockRepo.Setup(repo => repo.Create(user)).Returns(user2);

            Assert.ThrowsException<InvalidDataException>(() => _service.Create(user));
            _mockRepo.Verify(x => x.Create(It.IsAny<LoginUser>()), Times.Never);

        }

        [DataRow("perPerson335@easv365.dk", new byte[] { }, new byte[] {1, 2, 3}, false, false)]
        [DataRow("perPerson335@easv365.dk", null, new byte[] {1, 2, 3}, false, false)]
        [TestMethod]
        public void TesLoginUserHasPassWord(string userName, byte[] passwordHash, byte[] passWordSalt, bool admin,
            bool activated)
        {
            LoginUser user = new LoginUser()
            {
                UserName = userName,
                Activated = activated,
                Admin = admin,

            };

            LoginUser user2 = new LoginUser()
            {
                UserName = userName,
                Activated = activated,
                Admin = admin,
                PasswordHash = passwordHash,
                PasswordSalt = passWordSalt,
                Id = 1
            };

            _mockRepo.Setup(repo => repo.Create(user)).Returns(user2);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Create(user));
            _mockRepo.Verify(x => x.Create(It.IsAny<LoginUser>()), Times.Once);

        }

        [DataRow("perPerson335@easv365.dk", new byte[] {1, 2, 3}, new byte[] { }, false, false)]
        [DataRow("perPerson335@easv365.dk", new byte[] {1, 2, 3}, null, false, false)]
        [TestMethod]
        public void TesLoginUserSalt(string userName, byte[] passwordHash, byte[] passWordSalt, bool admin,
            bool activated)
        {
            LoginUser user = new LoginUser()
            {
                UserName = userName,
                Activated = activated,
                Admin = admin,

            };

            LoginUser user2 = new LoginUser()
            {
                UserName = userName,
                Activated = activated,
                Admin = admin,
                PasswordHash = passwordHash,
                PasswordSalt = passWordSalt,
                Id = 1
            };

            _mockRepo.Setup(repo => repo.Create(user)).Returns(user2);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Create(user));
            _mockRepo.Verify(x => x.Create(It.IsAny<LoginUser>()), Times.Once);

        }


        [DataRow("perPerson335@easv365.dk", new byte[] {1, 2, 3}, new byte[] {1, 2, 3}, false, false, null)]
        [DataRow("perPerson335@easv365.dk", new byte[] {1, 2, 3}, new byte[] {1, 2, 3}, false, false, -1)]
        [TestMethod]
        public void TestLoginUserIsVdIalid(string userName, byte[] passwordHash, byte[] passWordSalt, bool admin, bool activated, long id)
        {
            LoginUser user = new LoginUser()
            {
                UserName = userName,
                Activated = activated,
                Admin = admin,

            };

            LoginUser user2 = new LoginUser()
            {
                UserName = userName,
                Activated = activated,
                Admin = admin,
                PasswordHash = passwordHash,
                PasswordSalt = passWordSalt,
                Id = id
            };

            _mockRepo.Setup(repo => repo.Create(user)).Returns(user2);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Create(user));
            _mockRepo.Verify(x => x.Create(It.IsAny<LoginUser>()), Times.Once);

        }


        [TestMethod]
        public void TesetLogimUserServiceCreateNameIsInUseInvalidtData()
        {
            List<LoginUser> readAll = new List<LoginUser>()
            {

                new LoginUser()
                {
                    Activated = true,
                    Admin = false,
                    UserName = "perPerson335@easv365.dk",
                    PasswordHash = new byte[] {1, 2, 3},
                    PasswordSalt = new byte[] {1, 2, 3}
                } 

            };

            LoginUser userToCreate= new LoginUser()
            {
                Activated = true,
                Admin = false,
                UserName = "perPerson335@easv365.dk",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 }
            };

            _mockRepo.Setup(repo => repo.ReadAll()).Returns(readAll);
            _mockRepo.Setup(repo => repo.Create(userToCreate)).Returns(userToCreate);
            Assert.ThrowsException<InvalidDataException>(() => _service.Create(userToCreate));
            _mockRepo.Verify(x => x.Create(It.IsAny<LoginUser>()), Times.Never);
        }

        #endregion

        #region TestLoginUserService.Read(Id)

        public void TestLoginUserRead()
        {
            long id = 1;
            LoginUser user = new LoginUser()
            {
                Activated = true,
                Admin = false,
                UserName = "perPerson335@easv365.dk",
                PasswordHash = new byte[] {1, 2, 3},
                PasswordSalt = new byte[] {1, 2, 3},
                Id = 1
            };

            LoginUser wrongUser = new LoginUser()
            {
                Activated = true,
                Admin = false,
                UserName = "perPerson335@easv365.dk",
                PasswordHash = new byte[] {1, 2, 3},
                PasswordSalt = new byte[] {1, 2, 3},
                Id = 2
            };
            LoginUser userNull = null;

            _mockRepo.Setup(repo => repo.Read(id)).Returns(user);

            Assert.IsTrue(_service.Read(id).Id == id);

            _mockRepo.Verify(x => x.Read(id), Times.Once);

            _mockRepo.Setup(repo => repo.Read(id)).Returns(wrongUser);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Read(id));

            _mockRepo.Setup(repo => repo.Read(id)).Returns(userNull);

            Assert.ThrowsException<InvalidDataException>(() => _service.Read(id));
        }

        #endregion

        #region TestLoginUserService.ReadAll

        [TestMethod]
        public void TestLoginUserServiceGetAll()
        {
            IEnumerable<LoginUser> users = new List<LoginUser>()
            {
                new LoginUser()
                {
                    Activated = true,
                    Admin = false,
                    UserName = "perPerson335@easv365.dk",
                    PasswordHash = new byte[] {1, 2, 3},
                    PasswordSalt = new byte[] {1, 2, 3},
                    Id = 1

                },
                new LoginUser()
                {
                    Activated = true,
                    Admin = false,
                    UserName = "Person335@easv365.dk",
                    PasswordHash = new byte[] {1, 2, 3},
                    PasswordSalt = new byte[] {1, 2, 3},
                    Id = 2
                }

            };

            _mockRepo.Setup(repo => repo.ReadAll()).Returns(users);

            _service.ReadAll();
            _mockRepo.Verify(x => x.ReadAll(), Times.Once);



        }




        #endregion

        #region TesetLoginUserService.Update(LoginUser)


        [TestMethod]
        public void TestLoginUserUpdate()
        {
            LoginUser user =  new LoginUser()
            {
                Activated = true,
                Admin = false,
                UserName = "perPerson335@easv365.dk",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                Id = 1
            };

            LoginUser userUpdatedIfo = new LoginUser()
            {
                Activated = true,
                Admin = false,
                UserName = "IHaveBenUpdated@easv365.dk",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                Id = 1
            };

            
            _mockRepo.Setup(x => x.Update(user)).Returns(userUpdatedIfo);
            _mockRepo.Setup(x => x.Read(user.Id)).Returns(new Queue<LoginUser>(new[] { user, userUpdatedIfo }).Dequeue);

            Assert.IsTrue(_service.Update(user).Equals(userUpdatedIfo));

            _mockRepo.Verify(x => x.Update(user), Times.Once);
            _mockRepo.Verify(x => x.Read(user.Id), Times.Exactly(2));
        }

        [DataRow("perPerson335@easv365.dk", new byte[] {}, new byte[] { 1, 2, 3 }, false, false)]
        [DataRow("perPerson335@easv365.dk", null, new byte[] { 1, 2, 3 }, false, false)]
        [DataRow("ole", new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3 }, false, false)]
        [DataRow("ole€&@easv365.dk", new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3 }, false, false)]
        [DataRow("", new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3 }, false, false)]
        [DataRow("ole@e365.dk", new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3 }, false, false)]
        [DataRow(null, new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3 }, false, false)]
        [TestMethod]
        public void TestInvalidDataOnUpdate(string userName, byte[] passwordHash, byte[] passWordSalt, bool admin, bool activated)
        {
            LoginUser updateUser = new LoginUser()
            {
                Activated = activated,
                Admin = admin,
                PasswordHash = passwordHash,
                PasswordSalt = passWordSalt,
                UserName = userName
            };

            _mockRepo.Setup(repo => repo.Update(updateUser)).Returns(updateUser);
            Assert.ThrowsException<InvalidDataException>(() => _service.Update(updateUser));
            _mockRepo.Verify(x => x.Update(It.IsAny<LoginUser>()), Times.Never);
        }

        [TestMethod]
        public void TestUpdateInvalidOperationUserNotUpdated()
        {
            LoginUser user = new LoginUser()
            {
                Activated = true,
                Admin = false,
                UserName = "perPerson335@easv365.dk",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                Id = 1
            };

            LoginUser userUpdatedIfo = new LoginUser()
            {
                Activated = true,
                Admin = false,
                UserName = "IHaveBenUpdated@easv365.dk",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                Id = 1
            };

            _mockRepo.Setup(x => x.Update(user)).Returns(user);
            _mockRepo.Setup(x => x.Read(user.Id)).Returns(new Queue<LoginUser>(new[] { user, user }).Dequeue);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Update(user));
            _mockRepo.Verify(x => x.Update(It.IsAny<LoginUser>()), Times.Once);
        }

        [TestMethod]
        public void TestUpdateInvalidOperationUserIsNull()
        {
            LoginUser user = new LoginUser()
            {
                Activated = true,
                Admin = false,
                UserName = "perPerson335@easv365.dk",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                Id = 1
            };

            LoginUser nullUser = null;

            _mockRepo.Setup(x => x.Update(user)).Returns(user);
            _mockRepo.Setup(x => x.Read(user.Id)).Returns(nullUser);

            Assert.ThrowsException<InvalidDataException>(() => _service.Update(user));
            _mockRepo.Verify(x => x.Update(It.IsAny<LoginUser>()), Times.Never);
        }
    

        #endregion

        #region TestLoginUserService.Delete(LoginUser)

        [TestMethod]
        public void TestDeleteInvalidOperationLoginUserIsNull()
        {
            LoginUser user = new LoginUser()
            {
                Activated = true,
                Admin = false,
                UserName = "perPerson335@easv365.dk",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                Id = 1
            };

            LoginUser nullUser = null;
            _mockRepo.Setup(x => x.Delete(user)).Returns(nullUser);
            _mockRepo.Setup(x => x.Read(user.Id)).Returns(new Queue<LoginUser>(new[] { user, null }).Dequeue);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Delete(user.Id));
            _mockRepo.Verify(x => x.Delete(It.IsAny<LoginUser>()), Times.Once);
        }


        [TestMethod]
        public void TestDeleteInvalidOperationLoginUserNotDeleted()
        {
            LoginUser user = new LoginUser()
            {
                Activated = true,
                Admin = false,
                UserName = "perPerson335@easv365.dk",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                Id = 1
            };

            _mockRepo.Setup(x => x.Delete(user)).Returns(user);
            _mockRepo.Setup(x => x.Read(user.Id)).Returns(new Queue<LoginUser>(new[] { user, user }).Dequeue);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Delete(user.Id));
            _mockRepo.Verify(x => x.Delete(It.IsAny<LoginUser>()), Times.Once);
        }
        #endregion


    }

}

