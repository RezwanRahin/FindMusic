using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindMusic.Migrations
{
    public partial class sept16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Seasons");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Movies",
                newName: "ReleaseDate");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Series",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Movies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Series_Slug",
                table: "Series",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Slug",
                table: "Movies",
                column: "Slug",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Series_Slug",
                table: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Movies_Slug",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Movies",
                newName: "Year");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Series",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Seasons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
