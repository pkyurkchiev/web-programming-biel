using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GM.Models;
using GM.Server.Data;

namespace GM.Server.Controllers {
    [EnableCors]
    [ODataRoutePrefix("groceries")]
    public class GroceriesController : ODataController {
        private readonly GroceryDbContext _context;
        //returns list with all Grocery product
        public GroceriesController(GroceryDbContext context) {
            _context = context;
        }

        // GET: api/Grocery
        [EnableQuery]
        [ODataRoute]
        public IEnumerable<Grocery> GetGroceryList() {
            return _context.Groceries;
        }

        // GET: api/Grocery(5)
        [EnableQuery]
        [ODataRoute("({id})")]
        //makes a search in the Data Base for all Grocery items with Id
        public async Task<IActionResult> GetGrocery([FromODataUri] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var grocery = await _context.Groceries.FindAsync(id);

            if (grocery == null) {
                return NotFound();
            }

            return Ok(grocery);
        }

        // PATCH: api/Grocery(5)
        [ODataRoute("({id})")]
        public async Task<IActionResult> Patch([FromODataUri] int id, [FromBody]Grocery grocery) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != grocery.Id) {
                return BadRequest();
            }

            var check = _context.Groceries.Where(item => item.Id == grocery.Id).First();

            if (!check.IsExpire && grocery.IsExpire) {
                check.MarkAsExpire();
            }

            check.Name = grocery.Name;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!GroceryExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Grocery
        [ODataRoute]
        public async Task<IActionResult> Post([FromBody] Grocery grocery) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            _context.Groceries.Add(grocery);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrocery", new { id = grocery.Id }, grocery);
        }

        // Remove Item: api/Grocery(5)
        [ODataRoute("({id})")]
        public async Task<IActionResult> RemoveItem([FromODataUri] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var grocery = await _context.Groceries.FindAsync(id);
            if (grocery == null) {
                return NotFound();
            }

            _context.Groceries.Remove(grocery);
            await _context.SaveChangesAsync();

            return Ok(grocery);
        }

        private bool GroceryExists(int id) {
            return _context.Groceries.Any(e => e.Id == id);
        }
    }
}