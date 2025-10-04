namespace GreenCycleFypAPI.Models
{
    public class ItemListing
    {
        public int? ItemListingID { get; set; }
        public string? UserID { get; set; }
        public List<string>? ItemImageURL { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public double? ItemPrice { get; set; }
        public string? ItemCondition { get; set; }
        public string? ItemCategory { get; set; }
        public bool? IsSold { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}