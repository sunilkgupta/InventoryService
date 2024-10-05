using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory.DataContext;
using Inventory.Business.Interfaces;

namespace Inventory.API.Controllers
{
    /// <summary>
    /// Provides all Inventory related data 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IInventoryBusiness _inventoryBusiness;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="inventoryBusiness"></param>
        public InventoryController(ILogger<InventoryController> logger, IInventoryBusiness inventoryBusiness)
        {
            _logger = logger;
            _inventoryBusiness = inventoryBusiness;
        }

        /// <summary>
        /// Get all inventories
        /// </summary>
        /// <returns></returns>
        // GET: api/Inventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Common.Entities.Inventory>>> Get()
        {
            _logger.LogInformation("All Inventorys requested");
            var response = await _inventoryBusiness.GetInventories();
            if (!response.Any())
            {
                return NotFound();
            }
            return Ok(response);
        }

        /// <summary>
        /// Get specific Inventory record
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        // GET: api/Inventory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Common.Entities.Inventory>> Get(Guid id)
        {
            _logger.LogInformation("Inventory requested: Id is {0}", id);
            if (id == Guid.Empty)
            {
                _logger.LogWarning("Inventory requested: Id is null {0} ", id);
                return BadRequest();
            }
            else
            {
                var response = await _inventoryBusiness.GetInventoryById(id);
                if (response == null)
                {
                    _logger.LogWarning("Inventory requested: Id is {0}, does not exist.", id);
                    return NotFound();
                }
                return Ok(response);
            }
        }

        /// <summary>
        /// Update exising Inventory
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="Inventory">Inventory</param>
        /// <returns></returns>
        // PUT: api/Inventory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Common.Entities.Inventory Inventory)
        {
            _logger.LogInformation("Inventory updating: Id is {0}", id);

            if (id == Guid.Empty || Inventory == null)
            {
                return BadRequest();
            }
            await _inventoryBusiness.UpdateInventory(id, Inventory);
            return Ok(Inventory);

        }

        /// <summary>
        /// Create a new Inventory
        /// </summary>
        /// <param name="Inventory"></param>
        /// <returns></returns>
        // POST: api/Inventory
        [HttpPost]
        public async Task<ActionResult<Common.Entities.Inventory>> Post(Common.Entities.Inventory Inventory)
        {
            _logger.LogInformation("New Inventory creating..");
            if (Inventory == null)
            {
                return BadRequest("Inventory is null");
            }
            var response = await _inventoryBusiness.CreateInventory(Inventory);
            if (response == null)
            {
                _logger.LogWarning("Inventory not created!");
                return NotFound();
            }
            return Created("", response);
        }

        /// <summary>
        /// Delete existing Inventory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Inventory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Inventory deleting, Id is {0}", id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            var response = await _inventoryBusiness.DeleteInventory(id);
            if (!response)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
