using GreenCycleFypAPI.DBContext;
using GreenCycleFypAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenCycleFypAPI.Controllers
{
    [Route("GreenCycleAPI/Points")]
    [ApiController]
    public class PointsController : Controller
    {
        private readonly GreenCycleDBContext _context;
        public PointsController(GreenCycleDBContext context)
        {
            _context = context;
        }

        // GET: GreenCycleAPI/Points
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Points>>> GetAllPointTransaction()
        {
            var pointsTransactionList = await _context.Points.ToListAsync();
            return pointsTransactionList;
        }

        //GET: GreenCycleAPI/Points/{firebaseUserId}
        [HttpGet("{UserID}")]
        public async Task<ActionResult<IEnumerable<Points>>> GetPointTransactionWithUserId(string UserID)
        {
            var pointsTransactionList = await _context.Points
                                .Where(points => points.UserID == UserID)
                                .ToListAsync();

            return Ok(pointsTransactionList);
        }

        //GET: GreenCycleAPI/Points/1
        [HttpGet("{PointsID:int}")]
        public async Task<ActionResult<Points>> GetPointTransactionDetails(int PointsID)
        {
            var pointTransactionDetails = await _context.Points.FindAsync(PointsID);

            if (pointTransactionDetails == null)
            {
                return NotFound(new { status = 404, message = "Point transaction not found" });
            }

            return pointTransactionDetails;
        }

        //POST: GreenCycleAPI/Points
        [HttpPost]
        public async Task<ActionResult<Points>> PostPointTransaction(Points points)
        {
            _context.Points.Add(points);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPointTransactionDetails", new { PointsID = points.PointID }, points);
        }

        //PUT: GreenCycleAPI/Points/1
        [HttpPut("{PointsID:int}")]
        public async Task<IActionResult> PutItemListing(int PointsID, Points points)
        {
            if (PointsID != points.PointID)
            {
                return NotFound(new { status = 404, message = "Point transaction not found" });
            }

            _context.Entry(points).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointTransactionExists(PointsID))
                {
                    return NotFound(new { status = 404, message = "Point transaction not found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { status = 200, message = "Point transaction updated successfully." });
        }

        //DELETE: GreenCycleAPI/Points/1
        [HttpDelete("{PointsID:int}")]
        public async Task<IActionResult> DeletePointTransaction(int PointsID)
        {
            var pointTransaction = await _context.Points.FindAsync(PointsID);
            if (pointTransaction == null)
            {
                return NotFound(new { status = 404, message = "Point transaction not found" });
            }

            _context.Points.Remove(pointTransaction);
            await _context.SaveChangesAsync();

            return Ok(new { status = 200, message = "Point transaction deleted successfully." });
        }

        private bool PointTransactionExists(int PointsID) =>
           _context.Points.Any(e => e.PointID == PointsID);
    }
}
