using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShishaFlavours.Data.Migrations
{
    public partial class ChangedUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlavourCombinations_AspNetUsers_UserId1",
                table: "FlavourCombinations");

            migrationBuilder.DropIndex(
                name: "IX_FlavourCombinations_UserId1",
                table: "FlavourCombinations");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "FlavourCombinations");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FlavourCombinations",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_FlavourCombinations_UserId",
                table: "FlavourCombinations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlavourCombinations_AspNetUsers_UserId",
                table: "FlavourCombinations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlavourCombinations_AspNetUsers_UserId",
                table: "FlavourCombinations");

            migrationBuilder.DropIndex(
                name: "IX_FlavourCombinations_UserId",
                table: "FlavourCombinations");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "FlavourCombinations",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "FlavourCombinations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlavourCombinations_UserId1",
                table: "FlavourCombinations",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FlavourCombinations_AspNetUsers_UserId1",
                table: "FlavourCombinations",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
