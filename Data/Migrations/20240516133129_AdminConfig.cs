using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdminConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "aaff6c8d-e536-4907-b355-4d8c84009492", "admin@valcon.com", "ADMIN@VALCON.COM", "ADMIN@VALCON.COM", "AQAAAAIAAYagAAAAEBQ5NQNFFa4rBUQfSno3L8CTIrraULY96aHLMtBmsOtxg6vvIzSjKv7/pl47mtTETg==", "52411b56-75d9-4dca-9704-2afcb9180de3", "admin@valcon.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "500951ef-02e2-4110-a452-94866c62ff25", "admin@example.com", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEDGf9dhFdh1mX0l/106gksCz+fwr0uIOk34/DEUJ7GKS/XRcVxBO4kFy7UGneWz2DA==", "dbecb36a-9019-4864-8b6a-518f426ee3d8", "admin@example.com" });
        }
    }
}
