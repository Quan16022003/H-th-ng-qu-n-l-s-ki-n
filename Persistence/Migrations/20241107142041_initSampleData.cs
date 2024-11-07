using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", null, "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "613bd9ae-86e8-482f-ac5e-bdce6d164ec6", null, false, false, null, null, "MYUSER", "AQAAAAIAAYagAAAAELIAZczSRA63xUjoU9Kul1jPfPx9yF+7cgBO3WWn21PAjxDmIGJohf7i/M0HWipwXg==", null, false, "a256967b-eaec-493a-a004-921fcd56791e", false, "myuser" });

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
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.DeleteData(
                table: "CategoryEvents",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CategoryEvents",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CategoryEvents",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CategoryEvents",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CategoryEvents",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
