using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulseHub.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Remove_User_Credentials_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_credentials");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified_at_utc",
                table: "user",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deleted_at_utc",
                table: "user",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "user",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AddColumn<bool>(
                name: "is_email_verified",
                table: "user",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified_at_utc",
                table: "provider",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deleted_at_utc",
                table: "provider",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "provider",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified_at_utc",
                table: "credentials",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deleted_at_utc",
                table: "credentials",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "credentials",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "credentials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "version",
                table: "credentials",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified_at_utc",
                table: "application",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deleted_at_utc",
                table: "application",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "application",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified_at_utc",
                table: "access_key",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deleted_at_utc",
                table: "access_key",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                table: "access_key",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.UpdateData(
                table: "provider",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at_utc", "deleted_at_utc", "modified_at_utc" },
                values: new object[] { new DateTime(2024, 8, 27, 18, 14, 57, 434, DateTimeKind.Utc).AddTicks(8774), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 27, 18, 14, 57, 434, DateTimeKind.Utc).AddTicks(8776) });

            migrationBuilder.UpdateData(
                table: "provider",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at_utc", "deleted_at_utc", "modified_at_utc" },
                values: new object[] { new DateTime(2024, 8, 27, 18, 14, 57, 434, DateTimeKind.Utc).AddTicks(8779), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 27, 18, 14, 57, 434, DateTimeKind.Utc).AddTicks(8779) });

            migrationBuilder.CreateIndex(
                name: "ix_credentials_user_id",
                table: "credentials",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_credentials_user_user_id",
                table: "credentials",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_credentials_user_user_id",
                table: "credentials");

            migrationBuilder.DropIndex(
                name: "ix_credentials_user_id",
                table: "credentials");

            migrationBuilder.DropColumn(
                name: "is_email_verified",
                table: "user");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "credentials");

            migrationBuilder.DropColumn(
                name: "version",
                table: "credentials");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "modified_at_utc",
                table: "user",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "deleted_at_utc",
                table: "user",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at_utc",
                table: "user",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "modified_at_utc",
                table: "provider",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "deleted_at_utc",
                table: "provider",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at_utc",
                table: "provider",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "modified_at_utc",
                table: "credentials",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "deleted_at_utc",
                table: "credentials",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at_utc",
                table: "credentials",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "modified_at_utc",
                table: "application",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "deleted_at_utc",
                table: "application",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at_utc",
                table: "application",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "modified_at_utc",
                table: "access_key",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "deleted_at_utc",
                table: "access_key",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at_utc",
                table: "access_key",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "user_credentials",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    credentials_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    created_at_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modified_at_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_credentials", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_credentials_credentials_credentials_id",
                        column: x => x.credentials_id,
                        principalTable: "credentials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_credentials_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "provider",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at_utc", "deleted_at_utc", "modified_at_utc" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 8, 26, 12, 46, 39, 768, DateTimeKind.Unspecified).AddTicks(7410), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1999, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -4, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 26, 12, 46, 39, 768, DateTimeKind.Unspecified).AddTicks(7411), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "provider",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at_utc", "deleted_at_utc", "modified_at_utc" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 8, 26, 12, 46, 39, 768, DateTimeKind.Unspecified).AddTicks(7414), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1999, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -4, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 26, 12, 46, 39, 768, DateTimeKind.Unspecified).AddTicks(7415), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "ix_user_credentials_credentials_id",
                table: "user_credentials",
                column: "credentials_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_credentials_user_id",
                table: "user_credentials",
                column: "user_id");
        }
    }
}
