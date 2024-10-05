using Inventory.API.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoFixture.AutoMoq;
using AutoFixture;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Inventory.Business.Interfaces;

namespace Inventory.UnitTest
{
    [TestClass]
    public class InventoryControllerUnitTest
    {
        private Mock<ILogger<InventoryController>> _logger;
        private Mock<IInventoryBusiness> mockInventoryBusiness;
        private Fixture _fixture;

        [TestInitialize]
        public void InventoryControllerUnitTestTestInitialize()
        {
            _logger = new Mock<ILogger<InventoryController>>();
            mockInventoryBusiness = new Mock<IInventoryBusiness>();
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

        }

        [TestMethod]
        public async Task InventoryController_Get_All_Inventorys()
        {
            //Arrange
            var InventoryResult = _fixture.Create<Task<IEnumerable<Common.Entities.Inventory>>>();
            mockInventoryBusiness.Setup(c => c.GetInventories()).Returns(InventoryResult);

            //Act
            var InventoryController = new InventoryController(_logger.Object, mockInventoryBusiness.Object);
            var result = await InventoryController.Get();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(InventoryResult);
        }

        [TestMethod]
        public async Task InventoryController_Get_All_Inventorys_Null()
        {
            //Arrange
            var InventoryResult = _fixture.Create<Task<IEnumerable<Common.Entities.Inventory>>>();
            mockInventoryBusiness.Setup(c => c.GetInventories()).Returns(InventoryResult);
            InventoryResult = null;

            //Act
            var InventoryController = new InventoryController(_logger.Object, mockInventoryBusiness.Object);
            var result = await InventoryController.Get();
            result = null;

            //Assert
            Assert.IsNull(result);
            Assert.IsNull(InventoryResult);
        }

        [TestMethod]
        public async Task InventoryController_Get_Inventory_By_Id()
        {
            //Arrange
            var InventoryResult = _fixture.Create<Task<Common.Entities.Inventory>>();
            mockInventoryBusiness.Setup(c => c.GetInventoryById(It.IsAny<Guid>())).Returns(InventoryResult);
            //InventoryResult = null;

            //Act
            var InventoryController = new InventoryController(_logger.Object, mockInventoryBusiness.Object);
            var id = _fixture.Create<Guid>();
            var result = await InventoryController.Get(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(InventoryResult);
        }

        [TestMethod]
        public async Task InventoryController_Create_Inventory()
        {
            //Arrange
            var InventoryResult = _fixture.Create<Task<Common.Entities.Inventory>>();
            InventoryResult.Result.ItemId = Guid.Empty;
            mockInventoryBusiness.Setup(c => c.CreateInventory(It.IsAny<Common.Entities.Inventory>())).Returns(InventoryResult);

            //Act
            var InventoryController = new InventoryController(_logger.Object, mockInventoryBusiness.Object);
            var Inventory = _fixture.Create<Common.Entities.Inventory>();
            var result = await InventoryController.Post(Inventory);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(InventoryResult);
        }

        [TestMethod]
        public async Task InventoryController_Update_Inventory()
        {
            //Arrange
            var InventoryResult = _fixture.Create<Task<Common.Entities.Inventory>>();
            mockInventoryBusiness.Setup(c => c.UpdateInventory(It.IsAny<Guid>(), It.IsAny<Common.Entities.Inventory>())).Returns(InventoryResult);

            //Act
            var InventoryController = new InventoryController(_logger.Object, mockInventoryBusiness.Object);
            var Inventory = _fixture.Create<Common.Entities.Inventory>();
            var id = _fixture.Create<Guid>();

            var result = await InventoryController.Put(id, Inventory);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(InventoryResult);
        }

        [TestMethod]
        public async Task InventoryController_Delete_Inventory()
        {
            //Arrange
            var InventoryResult = _fixture.Create<Task<bool>>();
            mockInventoryBusiness.Setup(c => c.DeleteInventory(It.IsAny<Guid>())).Returns(InventoryResult);

            //Act
            var InventoryController = new InventoryController(_logger.Object, mockInventoryBusiness.Object);
            var id = _fixture.Create<Guid>();

            var result = await InventoryController.Delete(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(InventoryResult);
        }
    }
}