using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Configurations.Migrations
{
    /// <inheritdoc />
    public partial class AddingGameRelationshipToGameRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "GameRequest",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameRequest_GameId",
                table: "GameRequest",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRequest_Game_GameId",
                table: "GameRequest",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRequest_Game_GameId",
                table: "GameRequest");

            migrationBuilder.DropIndex(
                name: "IX_GameRequest_GameId",
                table: "GameRequest");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "GameRequest");
        }
    }
}
