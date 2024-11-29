using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ebae836-eaaa-41b2-9209-b7a77f2c7087", null, "Customer", "CUSTOMER" },
                    { "5045af52-8d12-4add-9d03-8deeb4500523", null, "Organizer", "ORGANIZER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "0407d405-bd4a-44aa-a33d-11c50b02e8e7", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAELIir28n/Pjd95Czjo1iDye5Kk3oJK2Wo7GBUUIckakZc384uB29rqUUITMDigTJ1A==", "d1775efc-c8aa-4c32-8922-ae5f2c0855d1", "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "029c00f5-5c22-48a1-bbf5-2a17bf6a2279", 0, "89b0ac17-ded4-4103-97bc-e6f308e8dc2d", null, false, false, null, null, "ORGANIZER@ADMIN.COM", "AQAAAAIAAYagAAAAEDKqRz/0cfMk7vWeyKALp33biH2qyH1uWua184tsLWh7f1sCGLR6UFfOLSCgAVd3Cg==", null, false, "782a4d01-2032-467a-b37e-71b5d6632cc2", false, "organizer@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5045af52-8d12-4add-9d03-8deeb4500523", "029c00f5-5c22-48a1-bbf5-2a17bf6a2279" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ebae836-eaaa-41b2-9209-b7a77f2c7087");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5045af52-8d12-4add-9d03-8deeb4500523", "029c00f5-5c22-48a1-bbf5-2a17bf6a2279" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5045af52-8d12-4add-9d03-8deeb4500523");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "029c00f5-5c22-48a1-bbf5-2a17bf6a2279");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "75554cea-8fa5-4a30-bb87-e0dd3219577b", "MYUSER", "AQAAAAIAAYagAAAAEOjk05oocCSP0YGOVnjR0/N0l+4x9priAvRJe1+f5wrJlVmmloroHHYXcx8d0BqoQQ==", "67a6c028-a5b9-453c-afc0-d041a08382a3", "myuser" });
        }
    }
}
