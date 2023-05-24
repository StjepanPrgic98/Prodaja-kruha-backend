using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prodaja_kruha_backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductInfoAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Order_Items_Order_itemId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Order_Items");

            migrationBuilder.RenameColumn(
                name: "Order_itemId",
                table: "Products",
                newName: "ProductInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Order_itemId",
                table: "Products",
                newName: "IX_Products_ProductInfoId");

            migrationBuilder.CreateTable(
                name: "ProductsInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Order_itemId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsInformation_Order_Items_Order_itemId",
                        column: x => x.Order_itemId,
                        principalTable: "Order_Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInformation_Order_itemId",
                table: "ProductsInformation",
                column: "Order_itemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductsInformation_ProductInfoId",
                table: "Products",
                column: "ProductInfoId",
                principalTable: "ProductsInformation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductsInformation_ProductInfoId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductsInformation");

            migrationBuilder.RenameColumn(
                name: "ProductInfoId",
                table: "Products",
                newName: "Order_itemId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductInfoId",
                table: "Products",
                newName: "IX_Products_Order_itemId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Order_Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Order_Items_Order_itemId",
                table: "Products",
                column: "Order_itemId",
                principalTable: "Order_Items",
                principalColumn: "Id");
        }
    }
}
