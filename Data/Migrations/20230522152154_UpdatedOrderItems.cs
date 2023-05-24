using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prodaja_kruha_backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedOrderItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Order_Items");

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "Order_Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Order_Items");

            migrationBuilder.AddColumn<float>(
                name: "TotalPrice",
                table: "Order_Items",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
