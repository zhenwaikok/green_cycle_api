using GreenCycleFypAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace GreenCycleFypAPI.DBContext
{
    public partial class GreenCycleDBContext : DbContext
    {
        public GreenCycleDBContext() { }

        public GreenCycleDBContext(DbContextOptions<GreenCycleDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Awareness> Awareness { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CollectorLocations> CollectorLocations { get; set; }
        public virtual DbSet<ItemListing> ItemListing { get; set; }
        public virtual DbSet<PickupRequest> PickupRequest { get; set; }
        public virtual DbSet<Points> Points { get; set; }
        public virtual DbSet<Purchases> Purchases { get; set; }
        public virtual DbSet<Reward> Reward { get; set; }
        public virtual DbSet<RewardRedemption> RewardRedemption { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<FcmToken> FcmToken { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=greencycledbinstance.corcokrfmxet.us-east-1.rds.amazonaws.com;Initial Catalog=greencycledb;User ID=admin;Password=greencycleapi;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var stringListConverter = new ValueConverter<List<string>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
            );


            var stringListComparer = new ValueComparer<List<string>>(
                 (c1, c2) => (c1 ?? new List<string>()).SequenceEqual(c2 ?? new List<string>()),
                 c => (c ?? new List<string>()).Aggregate(0, (a, v) => HashCode.Combine(a, v != null ? v.GetHashCode() : 0)),
                 c => c == null ? new List<string>() : c.ToList()
             );

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Awareness>(entity =>
            {
                entity.ToTable("Awareness");

                entity.HasKey(e => e.AwarenessID);
                entity.Property(e => e.AwarenessID).HasColumnName("AwarenessID").ValueGeneratedOnAdd();
                entity.Property(e => e.AwarenessImageURL).HasColumnName("AwarenessImageURL").HasColumnType("nvarchar(max)");
                entity.Property(e => e.AwarenessTitle).HasColumnName("AwarenessTitle").HasMaxLength(150);
                entity.Property(e => e.AwarenessContent).HasColumnName("AwarenessContent");
                entity.Property(e => e.CreatedDate).HasColumnName("CreatedDate");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.HasKey(e => e.CartID);
                entity.Property(e => e.CartID).HasColumnName("CartID").ValueGeneratedOnAdd();
                entity.Property(e => e.BuyerUserID).HasColumnName("BuyerUserID");
                entity.Property(e => e.SellerUserID).HasColumnName("SellerUserID");
                entity.Property(e => e.ItemListingID).HasColumnName("ItemListingID");
                entity.Property(e => e.AddedDate).HasColumnName("AddedDate");
                entity.HasOne(e => e.ItemListing)
                      .WithMany()
                      .HasForeignKey(e => e.ItemListingID)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<CollectorLocations>(entity =>
            {
                entity.ToTable("CollectorLocations");

                entity.HasKey(e => e.CollectorUserID);

                entity.Property(e => e.CollectorUserID).HasColumnName("CollectorUserID");
                entity.Property(e => e.CollectorLatitude).HasColumnName("CollectorLatitude");
                entity.Property(e => e.CollectorLongtitude).HasColumnName("CollectorLongtitude");
                entity.Property(e => e.LastUpdated).HasColumnName("LastUpdated");
            });

            modelBuilder.Entity<ItemListing>(entity =>
            {
                entity.ToTable("ItemListing");

                entity.HasKey(e => e.ItemListingID);

                entity.Property(e => e.ItemListingID).HasColumnName("ItemListingID").ValueGeneratedOnAdd();
                entity.Property(e => e.UserID).HasColumnName("UserID");
                entity.Property(e => e.ItemImageURL)
                    .HasColumnName("ItemImageURL")
                    .HasColumnType("nvarchar(max)")
                    .HasConversion(stringListConverter!)
                    .Metadata.SetValueComparer(stringListComparer);
                    
                entity.Property(e => e.ItemName).HasColumnName("ItemName").HasMaxLength(100);
                entity.Property(e => e.ItemDescription).HasColumnName("ItemDescription").HasMaxLength(150);
                entity.Property(e => e.ItemPrice).HasColumnName("ItemPrice");
                entity.Property(e => e.ItemCondition).HasColumnName("ItemCondition");
            });

            modelBuilder.Entity<PickupRequest>(entity =>
            {
                entity.ToTable("PickupRequest");

                entity.HasKey(e => e.PickupRequestID);

                entity.Property(e => e.PickupRequestID).HasColumnName("PickupRequestID");
                entity.Property(e => e.UserID).HasColumnName("UserID");
                entity.Property(e => e.CollectorUserID).HasColumnName("CollectorUserID");
                entity.Property(e => e.PickupLocation).HasColumnName("PickupLocation");
                entity.Property(e => e.PickupLatitude).HasColumnName("PickupLatitude");
                entity.Property(e => e.PickupLongtitude).HasColumnName("PickupLongtitude");
                entity.Property(e => e.Remarks).HasColumnName("Remarks");
                entity.Property(e => e.PickupDate).HasColumnName("PickupDate");
                entity.Property(e => e.PickupTimeRange).HasColumnName("PickupTimeRange");
                entity.Property(e => e.PickupItemImageURL)
                    .HasColumnName("PickupItemImageURL")
                    .HasColumnType("nvarchar(max)")
                    .HasConversion(stringListConverter!)
                    .Metadata.SetValueComparer(stringListComparer);
                entity.Property(e => e.PickupItemDescription).HasColumnName("PickupItemDescription");
                entity.Property(e => e.PickupItemCategory).HasColumnName("PickupItemCategory");
                entity.Property(e => e.PickupItemQuantity).HasColumnName("PickupItemQuantity");
                entity.Property(e => e.PickupItemCondition).HasColumnName("PickupItemCondition");
                entity.Property(e => e.PickupRequestStatus).HasColumnName("PickupRequestStatus");
                entity.Property(e => e.CollectionProofImageURL).HasColumnName("CollectionProofImageURL");
                entity.Property(e => e.RequestedDate).HasColumnName("RequestedDate");
                entity.Property(e => e.CompletedDate).HasColumnName("CompletedDate");
            });

            modelBuilder.Entity<Points>(entity =>
            {
                entity.ToTable("Points");

                entity.HasKey(e => e.PointID);

                entity.Property(e => e.PointID).HasColumnName("PointID").ValueGeneratedOnAdd();
                entity.Property(e => e.UserID).HasColumnName("UserID");
                entity.Property(e => e.Point).HasColumnName("Point");
                entity.Property(e => e.Type).HasColumnName("Type");
                entity.Property(e => e.Description).HasColumnName("Description").HasMaxLength(100);
                entity.Property(e => e.CreatedAt).HasColumnName("CreatedAt");
            });

            modelBuilder.Entity<Purchases>(entity =>
            {
                entity.ToTable("Purchases");

                entity.HasKey(e => e.PurchaseID);

                entity.Property(e => e.PurchaseID).HasColumnName("PurchaseID").ValueGeneratedOnAdd();
                entity.Property(e => e.BuyerUserID).HasColumnName("BuyerUserID");
                entity.Property(e => e.SellerUserID).HasColumnName("SellerUserID");
                entity.Property(e => e.ItemListingID).HasColumnName("ItemListingID");
                entity.Property(e => e.PurchaseGroupID).HasColumnName("PurchaseGroupID");
                entity.Property(e => e.ItemName).HasColumnName("ItemName");
                entity.Property(e => e.ItemPrice).HasColumnName("ItemPrice");
                entity.Property(e => e.ItemCondition).HasColumnName("ItemCondition");
                entity.Property(e => e.ItemCategory).HasColumnName("ItemCategory");
                entity.Property(e => e.ItemImageURL)
                    .HasColumnName("ItemImageURL")
                    .HasColumnType("nvarchar(max)")
                    .HasConversion(stringListConverter!)
                    .Metadata.SetValueComparer(stringListComparer);

                entity.Property(e => e.DeliveryAddress).HasColumnName("DeliveryAddress");
                entity.Property(e => e.IsDelivered).HasColumnName("IsDelivered");
                entity.Property(e => e.Status).HasColumnName("Status");
                entity.Property(e => e.PurchaseDate).HasColumnName("PurchaseDate");
                entity.Property(e => e.DeliveredDate).HasColumnName("DeliveredDate");
            });

            modelBuilder.Entity<Reward>(entity =>
            {
                entity.ToTable("Reward");

                entity.HasKey(e => e.RewardID);

                entity.Property(e => e.RewardID).HasColumnName("RewardID").ValueGeneratedOnAdd();
                entity.Property(e => e.RewardName).HasColumnName("RewardName").HasMaxLength(150);
                entity.Property(e => e.RewardDescription).HasColumnName("RewardDescription").HasMaxLength(200);
                entity.Property(e => e.PointsRequired).HasColumnName("PointsRequired");
                entity.Property(e => e.RewardImageURL).HasColumnName("RewardImageURL").HasColumnType("nvarchar(max)");
                entity.Property(e => e.CreatedDate).HasColumnName("CreatedDate");
                entity.Property(e => e.ExpiryDate).HasColumnName("ExpiryDate");
                entity.Property(e => e.IsActive).HasColumnName("IsActive");
            });

            modelBuilder.Entity<RewardRedemption>(entity =>
            {
                entity.ToTable("RewardRedemption");

                entity.HasKey(e => e.RewardRedemptionID);

                entity.Property(e => e.RewardRedemptionID).HasColumnName("RewardRedemptionID").ValueGeneratedOnAdd();
                entity.Property(e => e.UserID).HasColumnName("UserID");
                entity.Property(e => e.RewardID).HasColumnName("RewardID");
                entity.Property(e => e.IsUsed).HasColumnName("IsUsed");
                entity.Property(e => e.RedeemedDate).HasColumnName("RedeemedDate");
                entity.Property(e => e.RewardName).HasColumnName("RewardName");
                entity.Property(e => e.RewardDescription).HasColumnName("RewardDescription");
                entity.Property(e => e.PointsRequired).HasColumnName("PointsRequired");
                entity.Property(e => e.RewardImageURL).HasColumnName("RewardImageURL");
                entity.Property(e => e.ExpiryDate).HasColumnName("ExpiryDate");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasKey(e => e.UserID);
                entity.Property(e => e.UserID).HasColumnName("UserID");
                entity.Property(e => e.UserRole).HasColumnName("UserRole");
                entity.Property(e => e.FullName).HasColumnName("FullName").HasMaxLength(100);
                entity.Property(e => e.FirstName).HasColumnName("FirstName").HasMaxLength(50);
                entity.Property(e => e.LastName).HasColumnName("LastName").HasMaxLength(50);
                entity.Property(e => e.EmailAddress).HasColumnName("EmailAddress").HasMaxLength(100);
                entity.Property(e => e.Gender).HasColumnName("Gender");
                entity.Property(e => e.PhoneNumber).HasColumnName("PhoneNumber");
                entity.Property(e => e.Password).HasColumnName("Password");
                entity.Property(e => e.Address1).HasColumnName("Address1");
                entity.Property(e => e.Address2).HasColumnName("Address2");
                entity.Property(e => e.City).HasColumnName("City");
                entity.Property(e => e.PostalCode).HasColumnName("PostalCode");
                entity.Property(e => e.State).HasColumnName("State");
                entity.Property(e => e.VehicleType).HasColumnName("VehicleType").HasMaxLength(100);
                entity.Property(e => e.VehiclePlateNumber).HasColumnName("VehiclePlateNumber").HasMaxLength(20);
                entity.Property(e => e.CompanyName).HasColumnName("CompanyName").HasMaxLength(100);
                entity.Property(e => e.ProfileImageURL).HasColumnName("ProfileImageURL").HasColumnType("nvarchar(max)");
                entity.Property(e => e.ApprovalStatus).HasColumnName("ApprovalStatus");
                entity.Property(e => e.AccountRejectMessage).HasColumnName("AccountRejectMessage");
                entity.Property(e => e.CurrentPoint).HasColumnName("CurrentPoint");
                entity.Property(e => e.CreatedDate).HasColumnName("CreatedDate");
            });

            modelBuilder.Entity<FcmToken>(entity =>
            {
                entity.ToTable("FcmToken");

                entity.HasKey(e => e.UserID);

                entity.Property(e => e.UserID).HasColumnName("UserID");
                entity.Property(e => e.Token).HasColumnName("Token");
            });
        }
    }
}
