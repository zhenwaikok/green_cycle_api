using GreenCycleFypAPI.DBContext;
using GreenCycleFypAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenCycleFypAPI.Controllers
{
    [Route("GreenCycleAPI/PickupRequest")]
    [ApiController]
    public class PickupRequestController : Controller
    {
        private readonly GreenCycleDBContext _context;
        public PickupRequestController(GreenCycleDBContext context)
        {
            _context = context;
        }

        // GET: GreenCycleAPI/PickupRequest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PickupRequest>>> GetAllPickupRequest()
        {
            var pickupRequestList = await _context.PickupRequest.ToListAsync();
            return pickupRequestList;
        }

        //GET: GreenCycleAPI/PickupRequest/User/{firebaseUserId}
        [HttpGet("User/{UserID}")]
        public async Task<ActionResult<IEnumerable<PickupRequest>>> GetPickupRequestWithUserId(string UserID)
        {
            var pickupRequestList = await _context.PickupRequest
                                .Where(pickupRequest => pickupRequest.UserID == UserID)
                                .ToListAsync();

            return Ok(pickupRequestList);
        }

        //GET: GreenCycleAPI/PickupRequest/{PickupRequestID}
        [HttpGet("{PickupRequestID}")]
        public async Task<ActionResult<PickupRequest>> GetPickupRequestDetails(string PickupRequestID)
        {
            var pickupRequestDetails = await _context.PickupRequest.FindAsync(PickupRequestID);

            if (pickupRequestDetails == null)
            {
                return NotFound(new { status = 404, message = "Pickup Request not found" });
            }

            return pickupRequestDetails;
        }

        //POST: GreenCycleAPI/PickupRequest
        [HttpPost]
        public async Task<ActionResult<PickupRequest>> PostPickupRequest(PickupRequest pickupRequest)
        {
            _context.PickupRequest.Add(pickupRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPickupRequestDetails", new { PickupRequestID = pickupRequest.PickupRequestID }, pickupRequest);
        }

        //PUT: GreenCycleAPI/PickupRequest/{PickupRequestID}
        [HttpPut("{PickupRequestID}")]
        public async Task<IActionResult> PutItemListing(string PickupRequestID, PickupRequest pickupRequest)
        {
            if (PickupRequestID != pickupRequest.PickupRequestID)
            {
                return NotFound(new { status = 404, message = "Pickup request not found" });
            }

            _context.Entry(pickupRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PickupRequestExists(PickupRequestID))
                {
                    return NotFound(new { status = 404, message = "Pickup request not found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { status = 200, message = "Pickup request updated successfully." });
        }

        //DELETE: GreenCycleAPI/PickupRequest/{PickupRequestID}
        [HttpDelete("{PickupRequestID}")]
        public async Task<IActionResult> DeletePickupRequest(string PickupRequestID)
        {
            var pickupRequest = await _context.PickupRequest.FindAsync(PickupRequestID);
            if (pickupRequest == null)
            {
                return NotFound(new { status = 404, message = "Pickup request not found" });
            }

            _context.PickupRequest.Remove(pickupRequest);
            await _context.SaveChangesAsync();

            return Ok(new { status = 200, message = "Pickup request deleted successfully." });
        }

        private bool PickupRequestExists(string PickupRequestID) =>
           _context.PickupRequest.Any(e => e.PickupRequestID == PickupRequestID);
    }
}
