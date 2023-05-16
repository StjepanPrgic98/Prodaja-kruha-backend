using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prodaja_kruha_backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnglishLanguageChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Naziv",
                table: "Proizvodi",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Cijena",
                table: "Proizvodi",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Ime",
                table: "Kupci",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Proizvodi",
                newName: "Naziv");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Proizvodi",
                newName: "Cijena");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Kupci",
                newName: "Ime");
        }
    }
}
