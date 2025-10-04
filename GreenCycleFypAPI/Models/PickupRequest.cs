namespace GreenCycleFypAPI.Models
{
    public class PickupRequest
    {
        public string? PickupRequestID { get; set; }
        public string? UserID { get; set; }
        public string? CollectorUserID { get; set; }
        public string? PickupLocation { get; set; }
        public double? PickupLatitude { get; set; }
        public double? PickupLongtitude { get; set; }
        public string? Remarks { get; set; }
        public DateTime? PickupDate { get; set; }
        public string? PickupTimeRange { get; set; }
        public List<string>? PickupItemImageURL { get; set; }
        public string? PickupItemDescription { get; set; }
        public string? PickupItemCategory { get; set; }
        public int? PickupItemQuantity { get; set; }
        public string? PickupItemCondition { get; set; }
        public string? PickupRequestStatus { get; set; }
        public string? CollectionProofImageURL { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? CompletedDate { get; set; }

    }
}