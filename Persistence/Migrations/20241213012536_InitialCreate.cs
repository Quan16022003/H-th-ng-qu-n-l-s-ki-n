using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "029c00f5-5c22-48a1-bbf5-2a17bf6a2279",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f9a91a8-dc55-42b5-8063-eb51f2625d4e", "AQAAAAIAAYagAAAAELNeHjYgioeBPgKA3PL/zJTa4o8qjqH9KWmxr8KQrlDTLaE6MKqtsCQlQnvhePRBmw==", "f3ddb439-9406-4cd9-80c4-986d6c283505" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26b23ddf-3239-4350-a2ed-ba826b5e548e", "AQAAAAIAAYagAAAAEAZxqqpw4z4DORj1WQRG74bTfpEQaAoNTKdZJqE5AKtKZ+dRe5PCYt/jOM4a3c4NYw==", "d41cbeb5-32f2-4f9b-8c8b-a1d9c4211572" });
        }
    }
}
