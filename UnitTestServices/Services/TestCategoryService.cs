using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.DomainServices;
using VrBooking.Core.Entity;

namespace TestServices.Services
{
    [TestClass]
    public class TestCategoryService
    {
        private Mock<IRepository<Category>> _mockRepo;
        private ICategoryService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IRepository<Category>>();
            _service = new CategoryService(_mockRepo.Object);
        }

        #region Test Create

        [DataRow(1, "vr")]
        [DataRow(1, "v r")]
        [DataRow(1, "vr 2")]
        [DataRow(1, "v.r")]
        [TestMethod]
        public void TestCategoryServiceCreate(int id, string name)
        {
            Category beforeCreated = new Category()
            {
                Name = name
            };
            Category afterCreated = new Category()
            {
                Name = name,
                Id = id
            };
            _mockRepo.Setup(x => x.ReadAll()).Returns(new List<Category>());
            _mockRepo.Setup(x => x.Create(beforeCreated)).Returns(afterCreated);

            Assert.IsTrue(_service.Create(beforeCreated) != null);
            _mockRepo.Verify(x => x.Create(It.IsAny<Category>()), Times.Once);
            _mockRepo.Verify(x => x.ReadAll(), Times.Once);
        }

        [TestMethod]
        public void TestCategoryServiceCreateInvalidDataExceptionNull()
        {
            Category beforeCreated = null;
            _mockRepo.Setup(x => x.ReadAll()).Returns(new List<Category>());
            _mockRepo.Setup(x => x.Create(beforeCreated)).Returns(null as Category);

            Assert.ThrowsException<InvalidDataException>(() => _service.Create(beforeCreated));
            _mockRepo.Verify(x => x.Create(It.IsAny<Category>()), Times.Never);
            _mockRepo.Verify(x => x.ReadAll(), Times.Never);
        }

        [DataRow(1, "")]
        [DataRow(1, "    ")]
        [DataRow(1, " ")]
        [DataRow(1, null)]
        [TestMethod]
        public void TestCategoryServiceCreateInvalidDataExceptionName(int id, string name)
        {
            Category beforeCreated = new Category()
            {
                Name = name
            };
            Category afterCreated = new Category()
            {
                Name = name,
                Id = id
            };
            _mockRepo.Setup(x => x.ReadAll()).Returns(new List<Category>());
            _mockRepo.Setup(x => x.Create(beforeCreated)).Returns(afterCreated);

            Assert.ThrowsException<InvalidDataException>(() => _service.Create(beforeCreated));
            _mockRepo.Verify(x => x.Create(It.IsAny<Category>()), Times.Never);
        }

        [DataRow(1, "vr", 2, "vr")]
        [DataRow(1, "vr", 2, "VR")]
        [DataRow(1, "vr", 2, "vR")]
        [DataRow(1, "vr", 2, "Vr")]
        [DataRow(1, "Vr", 2, "VR")]
        [TestMethod]
        public void TestCategoryServiceCreateInvalidDataExceptionExists(int id, string name, int id2, string name2)
        {
            Category beforeCreated = new Category()
            {
                Name = name
            };
            Category afterCreated = new Category()
            {
                Name = name,
                Id = id
            };
            Category alreadyExisting = new Category()
            {
                Id = id2,
                Name = name2
            };
            _mockRepo.Setup(x => x.ReadAll()).Returns(new List<Category>() { alreadyExisting });
            _mockRepo.Setup(x => x.Create(beforeCreated)).Returns(afterCreated);

            Assert.ThrowsException<InvalidDataException>(() => _service.Create(beforeCreated));
            _mockRepo.Verify(x => x.Create(It.IsAny<Category>()), Times.Never);
        }

