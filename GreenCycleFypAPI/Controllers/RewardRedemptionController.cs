using GreenCycleFypAPI.DBContext;
using GreenCycleFypAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenCycleFypAPI.Controllers
{
    [Route("GreenCycleAPI/RewardRedemption")]
    [ApiController]
    public class RewardRedemptionController : Controller
    {
        private readonly GreenCycleDBContext _context;
        public RewardRedemptionController(GreenCycleDBContext context)
        {
            _context = context;
        }

        // GET: GreenCycleAPI/RewardRedemption
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RewardRedemption>>> GetAllRewardRedemption()
        {
            var rewardRedemptionList = await _context.RewardRedemption.ToListAsync();
            return rewardRedemptionList;
        }

        //GET: GreenCycleAPI/RewardRedemption/{firebaseUserId}
        [HttpGet("{UserID}")]
        public async Task<ActionResult<IEnumerable<RewardRedemption>>> GetRewardRedemptionWithUserId(string UserID)
        {
            var rewardRedemptionList = await _context.RewardRedemption
                .Where(rewardRedemption => rewardRedemption.UserID == UserID)
                .ToListAsync();

            return Ok(rewardRedemptionList);
        }

        //GET: GreenCycleAPI/RewardRedemption/1
        [HttpGet("{RewardRedemptionID:int}")]
        public async Task<ActionResult<RewardRedemption>> GetRewardRedemptionDetails(int RewardRedemptionID)
        {
            var rewardRedemptionDetails = await _context.RewardRedemption.FindAsync(RewardRedemptionID);

            if (rewardRedemptionDetails == null)
            {
                return NotFound(new { status = 404, message = "Reward redemption not found" });
            }

            return Ok(rewardRedemptionDetails);
        }

        //POST: GreenCycleAPI/RewardRedemption
        [HttpPost]
        public async Task<ActionResult<RewardRedemption>> PostRewardRedemption(RewardRedemption rewardRedemption)
        {
            _context.RewardRedemption.Add(rewardRedemption);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRewardRedemptionDetails", new { RewardRedemptionID = rewardRedemption.RewardRedemptionID }, rewardRedemption);
        }

        //PUT: GreenCycleAPI/RewardRedemption/1
        [HttpPut("{RewardRedemptionID:int}")]
        public async Task<IActionResult> PutRewardRedemption(int RewardRedemptionID, RewardRedemption rewardRedemption)
        {
            if (RewardRedemptionID != rewardRedemption.RewardRedemptionID)
            {
                return NotFound(new { status = 404, message = "Reward redemption not found" });
            }

            _context.Entry(rewardRedemption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RewardRedemptionExists(RewardRedemptionID))
                {
                    return NotFound(new { status = 404, message = "Reward redemption not found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { status = 200, message = "Reward redemption updated successfully." });
        }

        //DELETE: GreenCycleAPI/RewardRedemption/1
        [HttpDelete("{RewardRedemptionID:int}")]
        public async Task<IActionResult> DeleteRewardRedemption(int RewardRedemptionID)
        {
            var rewardRedemption = await _context.RewardRedemption.FindAsync(RewardRedemptionID);
            if (rewardRedemption == null)
            {
                return NotFound(new { status = 404, message = "Reward redemption not found" });
            }

            _context.RewardRedemption.Remove(rewardRedemption);
            await _context.SaveChangesAsync();

            return Ok(new { status = 200, message = "Reward redemption deleted successfully." });
        }

        private bool RewardRedemptionExists(int RewardRedemptionID) =>
            _context.RewardRedemption.Any(e => e.RewardRedemptionID == RewardRedemptionID);

    }
}
