using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppUserProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "029c00f5-5c22-48a1-bbf5-2a17bf6a2279",
                columns: new[] { "AvatarUrl", "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "10371730-b4be-4e4b-bdf2-41d1625282cc", null, null, "AQAAAAIAAYagAAAAEFOxjmDE7MTPV1BtWpYSSeFkh9BKqRLUMGq1Pkta/zGQXuMctviwaLU1MwHeXo2gqA==", "23be56f3-b396-4be8-86a2-9731fe7f3218" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "AvatarUrl", "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "2bbb9d7a-bd70-49f1-bb11-a2cd651a34de", null, null, "AQAAAAIAAYagAAAAEAiYnkFfQBPdWh6qoFm7zTBSlF+ZqirtFd9n4pjsK6KoQgDiBDOPtCOY1xH4wesX+g==", "988b32d0-4fec-4e8d-a1dc-3e1f2896502e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "029c00f5-5c22-48a1-bbf5-2a17bf6a2279",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89b0ac17-ded4-4103-97bc-e6f308e8dc2d", "AQAAAAIAAYagAAAAEDKqRz/0cfMk7vWeyKALp33biH2qyH1uWua184tsLWh7f1sCGLR6UFfOLSCgAVd3Cg==", "782a4d01-2032-467a-b37e-71b5d6632cc2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0407d405-bd4a-44aa-a33d-11c50b02e8e7", "AQAAAAIAAYagAAAAELIir28n/Pjd95Czjo1iDye5Kk3oJK2Wo7GBUUIckakZc384uB29rqUUITMDigTJ1A==", "d1775efc-c8aa-4c32-8922-ae5f2c0855d1" });
        }
    }
}