        [DataRow(1, "vr")]
        [TestMethod]
        public void TestCategoryServiceCreateInvalidOperationExceptionRepoReturnNull(int id, string name)
        {
            Category beforeCreated = new Category()
            {
                Name = name
            };
            Category afterCreated = new Category()
            {
                Name = name,
                Id = id
            };
            _mockRepo.Setup(x => x.ReadAll()).Returns(new List<Category>());
            _mockRepo.Setup(x => x.Create(beforeCreated)).Returns(null as Category);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Create(beforeCreated));
        }

        [DataRow(-2, "vr")]
        [DataRow(0, "vr")]
        [TestMethod]
        public void TestCategoryServiceCreateInvalidOperationExceptionInvalidID(int id, string name)
        {
            Category beforeCreated = new Category()
            {
                Name = name
            };
            Category afterCreated = new Category()
            {
                Name = name,
                Id = id
            };
            _mockRepo.Setup(x => x.ReadAll()).Returns(new List<Category>());
            _mockRepo.Setup(x => x.Create(beforeCreated)).Returns(null as Category);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Create(beforeCreated));
        }

        #endregion

        #region Test Read

        [DataRow(1, 1, "vr")]
        [DataRow(2, 2, "vr2")]
        [DataRow(3, 3, "vr3")]
        [TestMethod]
        public void TestCategoryServiceRead(int idSearch, int idFound, string name) 
        {
            Category found = new Category()
            {
                Id = idFound,
                Name = name
            };
            _mockRepo.Setup(x => x.Read(idSearch)).Returns(found);
            Assert.IsTrue(idSearch == _service.Read(idSearch).Id);
        }

        [DataRow(1)]
        [DataRow(2)]
        [TestMethod]
        public void TestCategoryServiceReadInvalidDataExceptionNoneFound(int idSearch)
        {
            _mockRepo.Setup(x => x.Read(idSearch)).Returns(null as Category);
            Assert.ThrowsException<InvalidDataException>(() => _service.Read(idSearch));
        }

        [DataRow(1, 2, "vr2")]
        [DataRow(2, 3, "vr3")]
        [DataRow(3, 4, "vr4")]
        [TestMethod]
        public void TestCategoryServiceReadInvalidOperationExceptionInvalidID(int idSearch, int idFound, string name)
        {
            Category found = new Category()
            {
                Id = idFound,
                Name = name
            };
            _mockRepo.Setup(x => x.Read(idSearch)).Returns(found);
            Assert.ThrowsException<InvalidOperationException>(() => _service.Read(idSearch));
        }

        #endregion

        #region Test ReadAll

        [TestMethod]
        public void TestCategoryServiceReadAll() 
        {
            _mockRepo.Setup(x => x.ReadAll()).Returns(new List<Category>());
            Assert.IsTrue(_service.ReadAll() != null);
        }

        #endregion

        #region Test Update

        [DataRow(1,"Name", "NewName")]
        [DataRow(1, "SameName", "SameName")]
        [TestMethod]
        public void TestCategoryServiceUpdate(int id, string name, string newName)
        {
            Category beforeUpdated = new Category()
            {
                Id = id,
                Name = name
            };
            Category afterUpdate = new Category()
            {
                Name = newName,
                Id = id
            };
            _mockRepo.Setup(x => x.Read(id)).Returns(afterUpdate);
            _mockRepo.Setup(x => x.ReadAll()).Returns(new List<Category>());
            _mockRepo.Setup(x => x.Update(afterUpdate)).Returns(afterUpdate);

            Assert.IsTrue(_service.Update(afterUpdate) != null);
            _mockRepo.Verify(x => x.Update(It.IsAny<Category>()), Times.Once);
            _mockRepo.Verify(x => x.ReadAll(), Times.Once);
        }

        [DataRow(1, "Name", "NewName")]
        [TestMethod]
        public void TestCategoryServiceUpdateInvalidDataExceptionNull(int id, string name, string newName)
        {
            Category beforeUpdated = new Category()
            {
                Id = id,
                Name = name
            };
            Category afterUpdate = new Category()
            {
                Name = newName,
                Id = id
            };
            _mockRepo.Setup(x => x.Read(id)).Returns(afterUpdate);
            _mockRepo.Setup(x => x.ReadAll()).Returns(new List<Category>(new[] { beforeUpdated }));
            _mockRepo.Setup(x => x.Update(afterUpdate)).Returns(afterUpdate);

            Assert.ThrowsException<InvalidDataException>(() => _service.Update(null));
            _mockRepo.Verify(x => x.Update(It.IsAny<Category>()), Times.Never);
            _mockRepo.Verify(x => x.ReadAll(), Times.Never);
        }

        [DataRow(1, "Name", "")]
        [DataRow(1, "Name", " ")]
        [DataRow(1, "Name", null)]

        [TestMethod]
        public void TestCategoryServiceUpdateInvalidDataExceptionName(int id, string name, string newName)
        {
            Category beforeUpdated = new Category()
            {
                Id = id,
                Name = name
            };
            Category afterUpdate = new Category()
            {
                Name = newName,
                Id = id
            };
            _mockRepo.Setup(x => x.Read(id)).Returns(afterUpdate);
            _mockRepo.Setup(x => x.ReadAll()).Returns(new List<Category>(new[] { beforeUpdated }));
            _mockRepo.Setup(x => x.Update(afterUpdate)).Returns(afterUpdate);

            Assert.ThrowsException<InvalidDataException>(() => _service.Update(afterUpdate));
            _mockRepo.Verify(x => x.Update(It.IsAny<Category>()), Times.Never);
            _mockRepo.Verify(x => x.ReadAll(), Times.Never);
        }

        [DataRow(1, "name", "name")]
        [DataRow(1, "Name", "name")]
        [DataRow(1, "Name", "nAME")]
        [DataRow(1, "NAME", "name")]
        [TestMethod]
        public void TestCategoryServiceUpdateInvalidDataExceptionExists(int id, string name, string name2)
        {
            Category exists = new Category()
            {
                Id = id + 1,
                Name = name
            };
            Category afterUpdate = new Category()
            {
                Name = name2,
                Id = id
            };
            _mockRepo.Setup(x => x.Read(id)).Returns(afterUpdate);
            _mockRepo.Setup(x => x.ReadAll()).Returns(new List<Category>(new[] { exists }));
            _mockRepo.Setup(x => x.Update(afterUpdate)).Returns(afterUpdate);

            Assert.ThrowsException<InvalidDataException>(() => _service.Update(afterUpdate));
            _mockRepo.Verify(x => x.Update(It.IsAny<Category>()), Times.Never);
            _mockRepo.Verify(x => x.ReadAll(), Times.Once);
        }

        [DataRow(1, "Name", "NewName")]
        [TestMethod]
        public void TestCategoryServiceUpdateInvalidOperationExceptionRepoReturnNull(int id, string name, string newName)
        {
            Category beforeUpdated = new Category()
            {
                Id = id,
                Name = name
            };
            Category afterUpdate = new Category()
            {
                Name = newName,
                Id = id
            };
            _mockRepo.Setup(x => x.Read(id)).Returns(afterUpdate);
            _mockRepo.Setup(x => x.ReadAll()).Returns(new List<Category>(new[] { beforeUpdated }));
            _mockRepo.Setup(x => x.Update(afterUpdate)).Returns(null as Category);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Update(afterUpdate));
            _mockRepo.Verify(x => x.Update(It.IsAny<Category>()), Times.Once);
            _mockRepo.Verify(x => x.ReadAll(), Times.Once);
        }

        [DataRow(1, "Name", "NewName")]
        [TestMethod]
        public void TestCategoryServiceUpdateInvalidOperationExceptionNotUpdated(int id, string name, string newName)
        {
            Category beforeUpdated = new Category()
            {
                Id = id,
                Name = name
            };
            Category afterUpdate = new Category()
            {
                Name = newName,
                Id = id
            };
            _mockRepo.Setup(x => x.Read(id)).Returns(beforeUpdated);
            _mockRepo.Setup(x => x.ReadAll()).Returns(new List<Category>(new[] { beforeUpdated }));
            _mockRepo.Setup(x => x.Update(afterUpdate)).Returns(afterUpdate);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Update(afterUpdate));
            _mockRepo.Verify(x => x.Update(It.IsAny<Category>()), Times.Once);
            _mockRepo.Verify(x => x.ReadAll(), Times.Once);
        }

        #endregion

        #region Test Delete

        [DataRow(1, "Name")]
        [TestMethod]
        public void TestCategoryServiceDelete(long id, string name) {
            Category category = new Category()
            {
                Id = id,
                Name = name
            };

            _mockRepo.Setup(x => x.Delete(category)).Returns(category);
            _mockRepo.Setup(x => x.Read(id)).Returns(new Queue<Category>(new[] { category, null }).Dequeue);

            Assert.IsTrue(_service.Delete(id).Equals(category));

            _mockRepo.Verify(x => x.Delete(category), Times.Once);
            _mockRepo.Verify(x => x.Read(id), Times.Exactly(2));
        }

        [DataRow(1, "Name")]
        [TestMethod]
        public void TestCategoryServiceDeleteInvalidOperationExceptionNotDeleted(long id, string name)
        {
            Category category = new Category()
            {
                Id = id,
                Name = name
            };

            _mockRepo.Setup(x => x.Delete(category)).Returns(category);
            _mockRepo.Setup(x => x.Read(id)).Returns(new Queue<Category>(new[] { category, category }).Dequeue);

            Assert.ThrowsException<InvalidOperationException>(() => _service.Delete(id));

            _mockRepo.Verify(x => x.Delete(category), Times.Once);
            _mockRepo.Verify(x => x.Read(id), Times.Exactly(2));
        }

        #endregion

    }
}
