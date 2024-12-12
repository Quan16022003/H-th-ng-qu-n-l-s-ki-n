using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class configTicketAndOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_TicketId",
                table: "OrderItems",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Tickets_TicketId",
                table: "OrderItems",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Tickets_TicketId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_TicketId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "OrderItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "029c00f5-5c22-48a1-bbf5-2a17bf6a2279",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10371730-b4be-4e4b-bdf2-41d1625282cc", "AQAAAAIAAYagAAAAEFOxjmDE7MTPV1BtWpYSSeFkh9BKqRLUMGq1Pkta/zGQXuMctviwaLU1MwHeXo2gqA==", "23be56f3-b396-4be8-86a2-9731fe7f3218" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bbb9d7a-bd70-49f1-bb11-a2cd651a34de", "AQAAAAIAAYagAAAAEAiYnkFfQBPdWh6qoFm7zTBSlF+ZqirtFd9n4pjsK6KoQgDiBDOPtCOY1xH4wesX+g==", "988b32d0-4fec-4e8d-a1dc-3e1f2896502e" });
        }
    }
}
