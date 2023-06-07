using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prodaja_kruha_backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderPropertiesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsInformation_Order_Items_Order_itemId",
                table: "ProductsInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsInformation_Products_ProductId",
                table: "ProductsInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsInformation",
                table: "ProductsInformation");

            migrationBuilder.RenameTable(
                name: "ProductsInformation",
                newName: "ProductInfo");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsInformation_ProductId",
                table: "ProductInfo",
                newName: "IX_ProductInfo_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsInformation_Order_itemId",
                table: "ProductInfo",
                newName: "IX_ProductInfo_Order_itemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInfo",
                table: "ProductInfo",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OrderProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Order_ItemId = table.Column<int>(type: "INTEGER", nullable: true),
                    Property = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProperties_Order_Items_Order_ItemId",
                        column: x => x.Order_ItemId,
                        principalTable: "Order_Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProperties_Order_ItemId",
                table: "OrderProperties",
                column: "Order_ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInfo_Order_Items_Order_itemId",
                table: "ProductInfo",
                column: "Order_itemId",
                principalTable: "Order_Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInfo_Products_ProductId",
                table: "ProductInfo",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInfo_Order_Items_Order_itemId",
                table: "ProductInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInfo_Products_ProductId",
                table: "ProductInfo");

            migrationBuilder.DropTable(
                name: "OrderProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInfo",
                table: "ProductInfo");

            migrationBuilder.RenameTable(
                name: "ProductInfo",
                newName: "ProductsInformation");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInfo_ProductId",
                table: "ProductsInformation",
                newName: "IX_ProductsInformation_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInfo_Order_itemId",
                table: "ProductsInformation",
                newName: "IX_ProductsInformation_Order_itemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsInformation",
                table: "ProductsInformation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsInformation_Order_Items_Order_itemId",
                table: "ProductsInformation",
                column: "Order_itemId",
                principalTable: "Order_Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsInformation_Products_ProductId",
                table: "ProductsInformation",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
