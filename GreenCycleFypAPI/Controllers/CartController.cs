using GreenCycleFypAPI.DBContext;
using GreenCycleFypAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenCycleFypAPI.Controllers
{
    [Route("GreenCycleAPI/Cart")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly GreenCycleDBContext _context;
        public CartController(GreenCycleDBContext context)
        {
            _context = context;
        }

        // GET: GreenCycleAPI/Cart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetAllCart()
        {
            var cartList = await _context.Cart
                .Include(cart => cart.ItemListing)
                .ToListAsync();
            return cartList;
        }

        //GET: GreenCycleAPI/Cart/{firebaseUserId}
        [HttpGet("{UserID}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartWithUserId(string UserID)
        {
            var cartList = await _context.Cart
                .Where(cart => cart.BuyerUserID == UserID)
                .Include(cart => cart.ItemListing)
                .ToListAsync();

            return Ok(cartList);
        }


        //GET: GreenCycleAPI/Cart/1
        [HttpGet("{CartID:int}")]
        public async Task<ActionResult<Cart>> GetCartDetails(int CartID)
        {
            var cartDetails = await _context.Cart
                .Include(cart => cart.ItemListing)
                .FirstOrDefaultAsync(cart => cart.CartID == CartID);

            if (cartDetails == null)
            {
                return NotFound(new { status = 404, message = "Cart not found" });
            }

            return Ok(cartDetails);
        }

        //POST: GreenCycleAPI/Cart
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            _context.Cart.Add(cart);
            await _context.SaveChangesAsync();

            var createdCart = await _context.Cart
                .Include(c => c.ItemListing)
                .FirstOrDefaultAsync(c => c.CartID == cart.CartID);

            return CreatedAtAction("GetCartDetails", new { CartID = cart.CartID }, createdCart);
        }

        //DELETE: GreenCycleAPI/Cart/1
        [HttpDelete("{CartID:int}")]
        public async Task<IActionResult> DeleteCart(int CartID)
        {
            var cart = await _context.Cart.FindAsync(CartID);
            if (cart == null)
            {
                return NotFound(new { status = 404, message = "Cart not found" });
            }

            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();

            return Ok(new { status = 200, message = "Cart deleted successfully." });
        }
    }
}
