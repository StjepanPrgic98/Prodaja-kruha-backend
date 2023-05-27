using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prodaja_kruha_backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedProductInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductsInformation_ProductInfoId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductInfoId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductInfoId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductsInformation",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInformation_ProductId",
                table: "ProductsInformation",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsInformation_Products_ProductId",
                table: "ProductsInformation",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsInformation_Products_ProductId",
                table: "ProductsInformation");

            migrationBuilder.DropIndex(
                name: "IX_ProductsInformation_ProductId",
                table: "ProductsInformation");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductsInformation");

            migrationBuilder.AddColumn<int>(
                name: "ProductInfoId",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductInfoId",
                table: "Products",
                column: "ProductInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductsInformation_ProductInfoId",
                table: "Products",
                column: "ProductInfoId",
                principalTable: "ProductsInformation",
                principalColumn: "Id");
        }
    }
}
