using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PulseHub.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "credentials",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hash_value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    salt_value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modified_at_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    deleted_at_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_credentials", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "provider",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modified_at_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    deleted_at_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_provider", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    lock_out_enable = table.Column<bool>(type: "bit", nullable: false),
                    lock_out_end = table.Column<DateTime>(type: "datetime2", nullable: false),
                    access_failed_count = table.Column<int>(type: "int", nullable: false),
                    created_at_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modified_at_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    deleted_at_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "application",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    provider_application_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    provider_id = table.Column<int>(type: "int", nullable: false),
                    created_at_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modified_at_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    deleted_at_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application", x => x.id);
                    table.ForeignKey(
                        name: "fk_application_provider_provider_id",
                        column: x => x.provider_id,
                        principalTable: "provider",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_application_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_credentials",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    credentials_id = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "access_key",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    encrypted_key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    application_id = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    last_used = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    created_at_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modified_at_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    deleted_at_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_access_key", x => x.id);
                    table.ForeignKey(
                        name: "fk_access_key_application_application_id",
                        column: x => x.application_id,
                        principalTable: "application",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "provider",
                columns: new[] { "id", "created_at_utc", "deleted_at_utc", "description", "is_deleted", "modified_at_utc", "name" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 8, 26, 12, 46, 39, 768, DateTimeKind.Unspecified).AddTicks(7410), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1999, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -4, 0, 0, 0)), "Discord is a platform for text, voice, and video chat, designed for creating and managing communities and staying connected.", false, new DateTimeOffset(new DateTime(2024, 8, 26, 12, 46, 39, 768, DateTimeKind.Unspecified).AddTicks(7411), new TimeSpan(0, 0, 0, 0, 0)), "Discord" },
                    { 2, new DateTimeOffset(new DateTime(2024, 8, 26, 12, 46, 39, 768, DateTimeKind.Unspecified).AddTicks(7414), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1999, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -4, 0, 0, 0)), "Telegram is a messaging app that offers fast, secure text, voice, and video communication. It supports group chats, channels, and multimedia sharing.", false, new DateTimeOffset(new DateTime(2024, 8, 26, 12, 46, 39, 768, DateTimeKind.Unspecified).AddTicks(7415), new TimeSpan(0, 0, 0, 0, 0)), "Telegram" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_access_key_application_id",
                table: "access_key",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "ix_application_provider_id",
                table: "application",
                column: "provider_id");

            migrationBuilder.CreateIndex(
                name: "ix_application_user_id",
                table: "application",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_email_value",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_username",
                table: "user",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_credentials_credentials_id",
                table: "user_credentials",
                column: "credentials_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_credentials_user_id",
                table: "user_credentials",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "access_key");

            migrationBuilder.DropTable(
                name: "user_credentials");

            migrationBuilder.DropTable(
                name: "application");

            migrationBuilder.DropTable(
                name: "credentials");

            migrationBuilder.DropTable(
                name: "provider");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
