using GreenCycleFypAPI.DBContext;
using GreenCycleFypAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenCycleFypAPI.Controllers
{
    [Route("GreenCycleAPI/Awareness")]
    [ApiController]
    public class AwarenessController : Controller
    {
        private readonly GreenCycleDBContext _context;
        public AwarenessController(GreenCycleDBContext context)
        {
            _context = context;
        }

        // GET: GreenCycleAPI/Awareness
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Awareness>>> GetAllAwareness()
        {
            var awarenessList = await _context.Awareness.ToListAsync();
            return awarenessList;
        }

        //GET: GreenCycleAPI/Awareness/1
        [HttpGet("{AwarenessID:int}")]
        public async Task<ActionResult<Awareness>> GetAwarenessDetails(int AwarenessID)
        {
            var awarenessDetails = await _context.Awareness.FindAsync(AwarenessID);

            if (awarenessDetails == null)
            {
                return NotFound(new { status = 404, message = "Awareness not found" });
            }

            return awarenessDetails;
        }

        //POST: GreenCycleAPI/Awareness
        [HttpPost]
        public async Task<ActionResult<Awareness>> PostVehicle(Awareness awareness)
        {
            _context.Awareness.Add(awareness);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAwarenessDetails", new { AwarenessID = awareness.AwarenessID }, awareness);
        }

        //PUT: GreenCycleAPI/Awareness/1
        [HttpPut("{AwarenessID:int}")]
        public async Task<IActionResult> PutServiceLog(int AwarenessID, Awareness awareness)
        {
            if (AwarenessID != awareness.AwarenessID)
            {
                return NotFound(new { status = 404, message = "Awareness not found" });
            }

            _context.Entry(awareness).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AwarenessExists(AwarenessID))
                {
                    return NotFound(new { status = 404, message = "Awareness not found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { status = 200, message = "Awareness updated successfully." });
        }

        //DELETE: GreenCycleAPI/Awareness/1
        [HttpDelete("{AwarenessID:int}")]
        public async Task<IActionResult> DeleteAwareness(int AwarenessID)
        {
            var awareness = await _context.Awareness.FindAsync(AwarenessID);
            if (awareness == null)
            {
                return NotFound(new { status = 404, message = "Awareness not found" });
            }

            _context.Awareness.Remove(awareness);
            await _context.SaveChangesAsync();

            return Ok(new { status = 200, message = "Awareness deleted successfully." });
        }

        private bool AwarenessExists(int AwarenessID) =>
           _context.Awareness.Any(e => e.AwarenessID == AwarenessID);

    }
}
