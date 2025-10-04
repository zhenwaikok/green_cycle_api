namespace GreenCycleFypAPI.Models
{
    public class Reward
    {
        public int? RewardID { get; set; }
        public string? RewardName { get; set; }
        public string? RewardDescription { get; set; }
        public int? PointsRequired { get; set; }
        public string? RewardImageURL { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
