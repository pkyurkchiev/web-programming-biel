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

namespace GM.Server.Controllers
{
    [EnableCors]
    [ODataRoutePrefix("groceries")]
    public class GroceriesController : ODataController
    {
        private readonly GroceryDbContext _context;

        public GroceriesController(GroceryDbContext context)
        {
            _context = context;
        }

        // GET: api/Grocery
        [EnableQuery]
        [ODataRoute]
        public IEnumerable<Grocery> GetGroceryList()
        {
            return _context.Groceries;
        }

        // GET: api/Grocery(5)
        [EnableQuery]
        [ODataRoute("({id})")]
        public async Task<IActionResult> GetGrocery([FromODataUri] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workOut = await _context.Groceries.FindAsync(id);

            if (workOut == null)
            {
                return NotFound();
            }

            return Ok(workOut);
        }

        // PATCH: api/Grocery(5)
        [ODataRoute("({id})")]
        public async Task<IActionResult> Patch([FromODataUri] int id, [FromBody]Grocery workOut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workOut.Id)
            {
                return BadRequest();
            }

            var check = _context.Groceries.Where(item => item.Id == workOut.Id).First();

            if (!check.IsExpire && workOut.IsExpire)
            {
                check.MarkAsExpire();
            }

            check.Name = workOut.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroceryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Grocery
        [ODataRoute]
        public async Task<IActionResult> Post([FromBody] Grocery workOut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Groceries.Add(workOut);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkOut", new { id = workOut.Id }, workOut);
        }

        // DELETE: api/Grocery(5)
        [ODataRoute("({id})")]
        public async Task<IActionResult> Delete([FromODataUri] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workOut = await _context.Groceries.FindAsync(id);
            if (workOut == null)
            {
                return NotFound();
            }

            _context.Groceries.Remove(workOut);
            await _context.SaveChangesAsync();

            return Ok(workOut);
        }

        private bool GroceryExists(int id)
        {
            return _context.Groceries.Any(e => e.Id == id);
        }
    }
}