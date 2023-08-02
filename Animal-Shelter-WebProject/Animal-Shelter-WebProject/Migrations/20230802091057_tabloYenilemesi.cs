using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animal_Shelter_WebProject.Migrations
{
    /// <inheritdoc />
    public partial class tabloYenilemesi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SurecDurumlari",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SahiplenmeBilgisi",
                table: "Adoptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SurecDurumlari",
                table: "Adoptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SurecDurumlari",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "SahiplenmeBilgisi",
                table: "Adoptions");

            migrationBuilder.DropColumn(
                name: "SurecDurumlari",
                table: "Adoptions");
        }
    }
}
