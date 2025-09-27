using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example.Domain.Migrations
{
    /// <inheritdoc />
    public partial class enterdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "auth",
                table: "authentication",
                columns: new[] { "RowId", "Description", "OwnerProject", "Password", "UserType", "Username" },
                values: new object[] { 1L, "1", "AllProjectOwner", "bJKg0LB4QfRPW8dIbQqLyg==", 1L, "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "auth",
                table: "authentication",
                keyColumn: "RowId",
                keyValue: 1L);
        }
    }
}
