using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrganizerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: true),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    VenueName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Ward = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(10,8)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(11,8)", nullable: true),
                    PlaceId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    CoverUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_AspNetUsers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_CategoryEvents_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    MaxPerPerson = table.Column<int>(type: "int", nullable: false),
                    QuantityAvailable = table.Column<int>(type: "int", nullable: false),
                    QuantitySold = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attendees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    HasArrived = table.Column<bool>(type: "bit", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendees_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendees_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendees_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attendees_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderTickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderTickets_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderTickets_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c5e174e-3b0e-446f-86af-483d56fd7210", null, "Administrator", "ADMINISTRATOR" },
                    { "4ebae836-eaaa-41b2-9209-b7a77f2c7087", null, "Customer", "CUSTOMER" },
                    { "5045af52-8d12-4add-9d03-8deeb4500523", null, "Organizer", "ORGANIZER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarUrl", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "029c00f5-5c22-48a1-bbf5-2a17bf6a2279", 0, null, "2afa91fd-e517-4d36-9f5d-6bcac4f657f3", null, false, null, null, false, null, null, "ORGANIZER@ADMIN.COM", "AQAAAAIAAYagAAAAEHTimvdgc2t+OjaCrZJbX0migDxgnOun4taYm6xNCXjoJZzuH+iqIqYGIPtWWbrWcQ==", null, false, "9816d483-7720-44f7-b1b8-01ae19476429", false, "organizer@admin.com" },
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, null, "ec9be6b8-ec3c-438e-ac5b-095a6b58366e", null, false, null, null, false, null, null, "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAELK4dGKGA7+V90vQfo/oSaQETPlaMbYOPRcgSZ0LChVra2XlqGe7dfiP103/2/Vfpg==", null, false, "5f5d5c01-cbc9-447a-a0f0-809be9b88c1c", false, "admin@admin.com" }
                });

            migrationBuilder.InsertData(
                table: "CategoryEvents",
                columns: new[] { "Id", "Description", "Name", "Slug", "Status", "ThumbnailUrl" },
                values: new object[,]
                {
                    { 1, "abc", "Business & Seminars", "business-&-seminars", true, "categories/business&seminars.webp" },
                    { 2, "abc", "Yoga & Health", "yoga-&-health", true, "categories/yoga&health.webp" },
                    { 3, "abc", "Sports & Fitness", "sports-&-fitness", true, "categories/sports&fitness.webp" },
                    { 4, "abc", "Music & Concerts", "music-&-concerts", true, "/categories/music&concerts.webp" },
                    { 5, "abcd", "Science & Tech", "science-&-tech", true, "categories/science&tech.webp" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "5045af52-8d12-4add-9d03-8deeb4500523", "029c00f5-5c22-48a1-bbf5-2a17bf6a2279" },
                    { "2c5e174e-3b0e-446f-86af-483d56fd7210", "8e445865-a24d-4543-a6c6-9443d048cdb9" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Address", "CategoryId", "City", "CoverUrl", "Description", "District", "EndDate", "EndTime", "IsPublic", "Latitude", "Longitude", "OrganizerId", "PhoneNumber", "PlaceId", "PostalCode", "Slug", "StartDate", "StartTime", "ThumbnailUrl", "Title", "VenueName", "Ward", "WebsiteUrl" },
                values: new object[,]
                {
                    { 1, "Đường 3/4", 4, "Tp. Đà Lạt", "/images/events/hanhitoichoem.png", "Liveshow Hà Nhi | TỘI CHO EM\nLululola Show - Hơn cả âm nhạc, không gian lãng mạn đậm chất thơ Đà Lạt bao trọn hình ảnh thung lũng Đà Lạt, được ngắm nhìn khoảng khắc hoàng hôn thơ mộng đến khi Đà Lạt về đêm siêu lãng mạn, được giao lưu với thần tượng một cách chân thật và gần gũi nhất trong không gian ấm áp và không khí se lạnh của Đà Lạt. Tất cả sẽ  mang đến một đêm nhạc ấn tượng mà bạn không thể quên khi đến với Đà Lạt.", "", new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 19, 30, 0, 0), true, 11.919983199999999m, 108.4421036m, "8e445865-a24d-4543-a6c6-9443d048cdb9", "", null, "", "lululola-show-ha-nhi-toi-cho-em", new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 17, 30, 0, 0), "/images/events/hanhitoichoem.png", "LULULOLA SHOW HÀ NHI | TỘI CHO EM", "lululola coffee", "Phường 3", "" },
                    { 2, "456 Đường Yên Bái, Đà Nẵng", 2, "Đà Nẵng", "/images/events/yoga-for-beginners-cover.jpg", "A gentle introduction to yoga.", "Hải Châu", new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), true, 16.0678m, 108.2208m, "8e445865-a24d-4543-a6c6-9443d048cdb9", "0987654321", null, "550000", "yoga-for-beginners", new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 0, 0, 0), "/images/events/yoga-for-beginners.jpg", "Yoga for Beginners", "Zen Yoga Studio", "", "https://example.com/yoga-for-beginners" },
                    { 3, "789 Đường Lê Duẩn, Hồ Chí Minh", 3, "Hồ Chí Minh", "/images/events/marathon-training-cover.jpg", "Get ready for your next marathon with our expert trainers.", "Quận 1", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), true, 10.7769m, 106.6955m, "8e445865-a24d-4543-a6c6-9443d048cdb9", "0123456789", null, "700000", "marathon-training", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0), "/images/events/marathon-training.jpg", "Marathon Training Program", "City Sports Complex", "", "https://example.com/marathon-training" },
                    { 4, "101 Đường Nguyễn Thái Học, Hà Nội", 4, "Hà Nội", "/images/events/live-music-concert-cover.jpg", "Enjoy live music from up-and-coming artists.", "Đống Đa", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 22, 0, 0, 0), true, 21.0202m, 105.7692m, "8e445865-a24d-4543-a6c6-9443d048cdb9", "0123456789", null, "100000", "live-music-concert", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 19, 0, 0, 0), "/images/events/live-music-concert.jpg", "Live Music Concert", "Main City Park", "", "https://example.com/live-music-concert" },
                    { 5, "456 Đường Nguyễn Thị Minh Khai, TP. Hồ Chí Minh", 5, "TP. Hồ Chí Minh", "/images/events/tech-innovation-summit-cover.jpg", "Explore the latest innovations in technology.", "Quận 1", new DateTime(2024, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 17, 0, 0, 0), true, 10.7769m, 106.6955m, "8e445865-a24d-4543-a6c6-9443d048cdb9", "0123456789", null, "700000", "tech-innovation-summit", new DateTime(2024, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), "/images/events/tech-innovation-summit.jpg", "Tech Innovation Summit", "Tech Convention Center", "", "https://example.com/tech-innovation-summit" },
                    { 6, "123 Đường Tràng Tiền, Hà Nội", 1, "Hà Nội", "/images/events/business-leadership-forum-cover.jpg", "A forum for business leaders to share insights.", "Hoàn Kiếm", new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 15, 0, 0, 0), true, 21.0285m, 105.8040m, "8e445865-a24d-4543-a6c6-9443d048cdb9", "0123456789", null, "100000", "business-leadership-forum", new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0), "/images/events/business-leadership-forum.jpg", "Business Leadership Forum", "Royal Business Hotel", "", "https://example.com/business-leadership-forum" },
                    { 7, "456 Đường Trần Phú, Nha Trang", 2, "Nha Trang", "/images/events/wellness-weekend-cover.jpg", "A weekend focused on wellness and relaxation.", "", new DateTime(2024, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 16, 0, 0, 0), true, 12.2385m, 109.1967m, "8e445865-a24d-4543-a6c6-9443d048cdb9", "", null, "", "wellness-weekend", new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 0, 0, 0), "/images/events/wellness-weekend.jpg", "Wellness Weekend Getaway", "Serenity Spa Resort", "", "" },
                    { 8, "789 Đường Lê Duẩn, Hồ Chí Minh", 3, "Hồ Chí Minh", "/images/events/sports-competition-cover.jpg", "Compete with other athletes in various sports.", "", new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 17, 0, 0, 0), true, 10.7769m, 106.6955m, "8e445865-a24d-4543-a6c6-9443d048cdb9", "", null, "", "sports-competition", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0), "/images/events/sports-competition.jpg", "City Sports Competition", "City Stadium", "", "" },
                    { 9, "101 Đường Hoàng Sa, Đà Nẵng", 4, "Đà Nẵng", "/images/events/music-performance-cover.jpg", "An evening of beautiful music performances.", "", new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 21, 0, 0, 0), true, 16.0678m, 108.2208m, "8e445865-a24d-4543-a6c6-9443d048cdb9", "", null, "", "music-performance", new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 18, 0, 0, 0), "/images/events/music-performance.jpg", "Evening Music Performance", "Musical Theatre", "", "" },
                    { 10, "456 Đường Trần Quốc Toản, Hà Nội", 5, "Hà Nội", "/images/events/science-discovery-day-cover.jpg", "A day of science experiments and discoveries.", "", new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 16, 0, 0, 0), true, 21.0285m, 105.8040m, "8e445865-a24d-4543-a6c6-9443d048cdb9", "", null, "", "science-discovery-day", new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), "/images/events/science-discovery-day.jpg", "Science Discovery Day", "Science Museum", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Description", "EventId", "MaxPerPerson", "Price", "QuantityAvailable", "QuantitySold", "Status", "Title" },
                values: new object[,]
                {
                    { 1, "Trải nghiệm cao cấp với khu vực VIP và nhiều đặc quyền.", 1, 2, 1000000, 100, 0, 1, "Vé vàng" },
                    { 2, "Trải nghiệm phổ thông nhưng đầy đủ tiện nghi.", 1, 4, 500000, 200, 0, 1, "Vé bạc" },
                    { 3, "Trải nghiệm cơ bản, phù hợp với ngân sách thấp.", 1, 5, 300000, 300, 0, 1, "Vé Đồng" },
                    { 4, "Giá ưu đãi cho người đặt vé sớm.", 1, 1, 2000000, 50, 0, 1, "Vé Early Bird" },
                    { 5, "Vé tham dự sự kiện Yoga for Beginners", 2, 2, 200000, 50, 0, 1, "Vé thường" },
                    { 6, "Vé VIP tham dự sự kiện Yoga for Beginners", 2, 1, 300000, 20, 0, 1, "Vé VIP" },
                    { 7, "Vé tham dự Marathon Training Program", 3, 1, 100000, 100, 0, 1, "Vé chạy bộ" },
                    { 8, "Vé VIP tham dự Marathon Training Program", 3, 1, 200000, 50, 0, 1, "Vé VIP chạy bộ" },
                    { 9, "Vé nhóm tham dự Marathon Training Program (cho 3 người)", 3, 3, 250000, 30, 0, 1, "Vé nhóm chạy bộ" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_EventId",
                table: "Attendees",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_OrderId",
                table: "Attendees",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_TicketId",
                table: "Attendees",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_UserId",
                table: "Attendees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerId",
                table: "Events",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_TicketId",
                table: "OrderItems",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EventId",
                table: "Orders",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTickets_OrderId",
                table: "OrderTickets",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTickets_TicketId",
                table: "OrderTickets",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EventId",
                table: "Tickets",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Attendees");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "OrderTickets");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CategoryEvents");
        }
    }
}
