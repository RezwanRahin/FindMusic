using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindMusic.Migrations
{
    public partial class _1Oct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timestamps_Episodes_EpisodeId",
                table: "Timestamps");

            migrationBuilder.DropForeignKey(
                name: "FK_Timestamps_Movies_MovieId",
                table: "Timestamps");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Tracks",
                newName: "Title");

            migrationBuilder.AddForeignKey(
                name: "FK_Timestamps_Episodes_EpisodeId",
                table: "Timestamps",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Timestamps_Movies_MovieId",
                table: "Timestamps",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timestamps_Episodes_EpisodeId",
                table: "Timestamps");

            migrationBuilder.DropForeignKey(
                name: "FK_Timestamps_Movies_MovieId",
                table: "Timestamps");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Tracks",
                newName: "Description");

            migrationBuilder.AddForeignKey(
                name: "FK_Timestamps_Episodes_EpisodeId",
                table: "Timestamps",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Timestamps_Movies_MovieId",
                table: "Timestamps",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }
    }
}
