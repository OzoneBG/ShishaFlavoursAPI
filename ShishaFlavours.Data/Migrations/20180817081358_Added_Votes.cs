using Microsoft.EntityFrameworkCore.Migrations;

namespace ShishaFlavours.Data.Migrations
{
    public partial class Added_Votes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Votes",
                table: "FlavourCombinations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Votes",
                table: "FlavourCombinations");
        }
    }
}
