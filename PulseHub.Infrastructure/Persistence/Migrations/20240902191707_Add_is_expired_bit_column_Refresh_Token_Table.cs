using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulseHub.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_is_expired_bit_column_Refresh_Token_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_expired",
                table: "refresh_token",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "provider",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at_utc", "modified_at_utc" },
                values: new object[] { new DateTime(2024, 9, 2, 19, 17, 7, 271, DateTimeKind.Utc).AddTicks(5963), new DateTime(2024, 9, 2, 19, 17, 7, 271, DateTimeKind.Utc).AddTicks(5965) });

            migrationBuilder.UpdateData(
                table: "provider",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at_utc", "modified_at_utc" },
                values: new object[] { new DateTime(2024, 9, 2, 19, 17, 7, 271, DateTimeKind.Utc).AddTicks(5967), new DateTime(2024, 9, 2, 19, 17, 7, 271, DateTimeKind.Utc).AddTicks(5968) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_expired",
                table: "refresh_token");

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
    }
}
