namespace GreenCycleFypAPI.Models
{
    public class RewardRedemption
    {
        public int? RewardRedemptionID { get; set; }
        public string? UserID { get; set; }
        public int? RewardID { get; set; }
        public bool? IsUsed { get; set; }
        public DateTime? RedeemedDate { get; set; }
        public string? RewardName { get; set; }
        public string? RewardDescription { get; set; }
        public int? PointsRequired { get; set; }
        public string? RewardImageURL { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}