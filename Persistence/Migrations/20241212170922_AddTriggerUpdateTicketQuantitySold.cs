using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTriggerUpdateTicketQuantitySold : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "029c00f5-5c22-48a1-bbf5-2a17bf6a2279",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61f43f0d-e6b6-45ca-b18a-d89768bc8a9d", "AQAAAAIAAYagAAAAEPPTaEW3D3G+0HE+QG2OenNTOP03GD2VLUeEkWS2BWvhe5P09MsYbTyrEV95LZ1egg==", "d63b534c-2a58-4595-a45b-7f65be32187e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f06d883a-116b-435f-bea7-16880286b741", "AQAAAAIAAYagAAAAEHQ/Fdky5A3tDY89d0+Hy3ZpBNE9Qde1r7ruH+ptq1/909KTP1dOazFuJRrAtxL2OA==", "6054ee56-e485-49f4-8cb5-669887183558" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "029c00f5-5c22-48a1-bbf5-2a17bf6a2279",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ba6f84f-6b85-4361-8f3d-4a5e1641d2ea", "AQAAAAIAAYagAAAAEHBZtu+sH82TdWiw7xLZhwgCz152xWeqZKWPzyrQxpM4pQX5INBAHVs9ZaRf0gTvSg==", "c04b3b6e-f54e-4181-a13b-a570aa1c248a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "08cbfbf0-4faa-485b-9dd0-d71ab1682df5", "AQAAAAIAAYagAAAAEIi0RhzHAW/OxcEf6qmCuZ4vOBdadtzODFYoExcEWFnd+yg5PSe075s8dYrocXj3hQ==", "297cce95-4355-4359-8f50-7ddbee2210d0" });
        }
    }
}
