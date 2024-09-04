using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PulseHub.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Unique_Index_Application_Access_Key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "provider",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "provider",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "application",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "encrypted_key",
                table: "access_key",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "ix_application_name",
                table: "application",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_access_key_encrypted_key",
                table: "access_key",
                column: "encrypted_key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_application_name",
                table: "application");

            migrationBuilder.DropIndex(
                name: "ix_access_key_encrypted_key",
                table: "access_key");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "application",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "encrypted_key",
                table: "access_key",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "provider",
                columns: new[] { "id", "created_at_utc", "deleted_at_utc", "description", "is_deleted", "modified_at_utc", "name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 2, 19, 17, 7, 271, DateTimeKind.Utc).AddTicks(5963), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Discord is a platform for text, voice, and video chat, designed for creating and managing communities and staying connected.", false, new DateTime(2024, 9, 2, 19, 17, 7, 271, DateTimeKind.Utc).AddTicks(5965), "Discord" },
                    { 2, new DateTime(2024, 9, 2, 19, 17, 7, 271, DateTimeKind.Utc).AddTicks(5967), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Telegram is a messaging app that offers fast, secure text, voice, and video communication. It supports group chats, channels, and multimedia sharing.", false, new DateTime(2024, 9, 2, 19, 17, 7, 271, DateTimeKind.Utc).AddTicks(5968), "Telegram" }
                });
        }
    }
}
