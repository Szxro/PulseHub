using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulseHub.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Email_code_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_email_verified",
                table: "user");

            migrationBuilder.CreateTable(
                name: "email_code",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_invalid = table.Column<bool>(type: "bit", nullable: false),
                    is_verified = table.Column<bool>(type: "bit", nullable: false),
                    is_expired = table.Column<bool>(type: "bit", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_at_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    deleted_at_utc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_email_code", x => x.id);
                    table.ForeignKey(
                        name: "fk_email_code_user_user_id",
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
                values: new object[] { new DateTime(2024, 8, 28, 12, 26, 6, 377, DateTimeKind.Utc).AddTicks(3421), new DateTime(2024, 8, 28, 12, 26, 6, 377, DateTimeKind.Utc).AddTicks(3424) });

            migrationBuilder.UpdateData(
                table: "provider",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at_utc", "modified_at_utc" },
                values: new object[] { new DateTime(2024, 8, 28, 12, 26, 6, 377, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 8, 28, 12, 26, 6, 377, DateTimeKind.Utc).AddTicks(3427) });

            migrationBuilder.CreateIndex(
                name: "ix_email_code_user_id",
                table: "email_code",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "email_code");

            migrationBuilder.AddColumn<bool>(
                name: "is_email_verified",
                table: "user",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "provider",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at_utc", "modified_at_utc" },
                values: new object[] { new DateTime(2024, 8, 27, 18, 14, 57, 434, DateTimeKind.Utc).AddTicks(8774), new DateTime(2024, 8, 27, 18, 14, 57, 434, DateTimeKind.Utc).AddTicks(8776) });

            migrationBuilder.UpdateData(
                table: "provider",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at_utc", "modified_at_utc" },
                values: new object[] { new DateTime(2024, 8, 27, 18, 14, 57, 434, DateTimeKind.Utc).AddTicks(8779), new DateTime(2024, 8, 27, 18, 14, 57, 434, DateTimeKind.Utc).AddTicks(8779) });
        }
    }
}
