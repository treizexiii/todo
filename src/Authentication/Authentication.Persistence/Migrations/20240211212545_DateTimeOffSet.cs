using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Authentication.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DateTimeOffSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("a8a419a0-f0e4-4571-8e40-60fda468d56c"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("f3549e74-b5c5-4849-9507-956960239a40"));

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("3b33ddef-f49a-4643-88e8-fcbf3c4fcf16"), "Admin" },
                    { new Guid("6387fd8f-b982-428e-b933-462b156334e9"), "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("3b33ddef-f49a-4643-88e8-fcbf3c4fcf16"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("6387fd8f-b982-428e-b933-462b156334e9"));

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("a8a419a0-f0e4-4571-8e40-60fda468d56c"), "Admin" },
                    { new Guid("f3549e74-b5c5-4849-9507-956960239a40"), "User" }
                });
        }
    }
}
