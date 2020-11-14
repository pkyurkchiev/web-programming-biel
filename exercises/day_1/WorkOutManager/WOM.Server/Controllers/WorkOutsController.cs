using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WOM.Models;
using WOM.Server.Data;

namespace WOM.Server.Controllers
{
    [EnableCors]
    [ODataRoutePrefix("workouts")]
    public class WorkOutsController : ODataController
    {
        private readonly WorkOutContext _context;

        public WorkOutsController(WorkOutContext context)
        {
            _context = context;
        }

        // GET: api/WorkOut
        [EnableQuery]
        [ODataRoute]
        public IEnumerable<WorkOut> GetWorkOutList()
        {
            return _context.WorkOutList;
        }

        // GET: api/WorkOut(5)
        [EnableQuery]
        [ODataRoute("({id})")]
        public async Task<IActionResult> GetWorkOut([FromODataUri] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workOut = await _context.WorkOutList.FindAsync(id);

            if (workOut == null)
            {
                return NotFound();
            }

            return Ok(workOut);
        }

        // PATCH: api/WorkOut(5)
        [ODataRoute("({id})")]
        public async Task<IActionResult> Patch([FromODataUri] int id, [FromBody]WorkOut workOut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workOut.Id)
            {
                return BadRequest();
            }

            var check = _context.WorkOutList.Where(item => item.Id == workOut.Id).First();

            if (!check.Complete && workOut.Complete)
            {
                check.MarkComplete();
            }

            check.Description = workOut.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkOutExists(id))
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

        // POST: api/WorkOut
        [ODataRoute]
        public async Task<IActionResult> Post([FromBody] WorkOut workOut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.WorkOutList.Add(workOut);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkOut", new { id = workOut.Id }, workOut);
        }

        // DELETE: api/WorkOut(5)
        [ODataRoute("({id})")]
        public async Task<IActionResult> Delete([FromODataUri] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workOut = await _context.WorkOutList.FindAsync(id);
            if (workOut == null)
            {
                return NotFound();
            }

            _context.WorkOutList.Remove(workOut);
            await _context.SaveChangesAsync();

            return Ok(workOut);
        }

        private bool WorkOutExists(int id)
        {
            return _context.WorkOutList.Any(e => e.Id == id);
        }
    }
}