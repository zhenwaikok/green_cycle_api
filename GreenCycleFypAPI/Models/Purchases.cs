namespace GreenCycleFypAPI.Models
{
    public class Purchases
    {
        public int? PurchaseID { get; set; }
        public string? BuyerUserID { get; set; }
        public string? SellerUserID { get; set; }
        public int? ItemListingID { get; set; } 
        public string? PurchaseGroupID { get; set; }
        public string? ItemName { get; set; }
        public double? ItemPrice { get; set; }
        public string? ItemCondition { get; set; }
        public string? ItemCategory { get; set; }
        public List<string>? ItemImageURL { get; set; }
        public string? DeliveryAddress { get; set; }
        public bool? IsDelivered { get; set; }
        public string? Status { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
    }
}
