using GreenCycleFypAPI.DBContext;
using GreenCycleFypAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenCycleFypAPI.Controllers
{
    
    [Route("GreenCycleAPI/CollectorLocations")]
    [ApiController]
    public class CollectorLocationsController : Controller
    {
        private readonly GreenCycleDBContext _context;

        public CollectorLocationsController(GreenCycleDBContext context)
        {
            _context = context;
        }

        // GET: GreenCycleAPI/CollectorLocations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectorLocations>>> GetAllCollectorLocations()
        {
            var collectorLocationList = await _context.CollectorLocations.ToListAsync();
            return collectorLocationList;
        }

        //GET: GreenCycleAPI/CollectorLocations/{firebaseUserId}
        [HttpGet("{CollectorUserID}")]
        public async Task<ActionResult<CollectorLocations>> GetCollectorLocationsDetailsWithUserID(string CollectorUserID)
        {
            var collectorLocationsDetails = await _context.CollectorLocations.FindAsync(CollectorUserID);


            if (collectorLocationsDetails == null)
            {
                return NotFound(new { status = 404, message = "Collector not found" });
            }

            return collectorLocationsDetails;
        }


        //POST: GreenCycleAPI/CollectorLocations
        [HttpPost]
        public async Task<ActionResult<CollectorLocations>> PostCollectorLocations(CollectorLocations collectorLocations)
        {
            _context.CollectorLocations.Add(collectorLocations);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCollectorLocationsDetailsWithUserID", new { CollectorUserId = collectorLocations.CollectorUserID }, collectorLocations);
        }

        //PUT: GreenCycleAPI/CollectorLocations/{firebaseUserId}
        [HttpPut("{CollectorUserID}")]
        public async Task<IActionResult> PutCollectorLocations(string CollectorUserID, CollectorLocations collectorLocations)
        {

            if (CollectorUserID != collectorLocations.CollectorUserID)
            {
                return BadRequest();
            }

            _context.Entry(collectorLocations).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectorLocationExists(CollectorUserID))
                {
                    return NotFound(new { status = 404, message = "Collector location not found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { status = 200, message = "Collector location updated successfully." });
        }

        //DELETE: GreenCycleAPI/CollectorLocations/{firebaseUserId}
        [HttpDelete("{CollectorUserID}")]
        public async Task<IActionResult> DeleteCollectorLocations(string CollectorUserID)
        {
            var collectorLocation = await _context.CollectorLocations.FindAsync(CollectorUserID);

            if (collectorLocation == null)
            {
                return NotFound(new { status = 404, message = "Collector location not found" });
            }

            _context.CollectorLocations.Remove(collectorLocation);
            await _context.SaveChangesAsync();

            return Ok(new { status = 200, message = "Collector location deleted successfully." });
        }

        private bool CollectorLocationExists(string CollectorUserID) =>
             _context.CollectorLocations.Any(e => e.CollectorUserID == CollectorUserID);
    }
}
