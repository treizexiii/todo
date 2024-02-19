using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Authentication.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Secrets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "secrets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ownerid = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    openedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    closedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    revokedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    value = table.Column<byte[]>(type: "bytea", nullable: false),
                    salt = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_secrets", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("a8a419a0-f0e4-4571-8e40-60fda468d56c"), "Admin" },
                    { new Guid("f3549e74-b5c5-4849-9507-956960239a40"), "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "secrets");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("a8a419a0-f0e4-4571-8e40-60fda468d56c"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("f3549e74-b5c5-4849-9507-956960239a40"));
        }
    }
}
