using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b3322cd9-9a4c-4c3e-9811-2879339a9968", "Admin", "Adminic", "AQAAAAIAAYagAAAAEDpCwgYQeb3a+J/bKFb9GjxFd8YC1feDUqhsbcrG/ZadwBolo1Ls0Y8EQgPlbNwe/Q==", "b28bc70a-2215-4ca9-97c5-805cc367ba6c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9eac5304-1486-4142-a343-220b848dc775", "AQAAAAIAAYagAAAAEBrd6NCBKZsbCkuaU74cX2e+FUJbX9oQRQ7zMDP7gIBH7L4pPrcffcYZ6l4Sjqn5mw==", "32cd13f3-6cc8-40b9-a093-903e7ef23b3e" });
        }
    }
}
