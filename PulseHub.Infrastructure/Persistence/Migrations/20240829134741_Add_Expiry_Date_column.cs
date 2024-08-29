using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulseHub.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Expiry_Date_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "expiry_date",
                table: "email_code",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "provider",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at_utc", "modified_at_utc" },
                values: new object[] { new DateTime(2024, 8, 29, 13, 47, 40, 691, DateTimeKind.Utc).AddTicks(5763), new DateTime(2024, 8, 29, 13, 47, 40, 691, DateTimeKind.Utc).AddTicks(5765) });

            migrationBuilder.UpdateData(
                table: "provider",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at_utc", "modified_at_utc" },
                values: new object[] { new DateTime(2024, 8, 29, 13, 47, 40, 691, DateTimeKind.Utc).AddTicks(5768), new DateTime(2024, 8, 29, 13, 47, 40, 691, DateTimeKind.Utc).AddTicks(5768) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expiry_date",
                table: "email_code");

            migrationBuilder.UpdateData(
                table: "provider",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at_utc", "modified_at_utc" },
                values: new object[] { new DateTime(2024, 8, 28, 12, 26, 6, 377, DateTimeKind.Utc).AddTicks(3421), new DateTime(2024, 8, 28, 12, 26, 6, 377, DateTimeKind.Utc).AddTicks(3424) });

            migrationBuilder.UpdateData(
                table: "provider",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at_utc", "modified_at_utc" },
                values: new object[] { new DateTime(2024, 8, 28, 12, 26, 6, 377, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 8, 28, 12, 26, 6, 377, DateTimeKind.Utc).AddTicks(3427) });
        }
    }
}
