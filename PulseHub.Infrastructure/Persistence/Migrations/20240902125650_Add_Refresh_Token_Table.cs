using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulseHub.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Refresh_Token_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "refresh_token",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_revoked = table.Column<bool>(type: "bit", nullable: false),
                    is_used = table.Column<bool>(type: "bit", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_at_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    deleted_at_utc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_refresh_token", x => x.id);
                    table.ForeignKey(
                        name: "fk_refresh_token_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "ix_refresh_token_user_id",
                table: "refresh_token",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "refresh_token");

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
    }
}
