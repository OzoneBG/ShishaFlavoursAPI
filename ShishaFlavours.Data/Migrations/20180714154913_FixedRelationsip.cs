using Microsoft.EntityFrameworkCore.Migrations;

namespace ShishaFlavours.Data.Migrations
{
    public partial class FixedRelationsip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flavours_FlavourCombinations_FlavourCombinationId",
                table: "Flavours");

            migrationBuilder.DropIndex(
                name: "IX_Flavours_FlavourCombinationId",
                table: "Flavours");

            migrationBuilder.DropColumn(
                name: "FlavourCombinationId",
                table: "Flavours");

            migrationBuilder.CreateTable(
                name: "FlavourCombinationReference",
                columns: table => new
                {
                    FlavourId = table.Column<int>(nullable: false),
                    FlavourCombinationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlavourCombinationReference", x => new { x.FlavourId, x.FlavourCombinationId });
                    table.ForeignKey(
                        name: "FK_FlavourCombinationReference_FlavourCombinations_FlavourCombinationId",
                        column: x => x.FlavourCombinationId,
                        principalTable: "FlavourCombinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlavourCombinationReference_Flavours_FlavourId",
                        column: x => x.FlavourId,
                        principalTable: "Flavours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlavourCombinationReference_FlavourCombinationId",
                table: "FlavourCombinationReference",
                column: "FlavourCombinationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlavourCombinationReference");

            migrationBuilder.AddColumn<int>(
                name: "FlavourCombinationId",
                table: "Flavours",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flavours_FlavourCombinationId",
                table: "Flavours",
                column: "FlavourCombinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flavours_FlavourCombinations_FlavourCombinationId",
                table: "Flavours",
                column: "FlavourCombinationId",
                principalTable: "FlavourCombinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
