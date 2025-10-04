using GreenCycleFypAPI.DBContext;
using GreenCycleFypAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenCycleFypAPI.Controllers
{
    [Route("GreenCycleAPI/Reward")]
    [ApiController]
    public class RewardController : Controller
    {
        private readonly GreenCycleDBContext _context;
        public RewardController(GreenCycleDBContext context)
        {
            _context = context;
        }

        // GET: GreenCycleAPI/Reward
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reward>>> GetAllReward()
        {
            var rewardList = await _context.Reward.ToListAsync();
            return rewardList;
        }

        //GET: GreenCycleAPI/Reward/1
        [HttpGet("{RewardID:int}")]
        public async Task<ActionResult<Reward>> GetRewardDetails(int RewardID)
        {
            var rewardDetails = await _context.Reward.FindAsync(RewardID);

            if (rewardDetails == null)
            {
                return NotFound(new { status = 404, message = "Reward not found" });
            }

            return rewardDetails;
        }

        //POST: GreenCycleAPI/Reward
        [HttpPost]
        public async Task<ActionResult<Reward>> PostReward(Reward reward)
        {
            _context.Reward.Add(reward);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRewardDetails", new { RewardID = reward.RewardID }, reward);
        }

        //PUT: GreenCycleAPI/Reward/1
        [HttpPut("{RewardID:int}")]
        public async Task<IActionResult> PutReward(int RewardID, Reward reward)
        {
            if (RewardID != reward.RewardID)
            {
                return NotFound(new { status = 404, message = "Reward not found" });
            }

            _context.Entry(reward).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RewardExists(RewardID))
                {
                    return NotFound(new { status = 404, message = "Reward not found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { status = 200, message = "Reward updated successfully." });
        }

        //DELETE: GreenCycleAPI/Reward/1
        [HttpDelete("{RewardID:int}")]
        public async Task<IActionResult> DeleteReward(int RewardID)
        {
            var reward = await _context.Reward.FindAsync(RewardID);
            if (reward == null)
            {
                return NotFound(new { status = 404, message = "Reward not found" });
            }

            _context.Reward.Remove(reward);
            await _context.SaveChangesAsync();

            return Ok(new { status = 200, message = "Reward deleted successfully." });
        }

        private bool RewardExists(int RewardID) =>
           _context.Reward.Any(e => e.RewardID == RewardID);

    }
}
