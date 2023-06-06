using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBMS.Repository.Migrations
{
    public partial class Em16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BarBeersBarId",
                table: "Beers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BarBeersBeerId",
                table: "Beers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarBeers",
                table: "BarBeers",
                columns: new[] { "BarId", "BeerId" });

            migrationBuilder.CreateTable(
                name: "BarBeer",
                columns: table => new
                {
                    BarsId = table.Column<int>(type: "int", nullable: false),
                    BeersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarBeer", x => new { x.BarsId, x.BeersId });
                    table.ForeignKey(
                        name: "FK_BarBeer_Bars_BarsId",
                        column: x => x.BarsId,
                        principalTable: "Bars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarBeer_Beers_BeersId",
                        column: x => x.BeersId,
                        principalTable: "Beers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beers_BarBeersBarId_BarBeersBeerId",
                table: "Beers",
                columns: new[] { "BarBeersBarId", "BarBeersBeerId" });

            migrationBuilder.CreateIndex(
                name: "IX_BarBeer_BeersId",
                table: "BarBeer",
                column: "BeersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_BarBeers_BarBeersBarId_BarBeersBeerId",
                table: "Beers",
                columns: new[] { "BarBeersBarId", "BarBeersBeerId" },
                principalTable: "BarBeers",
                principalColumns: new[] { "BarId", "BeerId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_BarBeers_BarBeersBarId_BarBeersBeerId",
                table: "Beers");

            migrationBuilder.DropTable(
                name: "BarBeer");

            migrationBuilder.DropIndex(
                name: "IX_Beers_BarBeersBarId_BarBeersBeerId",
                table: "Beers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarBeers",
                table: "BarBeers");

            migrationBuilder.DropColumn(
                name: "BarBeersBarId",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "BarBeersBeerId",
                table: "Beers");
        }
    }
}
