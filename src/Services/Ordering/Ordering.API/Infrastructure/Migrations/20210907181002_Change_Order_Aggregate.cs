using Microsoft.EntityFrameworkCore.Migrations;

namespace Ordering.API.Infrastructure.Migrations
{
    public partial class Change_Order_Aggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                schema: "ordering",
                table: "orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiscountCode",
                schema: "ordering",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DiscountConfirmed",
                schema: "ordering",
                table: "orders",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                schema: "ordering",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "DiscountCode",
                schema: "ordering",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "DiscountConfirmed",
                schema: "ordering",
                table: "orders");
        }
    }
}
