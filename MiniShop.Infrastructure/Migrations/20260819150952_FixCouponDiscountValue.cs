using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCouponDiscountValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1,
                column: "DiscountValue",
                value: 0.2m);

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "Id", "Code", "DiscountType", "DiscountValue", "ExpiresAt", "IsActive", "MinimumOrderAmount", "UsageLimit" },
                values: new object[] { 2, "SCHOOL5", "FixedAmount", 5m, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 20m, 1000 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1,
                column: "DiscountValue",
                value: 10m);
        }
    }
}
