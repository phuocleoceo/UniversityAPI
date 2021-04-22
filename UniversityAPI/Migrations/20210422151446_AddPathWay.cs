using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityAPI.Migrations
{
    public partial class AddPathWay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PathWays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    UniversityId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathWays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PathWays_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PathWays",
                columns: new[] { "Id", "DateCreated", "Difficulty", "Distance", "Name", "UniversityId" },
                values: new object[] { 1, new DateTime(2021, 4, 22, 22, 14, 45, 783, DateTimeKind.Local).AddTicks(5307), 1, 1.0, "Nguyen Luong Bang", 1 });

            migrationBuilder.InsertData(
                table: "PathWays",
                columns: new[] { "Id", "DateCreated", "Difficulty", "Distance", "Name", "UniversityId" },
                values: new object[] { 2, new DateTime(2021, 4, 22, 22, 14, 45, 785, DateTimeKind.Local).AddTicks(1407), 0, 2.0, "Ngu Hanh Son", 2 });

            migrationBuilder.InsertData(
                table: "PathWays",
                columns: new[] { "Id", "DateCreated", "Difficulty", "Distance", "Name", "UniversityId" },
                values: new object[] { 3, new DateTime(2021, 4, 22, 22, 14, 45, 785, DateTimeKind.Local).AddTicks(1456), 2, 1.5, "Luong Nhu Hoc", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_PathWays_UniversityId",
                table: "PathWays",
                column: "UniversityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PathWays");
        }
    }
}
