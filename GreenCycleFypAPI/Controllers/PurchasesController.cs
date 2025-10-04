using GreenCycleFypAPI.DBContext;
using GreenCycleFypAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenCycleFypAPI.Controllers
{
    [Route("GreenCycleAPI/Purchases")]
    [ApiController]
    public class PurchasesController : Controller
    {
        private readonly GreenCycleDBContext _context;
        public PurchasesController(GreenCycleDBContext context)
        {
            _context = context;
        }

        // GET: GreenCycleAPI/Purchases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Purchases>>> GetAllPurchases()
        {
            var purchasesList = await _context.Purchases.ToListAsync();
            return purchasesList;
        }

        //GET: GreenCycleAPI/Purchases/User/{firebaseUserId}
        [HttpGet("User/{UserID}")]
        public async Task<ActionResult<IEnumerable<Purchases>>> GetPurchasesWithUserId(string UserID)
        {
            var purchasesList = await _context.Purchases
                                .Where(purchases => purchases.BuyerUserID == UserID)
                                .ToListAsync();

            return Ok(purchasesList);
        }

        //GET: GreenCycleAPI/Purchases/Seller/{firebaseUserId}
        [HttpGet("Seller/{SellerUserID}")]
        public async Task<ActionResult<IEnumerable<Purchases>>> GetPurchasesWitSellerhUserId(string SellerUserID)
        {
            var purchasesList = await _context.Purchases
                                .Where(purchases => purchases.SellerUserID == SellerUserID)
                                .ToListAsync();

            return Ok(purchasesList);
        }

        //GET: GreenCycleAPI/Purchases/P123
        [HttpGet("{PurchaseID:int}")]
        public async Task<ActionResult<Purchases>> GetPurchaseDetails(int PurchaseID)
        {
            var purchaseDetails = await _context.Purchases.FindAsync(PurchaseID);

            if (purchaseDetails == null)
            {
                return NotFound(new { status = 404, message = "Purchase not found" });
            }

            return purchaseDetails;
        }

        //POST: GreenCycleAPI/Purchases
        [HttpPost]
        public async Task<ActionResult<Purchases>> PostPurchases(Purchases purchases)
        {
            _context.Purchases.Add(purchases);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseDetails", new { PurchaseID = purchases.PurchaseID }, purchases);
        }

        //PUT: GreenCycleAPI/Purchases/1
        [HttpPut("{PurchaseID:int}")]
        public async Task<IActionResult> PutPurchases(int PurchaseID, Purchases purchases)
        {
            if (PurchaseID != purchases.PurchaseID)
            {
                return NotFound(new { status = 404, message = "Purchase not found" });
            }

            _context.Entry(purchases).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchasesExists(PurchaseID))
                {
                    return NotFound(new { status = 404, message = "Purchase not found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { status = 200, message = "Purchase updated successfully." });
        }

        //DELETE: GreenCycleAPI/Purchases/1
        [HttpDelete("{PurchaseID:int}")]
        public async Task<IActionResult> DeletePurchases(int PurchaseID)
        {
            var purchases = await _context.Purchases.FindAsync(PurchaseID);
            if (purchases == null)
            {
                return NotFound(new { status = 404, message = "Purchase not found" });
            }

            _context.Purchases.Remove(purchases);
            await _context.SaveChangesAsync();

            return Ok(new { status = 200, message = "Purchase deleted successfully." });
        }

        private bool PurchasesExists(int PurchaseID) =>
           _context.Purchases.Any(e => e.PurchaseID == PurchaseID);
    }
}
