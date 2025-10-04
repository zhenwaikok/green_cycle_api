using GreenCycleFypAPI.DBContext;
using GreenCycleFypAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenCycleFypAPI.Controllers
{
    [Route("GreenCycleAPI/User")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly GreenCycleDBContext _context;

        public UserController(GreenCycleDBContext context)
        {
            _context = context;
        }


        // GET: GreenCycleAPI/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var userList = await _context.User.ToListAsync();
            return userList;
        }

        //GET: GreenCycleAPI/User/{firebaseUserId}
        [HttpGet("{UserID}")]
        public async Task<ActionResult<User>> GetUserWithUserId(string UserID)
        {
            var user = await _context.User.FindAsync(UserID);

            if (user == null)
            {
                return NotFound(new { status = 404, message = "User not found" });
            }

            return user;
        }

        //POST: GreenCycleAPI/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserWithUserId", new { UserId = user.UserID }, user);
        }

        //PUT: GreenCycleAPI/User/{firebaseUserId}
        [HttpPut("{UserID}")]
        public async Task<IActionResult> PutUser(string UserID, User user)
        {

            if (UserID != user.UserID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(UserID))
                {
                    return NotFound(new { status = 404, message = "User not found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { status = 200, message = "Account updated successfully." });
        }

        //DELETE: GreenCycleAPI/User/{firebaseUserId}
        [HttpDelete("{UserID}")]
        public async Task<IActionResult> DeleteUser(string UserID)
        {
            var user = await _context.User.FindAsync(UserID);
            if (user == null)
            {
                return NotFound(new { status = 404, message = "User not found" });
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { status = 200, message = "User deleted successfully." });
        }

        private bool UserExists(string UserID) =>
             _context.User.Any(e => e.UserID == UserID);
    }
}
