using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenCycleFypAPI.Migrations
{
    /// <inheritdoc />
    public partial class GreenCycleAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Awareness",
                columns: table => new
                {
                    AwarenessID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AwarenessImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwarenessTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    AwarenessContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Awareness", x => x.AwarenessID);
                });

            migrationBuilder.CreateTable(
                name: "CollectorLocations",
                columns: table => new
                {
                    CollectorUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CollectorLatitude = table.Column<double>(type: "float", nullable: true),
                    CollectorLongtitude = table.Column<double>(type: "float", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectorLocations", x => x.CollectorUserID);
                });

            migrationBuilder.CreateTable(
                name: "FcmToken",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FcmToken", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "ItemListing",
                columns: table => new
                {
                    ItemListingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ItemDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ItemPrice = table.Column<double>(type: "float", nullable: true),
                    ItemCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSold = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemListing", x => x.ItemListingID);
                });

            migrationBuilder.CreateTable(
                name: "PickupRequest",
                columns: table => new
                {
                    PickupRequestID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectorUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupLatitude = table.Column<double>(type: "float", nullable: true),
                    PickupLongtitude = table.Column<double>(type: "float", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PickupTimeRange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupItemImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupItemCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupItemQuantity = table.Column<int>(type: "int", nullable: true),
                    PickupItemCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupRequestStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectionProofImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupRequest", x => x.PickupRequestID);
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    PointID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Point = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.PointID);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    PurchaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemListingID = table.Column<int>(type: "int", nullable: true),
                    PurchaseGroupID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemPrice = table.Column<double>(type: "float", nullable: true),
                    ItemCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveredDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.PurchaseID);
                });

            migrationBuilder.CreateTable(
                name: "Reward",
                columns: table => new
                {
                    RewardID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RewardName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    RewardDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PointsRequired = table.Column<int>(type: "int", nullable: true),
                    RewardImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reward", x => x.RewardID);
                });

            migrationBuilder.CreateTable(
                name: "RewardRedemption",
                columns: table => new
                {
                    RewardRedemptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RewardID = table.Column<int>(type: "int", nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: true),
                    RedeemedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RewardName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RewardDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PointsRequired = table.Column<int>(type: "int", nullable: true),
                    RewardImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardRedemption", x => x.RewardRedemptionID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VehiclePlateNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProfileImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountRejectMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentPoint = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemListingID = table.Column<int>(type: "int", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.CartID);
                    table.ForeignKey(
                        name: "FK_Cart_ItemListing_ItemListingID",
                        column: x => x.ItemListingID,
                        principalTable: "ItemListing",
                        principalColumn: "ItemListingID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ItemListingID",
                table: "Cart",
                column: "ItemListingID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Awareness");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "CollectorLocations");

            migrationBuilder.DropTable(
                name: "FcmToken");

            migrationBuilder.DropTable(
                name: "PickupRequest");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Reward");

            migrationBuilder.DropTable(
                name: "RewardRedemption");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ItemListing");
        }
    }
}
