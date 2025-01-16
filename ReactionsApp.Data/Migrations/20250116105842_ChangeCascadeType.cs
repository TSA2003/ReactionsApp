using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactionsApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCascadeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RandomPointsGameResults_Users_PlayerId",
                table: "RandomPointsGameResults");

            migrationBuilder.DropForeignKey(
                name: "FK_StartingLightsGameResults_Users_PlayerId",
                table: "StartingLightsGameResults");

            migrationBuilder.AddForeignKey(
                name: "FK_RandomPointsGameResults_Users_PlayerId",
                table: "RandomPointsGameResults",
                column: "PlayerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StartingLightsGameResults_Users_PlayerId",
                table: "StartingLightsGameResults",
                column: "PlayerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RandomPointsGameResults_Users_PlayerId",
                table: "RandomPointsGameResults");

            migrationBuilder.DropForeignKey(
                name: "FK_StartingLightsGameResults_Users_PlayerId",
                table: "StartingLightsGameResults");

            migrationBuilder.AddForeignKey(
                name: "FK_RandomPointsGameResults_Users_PlayerId",
                table: "RandomPointsGameResults",
                column: "PlayerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StartingLightsGameResults_Users_PlayerId",
                table: "StartingLightsGameResults",
                column: "PlayerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
