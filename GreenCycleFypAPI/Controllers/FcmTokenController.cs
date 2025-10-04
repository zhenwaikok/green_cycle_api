using GreenCycleFypAPI.DBContext;
using GreenCycleFypAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenCycleFypAPI.Controllers
{
    [Route("GreenCycleAPI/FcmToken")]
    [ApiController]
    public class FcmTokenController : Controller
    {
        private readonly GreenCycleDBContext _context;
        public FcmTokenController(GreenCycleDBContext context)
        {
            _context = context;
        }


        // GET: GreenCycleAPI/FcmToken
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FcmToken>>> GetAllUsersFcmToken()
        {
            var userFcmTokenList = await _context.FcmToken.ToListAsync();
            return userFcmTokenList;
        }

        //GET: GreenCycleAPI/FcmToken/{firebaseUserId}
        [HttpGet("{UserID}")]
        public async Task<ActionResult<FcmToken>> GetFcmTokenWithUserId(string UserID)
        {
            var user = await _context.FcmToken.FindAsync(UserID);

            if (user == null)
            {
                return NotFound(new { status = 404, message = "User not found" });
            }

            return user;
        }

        // POST: GreenCycleAPI/FcmToken
        [HttpPost]
        public async Task<ActionResult<FcmToken>> PostOrUpdateFcmToken(FcmToken fcmToken)
        {
            var existingToken = await _context.FcmToken.FindAsync(fcmToken.UserID);

            if (existingToken == null)
            {
                // Insert new token
                _context.FcmToken.Add(fcmToken);
            }
            else
            {
                // Update existing token
                existingToken.Token = fcmToken.Token;
                _context.FcmToken.Update(existingToken);
            }

            await _context.SaveChangesAsync();

            return Ok(new { status = 200, message = "Token saved successfully" });
        }

        //DELETE: GreenCycleAPI/FcmToken/{firebaseUserID}
        [HttpDelete("{UserID}")]
        public async Task<IActionResult> DeleteUserFcmToken(string UserID)
        {
            var fcmToken = await _context.FcmToken.FindAsync(UserID);
            if (fcmToken == null)
            {
                return NotFound(new { status = 404, message = "User not found" });
            }

            _context.FcmToken.Remove(fcmToken);
            await _context.SaveChangesAsync();

            return Ok(new { status = 200, message = "User fcm token deleted successfully." });
        }


    }
}
