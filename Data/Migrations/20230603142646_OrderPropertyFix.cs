using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prodaja_kruha_backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderPropertyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProperties_Order_Items_Order_ItemId",
                table: "OrderProperties");

            migrationBuilder.DropIndex(
                name: "IX_OrderProperties_Order_ItemId",
                table: "OrderProperties");

            migrationBuilder.DropColumn(
                name: "Order_ItemId",
                table: "OrderProperties");

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Order_Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_Items_PropertyId",
                table: "Order_Items",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Items_OrderProperties_PropertyId",
                table: "Order_Items",
                column: "PropertyId",
                principalTable: "OrderProperties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Items_OrderProperties_PropertyId",
                table: "Order_Items");

            migrationBuilder.DropIndex(
                name: "IX_Order_Items_PropertyId",
                table: "Order_Items");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Order_Items");

            migrationBuilder.AddColumn<int>(
                name: "Order_ItemId",
                table: "OrderProperties",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProperties_Order_ItemId",
                table: "OrderProperties",
                column: "Order_ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProperties_Order_Items_Order_ItemId",
                table: "OrderProperties",
                column: "Order_ItemId",
                principalTable: "Order_Items",
                principalColumn: "Id");
        }
    }
}
