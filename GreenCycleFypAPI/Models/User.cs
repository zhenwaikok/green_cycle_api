namespace GreenCycleFypAPI.Models
{
    public class User
    {
        public string? UserID { get; set; }
        public string? UserRole { get; set; }
        public string? FullName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? Address1 { get; set; }       
        public string? Address2 { get; set; }       
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? State { get; set; }
        public string? VehicleType { get; set; }
        public string? VehiclePlateNumber { get; set; }
        public string? CompanyName { get; set; }
        public string? ProfileImageURL { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? AccountRejectMessage { get; set; }
        public int? CurrentPoint { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}