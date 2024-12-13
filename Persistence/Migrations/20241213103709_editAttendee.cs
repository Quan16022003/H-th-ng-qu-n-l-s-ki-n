using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editAttendee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrivateReferenceNUmber",
                table: "Attendees");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "029c00f5-5c22-48a1-bbf5-2a17bf6a2279",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b3910538-ce05-45bf-a33d-6d3c695c2e32", "AQAAAAIAAYagAAAAEOXgPX5f1+ssaAli/GMcRb8ykBNEYQcJypBii55OjRfSmBOLcc+Rr0G1K/xi4JFn0g==", "c41fdef2-a57a-4e76-ad03-e933e08113de" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7023a0a0-94fe-4842-bc08-959b657bff6e", "AQAAAAIAAYagAAAAECgtIBP+xpLrlj/1KJWngCm2Z+p3tLEbE9pvMf3yXW/rXKtkKZCl1TtXM0iJQEx9/w==", "af5a25c5-ea1b-41c2-bc1f-735f52b068dc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrivateReferenceNUmber",
                table: "Attendees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "029c00f5-5c22-48a1-bbf5-2a17bf6a2279",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a62ffe77-2642-4dcd-985c-4a06b53765f5", "AQAAAAIAAYagAAAAEIXyxsRRHNPV/uaOHzKJXDepEMo5GmS2Tz/JMTAyzTT4mZ+ygc3ZQqBnn0eNrB+tAg==", "53fc703a-66fa-4bc1-a71c-2e69661bf245" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af0bd451-7dab-4fb0-ad94-e2a9799d9ac2", "AQAAAAIAAYagAAAAEPwWU07TwnN2s2rJrhpD+BHorEGRQ3WUGo6/8jjtVNgDm6F3duBsxysHVKwIRcy5Sw==", "48412a03-f8f2-49fb-8596-591ff46dfb74" });
        }
    }
}
