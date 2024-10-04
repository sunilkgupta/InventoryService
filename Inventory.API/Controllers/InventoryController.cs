using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory.API.Data;

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
        private readonly InventoryAPIContext _context;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="context"></param>
        public InventoryController(ILogger<InventoryController> logger, InventoryAPIContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Get all inventories data
        /// </summary>
        /// <returns></returns>
        // GET: api/Inventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Inventory>>> Get()
        {
            try
            {
                _logger.LogInformation("All Inventory requested");
                if (_context.Inventory == null)
                {
                    return NotFound();
                }
                return await _context.Inventory.ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred.");
            }
        }

        /// <summary>
        /// Get specific Inventory record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Inventory/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Models.Inventory), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Models.Inventory>> Get(Guid id)
        {
            try
            {
                _logger.LogInformation("Inventory requested: Id is {0}", id);
                if (_context.Inventory == null)
                {
                    return NotFound();
                }
                var inventory = await _context.Inventory.FindAsync(id);

                if (inventory == null)
                {
                    return NotFound();
                }
                return inventory;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred.");
            }
        }

        /// <summary>
        /// Update exising inventory
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inventory"></param>
        /// <returns></returns>
        // PUT: api/Inventory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Models.Inventory inventory)
        {
            _logger.LogInformation("Inventory updating: Id is {0}", id);
            try
            {
                if (id != inventory.ItemId)
                {
                    return BadRequest();
                }

                _context.Entry(inventory).State = EntityState.Modified;            
                await _context.SaveChangesAsync();
                return Ok(inventory);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!InventoryExists(id))
                {
                    _logger.LogError(ex, "Db Update Concurrency Exception occurred as Id is missing: {0}", id);
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, "An error occurred while processing the request.");
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred.");
            }
        }

        /// <summary>
        /// Create a new inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        // POST: api/Inventory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Models.Inventory>> Post(Models.Inventory inventory)
        {
            try
            {
                _logger.LogInformation("New Inventory creating..");
                if (_context.Inventory == null)
                {
                    return Problem("Entity set 'InventoryAPIContext.Inventory'  is null.");
                }
                _context.Inventory.Add(inventory);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Inventory created successfully, Id is {0}", inventory.ItemId);

                return CreatedAtAction("Get", new { id = inventory.ItemId }, inventory);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred.");
            }
        }

        /// <summary>
        /// Delete existing inventory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Inventory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                _logger.LogInformation("Inventory deleting, Id is {0}", id);
                if (_context.Inventory == null)
                {
                    return NotFound();
                }
                var inventory = await _context.Inventory.FindAsync(id);
                if (inventory == null)
                {
                    return NotFound();
                }

                _context.Inventory.Remove(inventory);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Inventory deleted successfully, Id is {0}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred.");
            }

        }

        private bool InventoryExists(Guid id)
        {
            return (_context.Inventory?.Any(e => e.ItemId == id)).GetValueOrDefault();
        }
    }
}
