using GreenCycleFypAPI.DBContext;
using GreenCycleFypAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenCycleFypAPI.Controllers
{
    [Route("GreenCycleAPI/ItemListing")]
    [ApiController]
    public class ItemListingController : Controller
    {
        private readonly GreenCycleDBContext _context;
        public ItemListingController(GreenCycleDBContext context)
        {
            _context = context;
        }

        // GET: GreenCycleAPI/ItemListing
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemListing>>> GetAllItemListing()
        {
            var itemListingList = await _context.ItemListing.ToListAsync();
            return itemListingList;
        }

        //GET: GreenCycleAPI/ItemListing/{firebaseUserId}
        [HttpGet("{UserID}")]
        public async Task<ActionResult<IEnumerable<ItemListing>>> GetItemListingWithUserId(string UserID)
        {
            var itemListingList = await _context.ItemListing
                                .Where(itemListing => itemListing.UserID == UserID)
                                .ToListAsync();

            return Ok(itemListingList);
        }


        //GET: GreenCycleAPI/ItemListing/1
        [HttpGet("{ItemListingID:int}")]
        public async Task<ActionResult<ItemListing>> GetItemListingDetails(int ItemListingID)
        {
            var itemListingDetails = await _context.ItemListing.FindAsync(ItemListingID);

            if (itemListingDetails == null)
            {
                return NotFound(new { status = 404, message = "Item listing not found" });
            }

            return itemListingDetails;
        }

        //POST: GreenCycleAPI/ItemListing
        [HttpPost]
        public async Task<ActionResult<ItemListing>> PostItemListing(ItemListing itemListing)
        {
            _context.ItemListing.Add(itemListing);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemListingDetails", new { ItemListingID = itemListing.ItemListingID }, itemListing);
        }

        //PUT: GreenCycleAPI/ItemListing/1
        [HttpPut("{ItemListingID:int}")]
        public async Task<IActionResult> PutItemListing(int ItemListingID, ItemListing itemListing)
        {
            if (ItemListingID != itemListing.ItemListingID)
            {
                return NotFound(new { status = 404, message = "Item listing not found" });
            }

            _context.Entry(itemListing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemListingExists(ItemListingID))
                {
                    return NotFound(new { status = 404, message = "Item listing not found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { status = 200, message = "Item listing updated successfully." });
        }

        //DELETE: GreenCycleAPI/ItemListing/1
        [HttpDelete("{ItemListingID:int}")]
        public async Task<IActionResult> DeleteItemListing(int ItemListingID)
        {
            var itemListing = await _context.ItemListing.FindAsync(ItemListingID);
            if (itemListing == null)
            {
                return NotFound(new { status = 404, message = "Item listing not found" });
            }

            _context.ItemListing.Remove(itemListing);
            await _context.SaveChangesAsync();

            return Ok(new { status = 200, message = "Item listing deleted successfully." });
        }

        private bool ItemListingExists(int ItemListingID) =>
           _context.ItemListing.Any(e => e.ItemListingID == ItemListingID);
    }
}
