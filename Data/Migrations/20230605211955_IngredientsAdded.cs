using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prodaja_kruha_backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class IngredientsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Weight = table.Column<float>(type: "REAL", nullable: false),
                    Price = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientsUsed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsUsed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientsUsed_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IngredientInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: true),
                    Ammount = table.Column<float>(type: "REAL", nullable: false),
                    IngredientUsedId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientInfos_IngredientsUsed_IngredientUsedId",
                        column: x => x.IngredientUsedId,
                        principalTable: "IngredientsUsed",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IngredientInfos_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientInfos_IngredientId",
                table: "IngredientInfos",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientInfos_IngredientUsedId",
                table: "IngredientInfos",
                column: "IngredientUsedId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsUsed_ProductId",
                table: "IngredientsUsed",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientInfos");

            migrationBuilder.DropTable(
                name: "IngredientsUsed");

            migrationBuilder.DropTable(
                name: "Ingredients");
        }
    }
}
