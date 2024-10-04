using Inventory.API.Controllers;
using Inventory.API.Data;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoFixture.AutoMoq;
using AutoFixture;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Inventory.UnitTest
{
    [TestClass]
    public class InventoryControllerUnitTest
    {
        private readonly Mock<ILogger<InventoryController>> _logger;
        private readonly Mock<InventoryAPIContext> _context;
        private IFixture _fixture;

        public InventoryControllerUnitTest(IFixture fixture)
        {
            _logger = new Mock<ILogger<InventoryController>>();
            _context = new Mock<InventoryAPIContext>();
            _fixture = fixture;
        }

        [TestMethod]
        public async Task InventoryController_Get_When_Inventory_Is_Null()
        {
            //Arrange
            var inventoryController = new InventoryController(_logger.Object, _context.Object);
            var inventrory = _fixture.Create<API.Models.Inventory>();
            inventrory = null;
            
            //Act
            var data = await inventoryController.Get();

            //Asset
            Assert.IsNull(data);
            Assert.IsNull(inventrory);

        }

        [TestMethod]
        public async Task InventoryController_Get_When_Inventory_Is_Not_Null()
        {
            //Arrange
            var inventoryController = new InventoryController(_logger.Object, _context.Object);
            var lstInventrory = _fixture.Create<List<API.Models.Inventory>>();

            //Act
            var data = await inventoryController.Get();

            //Asset
            Assert.IsNotNull(data);
            Assert.IsNotNull(lstInventrory);

        }

        [TestMethod]
        public void InventoryController_Get_When_Throw_Exception()
        {
            //Arrange
            var inventoryController = new InventoryController(_logger.Object, _context.Object);
            var getException = _fixture.Create<Exception>();

            //Act
            var result = Assert.ThrowsException<Exception>(() => inventoryController.Get());
            Assert.AreEqual(getException, result);

        }


    }
}