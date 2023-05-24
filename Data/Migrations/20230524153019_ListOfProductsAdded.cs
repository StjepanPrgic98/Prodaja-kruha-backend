using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prodaja_kruha_backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class ListOfProductsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Items_Products_ProductsId",
                table: "Order_Items");

            migrationBuilder.DropIndex(
                name: "IX_Order_Items_ProductsId",
                table: "Order_Items");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "Order_Items");

            migrationBuilder.AddColumn<int>(
                name: "Order_itemId",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Order_itemId",
                table: "Products",
                column: "Order_itemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Order_Items_Order_itemId",
                table: "Products",
                column: "Order_itemId",
                principalTable: "Order_Items",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Order_Items_Order_itemId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Order_itemId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Order_itemId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "Order_Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_Items_ProductsId",
                table: "Order_Items",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Items_Products_ProductsId",
                table: "Order_Items",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
