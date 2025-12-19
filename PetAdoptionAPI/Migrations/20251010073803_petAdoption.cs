using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAdoptionAPI.Migrations
{
    /// <inheritdoc />
    public partial class petAdoption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardPayments",
                columns: table => new
                {
                    CardPaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardHolderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    ExpirationDate = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CVV = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    SaveCard = table.Column<bool>(type: "bit", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPayments", x => x.CardPaymentId);
                });

            migrationBuilder.CreateTable(
                name: "PetCategoryMasters",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetCategoryMasters", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "RoleMasters",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMasters", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "WhatsAppPayments",
                columns: table => new
                {
                    WhatsAppPaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserPhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    WhatsAppTransactionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhatsAppPayments", x => x.WhatsAppPaymentId);
                });

            migrationBuilder.CreateTable(
                name: "PetMasters",
                columns: table => new
                {
                    PetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PetImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    PetCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetMasters", x => x.PetId);
                    table.ForeignKey(
                        name: "FK_PetMasters_PetCategoryMasters_PetCategoryId",
                        column: x => x.PetCategoryId,
                        principalTable: "PetCategoryMasters",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMasters",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMasters", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserMasters_RoleMasters_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleMasters",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    CardPaymentId = table.Column<int>(type: "int", nullable: true),
                    WhatsAppPaymentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_CardPayments_CardPaymentId",
                        column: x => x.CardPaymentId,
                        principalTable: "CardPayments",
                        principalColumn: "CardPaymentId");
                    table.ForeignKey(
                        name: "FK_Payments_WhatsAppPayments_WhatsAppPaymentId",
                        column: x => x.WhatsAppPaymentId,
                        principalTable: "WhatsAppPayments",
                        principalColumn: "WhatsAppPaymentId");
                });

            migrationBuilder.CreateTable(
                name: "PetBehaviorProfiles",
                columns: table => new
                {
                    BehaviorProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Behavior = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Training = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetBehaviorProfiles", x => x.BehaviorProfileId);
                    table.ForeignKey(
                        name: "FK_PetBehaviorProfiles_PetMasters_PetId",
                        column: x => x.PetId,
                        principalTable: "PetMasters",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PetMedicalRecords",
                columns: table => new
                {
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaccinationDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetMedicalRecords", x => x.MedicalRecordId);
                    table.ForeignKey(
                        name: "FK_PetMedicalRecords_PetMasters_PetId",
                        column: x => x.PetId,
                        principalTable: "PetMasters",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdoptionMasters",
                columns: table => new
                {
                    AdoptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    AdoptionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinalStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AdopterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionMasters", x => x.AdoptionId);
                    table.ForeignKey(
                        name: "FK_AdoptionMasters_UserMasters_AdopterId",
                        column: x => x.AdopterId,
                        principalTable: "UserMasters",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdoptionRequests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionRequests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_AdoptionRequests_PetMasters_PetId",
                        column: x => x.PetId,
                        principalTable: "PetMasters",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdoptionRequests_UserMasters_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMasters",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    DonationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.DonationId);
                    table.ForeignKey(
                        name: "FK_Donations_UserMasters_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMasters",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackMasters",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackMasters", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_FeedbackMasters_UserMasters_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMasters",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationMasters",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationMasters", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_NotificationMasters_UserMasters_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMasters",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionMasters_AdopterId",
                table: "AdoptionMasters",
                column: "AdopterId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionRequests_PetId",
                table: "AdoptionRequests",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionRequests_UserId",
                table: "AdoptionRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_UserId",
                table: "Donations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackMasters_UserId",
                table: "FeedbackMasters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationMasters_UserId",
                table: "NotificationMasters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CardPaymentId",
                table: "Payments",
                column: "CardPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_WhatsAppPaymentId",
                table: "Payments",
                column: "WhatsAppPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PetBehaviorProfiles_PetId",
                table: "PetBehaviorProfiles",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_PetMasters_PetCategoryId",
                table: "PetMasters",
                column: "PetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PetMedicalRecords_PetId",
                table: "PetMedicalRecords",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMasters_RoleId",
                table: "UserMasters",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdoptionMasters");

            migrationBuilder.DropTable(
                name: "AdoptionRequests");

            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "FeedbackMasters");

            migrationBuilder.DropTable(
                name: "NotificationMasters");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PetBehaviorProfiles");

            migrationBuilder.DropTable(
                name: "PetMedicalRecords");

            migrationBuilder.DropTable(
                name: "UserMasters");

            migrationBuilder.DropTable(
                name: "CardPayments");

            migrationBuilder.DropTable(
                name: "WhatsAppPayments");

            migrationBuilder.DropTable(
                name: "PetMasters");

            migrationBuilder.DropTable(
                name: "RoleMasters");

            migrationBuilder.DropTable(
                name: "PetCategoryMasters");
        }
    }
}
