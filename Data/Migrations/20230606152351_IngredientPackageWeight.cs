using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prodaja_kruha_backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class IngredientPackageWeight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePerKg",
                table: "Ingredients",
                newName: "Weight");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Ingredients",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Ingredients",
                newName: "PricePerKg");
        }
    }
}
