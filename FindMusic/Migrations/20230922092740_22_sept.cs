using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindMusic.Migrations
{
    public partial class _22_sept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimestampDurations");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Timestamps");

            migrationBuilder.AddColumn<int>(
                name: "Hour",
                table: "Timestamps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Minute",
                table: "Timestamps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Second",
                table: "Timestamps",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hour",
                table: "Timestamps");

            migrationBuilder.DropColumn(
                name: "Minute",
                table: "Timestamps");

            migrationBuilder.DropColumn(
                name: "Second",
                table: "Timestamps");

            migrationBuilder.AddColumn<DateTime>(
                name: "Duration",
                table: "Timestamps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "TimestampDurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimestampId = table.Column<int>(type: "int", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimestampDurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimestampDurations_Timestamps_TimestampId",
                        column: x => x.TimestampId,
                        principalTable: "Timestamps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimestampDurations_TimestampId",
                table: "TimestampDurations",
                column: "TimestampId");
        }
    }
}
