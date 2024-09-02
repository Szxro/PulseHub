using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulseHub.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Expiration_date_column_Refresh_Token_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "expiration_date",
                table: "refresh_token",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "provider",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at_utc", "modified_at_utc" },
                values: new object[] { new DateTime(2024, 9, 2, 13, 18, 33, 498, DateTimeKind.Utc).AddTicks(5102), new DateTime(2024, 9, 2, 13, 18, 33, 498, DateTimeKind.Utc).AddTicks(5104) });

            migrationBuilder.UpdateData(
                table: "provider",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at_utc", "modified_at_utc" },
                values: new object[] { new DateTime(2024, 9, 2, 13, 18, 33, 498, DateTimeKind.Utc).AddTicks(5106), new DateTime(2024, 9, 2, 13, 18, 33, 498, DateTimeKind.Utc).AddTicks(5106) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expiration_date",
                table: "refresh_token");

            migrationBuilder.UpdateData(
                table: "provider",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at_utc", "modified_at_utc" },
                values: new object[] { new DateTime(2024, 9, 2, 12, 56, 49, 865, DateTimeKind.Utc).AddTicks(9943), new DateTime(2024, 9, 2, 12, 56, 49, 865, DateTimeKind.Utc).AddTicks(9945) });

            migrationBuilder.UpdateData(
                table: "provider",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at_utc", "modified_at_utc" },
                values: new object[] { new DateTime(2024, 9, 2, 12, 56, 49, 865, DateTimeKind.Utc).AddTicks(9947), new DateTime(2024, 9, 2, 12, 56, 49, 865, DateTimeKind.Utc).AddTicks(9948) });
        }
    }
}
