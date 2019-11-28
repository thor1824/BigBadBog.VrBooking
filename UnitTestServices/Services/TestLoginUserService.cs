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

            mockRepo.Setup(repo => repo.Create(userBeforeCreate)).Returns(userAfterCreate);
            Assert.IsTrue(service.Create(userBeforeCreate) != null);
            mockRepo.Verify(x => x.Create(It.IsAny<LoginUser>()), Times.Once);
        }

        #region TestCreate





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

            mockRepo.Setup(repo => repo.Create(user)).Returns(user2);

            Assert.ThrowsException<InvalidDataException>(() => service.Create(user));
            mockRepo.Verify(x => x.Create(It.IsAny<LoginUser>()), Times.Never);

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

            mockRepo.Setup(repo => repo.Create(user)).Returns(user2);

            Assert.ThrowsException<InvalidOperationException>(() => service.Create(user));
            mockRepo.Verify(x => x.Create(It.IsAny<LoginUser>()), Times.Once);

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

            mockRepo.Setup(repo => repo.Create(user)).Returns(user2);

            Assert.ThrowsException<InvalidOperationException>(() => service.Create(user));
            mockRepo.Verify(x => x.Create(It.IsAny<LoginUser>()), Times.Once);

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

            mockRepo.Setup(repo => repo.Create(user)).Returns(user2);

            Assert.ThrowsException<InvalidOperationException>(() => service.Create(user));
            mockRepo.Verify(x => x.Create(It.IsAny<LoginUser>()), Times.Once);

        }

        #endregion

        #region TestRead

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

            mockRepo.Setup(repo => repo.Read(id)).Returns(user);

            Assert.IsTrue(service.Read(id).Id == id);

            mockRepo.Verify(x => x.Read(id), Times.Once);

            mockRepo.Setup(repo => repo.Read(id)).Returns(wrongUser);

            Assert.ThrowsException<InvalidOperationException>(() => service.Read(id));

            mockRepo.Setup(repo => repo.Read(id)).Returns(userNull);

            Assert.ThrowsException<InvalidDataException>(() => service.Read(id));
        }

        #endregion

        #region TestReadAll

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

            mockRepo.Setup(repo => repo.ReadAll()).Returns(users);

            service.ReadAll();
            mockRepo.Verify(x => x.ReadAll(), Times.Once);



        }




        #endregion

        #region TesetUpdate

        
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

            LoginUser nullUser = null;

            mockRepo.Setup(x => x.Update(user)).Returns(nullUser);
            mockRepo.Setup(x => x.Update(user)).Returns(userUpdatedIfo);
            mockRepo.Setup(x => x.Read(user.Id)).Returns(new Queue<LoginUser>(new[] { user, userUpdatedIfo }).Dequeue);

            Assert.IsTrue(service.Update(user).Equals(userUpdatedIfo));

            mockRepo.Verify(x => x.Update(user), Times.Once);
            mockRepo.Verify(x => x.Read(user.Id), Times.Exactly(2));
        }

        [DataRow("perPerson335@easv365.dk", new byte[] {}, new byte[] { 1, 2, 3 }, false, false)]
        [DataRow("perPerson335@easv365.dk", null, new byte[] { 1, 2, 3 }, false, false)]
        [DataRow("ole", new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3 }, false, false)]
        [DataRow("ole€&@easv365.dk", new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3 }, false, false)]
        [DataRow("", new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3 }, false, false)]
        [DataRow("ole@e365.dk", new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3 }, false, false)]
        [DataRow(null, new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3 }, false, false)]
        [TestMethod]
        public void TestInvalidDateOnUpdate(string userName, byte[] passwordHash, byte[] passWordSalt, bool admin, bool activated)
        {
            LoginUser updateUser = new LoginUser()
            {
                Activated = activated,
                Admin = admin,
                PasswordHash = passwordHash,
                PasswordSalt = passWordSalt,
                UserName = userName
            };

            mockRepo.Setup(repo => repo.Update(updateUser)).Returns(updateUser);
            Assert.ThrowsException<InvalidDataException>(() => service.Update(updateUser));
            mockRepo.Verify(x => x.Update(It.IsAny<LoginUser>()), Times.Never);
        }

        [TestMethod]
        public void TestUpdateInvalidOperation()
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

            mockRepo.Setup(x => x.Update(user)).Returns(user);
            mockRepo.Setup(x => x.Read(user.Id)).Returns(new Queue<LoginUser>(new[] { user, user }).Dequeue);

            Assert.ThrowsException<InvalidOperationException>(() => service.Update(user));
            mockRepo.Verify(x => x.Update(It.IsAny<LoginUser>()), Times.Once);
        }

        #endregion



        #region TestDelete

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
            mockRepo.Setup(x => x.Delete(user)).Returns(nullUser);
            mockRepo.Setup(x => x.Read(user.Id)).Returns(new Queue<LoginUser>(new[] { user, null }).Dequeue);

            Assert.ThrowsException<InvalidOperationException>(() => service.Delete(user.Id));
            mockRepo.Verify(x => x.Delete(It.IsAny<LoginUser>()), Times.Once);
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

            mockRepo.Setup(x => x.Delete(user)).Returns(user);
            mockRepo.Setup(x => x.Read(user.Id)).Returns(new Queue<LoginUser>(new[] { user, user }).Dequeue);

            Assert.ThrowsException<InvalidOperationException>(() => service.Delete(user.Id));
            mockRepo.Verify(x => x.Delete(It.IsAny<LoginUser>()), Times.Once);
        }
        #endregion


    }

}

