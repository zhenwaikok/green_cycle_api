namespace GreenCycleFypAPI.Models
{
    public class Cart
    {
        public int? CartID { get; set; }
        public string? BuyerUserID { get; set; }
        public string? SellerUserID { get; set; }
        public int? ItemListingID { get; set; }
        public DateTime? AddedDate { get; set; }
        public ItemListing? ItemListing { get; set; }
    }
}
