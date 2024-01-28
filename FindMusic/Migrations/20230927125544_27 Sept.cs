using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindMusic.Migrations
{
    public partial class _27Sept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timestamps_Episodes_EpisodeId",
                table: "Timestamps");

            migrationBuilder.DropForeignKey(
                name: "FK_Timestamps_Movies_MovieId",
                table: "Timestamps");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Timestamps",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EpisodeId",
                table: "Timestamps",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timestamps_Episodes_EpisodeId",
                table: "Timestamps");

            migrationBuilder.DropForeignKey(
                name: "FK_Timestamps_Movies_MovieId",
                table: "Timestamps");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Timestamps",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EpisodeId",
                table: "Timestamps",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
