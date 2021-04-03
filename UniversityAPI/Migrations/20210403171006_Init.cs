using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityAPI.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Established = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Universities",
                columns: new[] { "Id", "Abbreviation", "Address", "Established", "Name" },
                values: new object[] { 1, "DUT", "Đà Nẵng", new DateTime(1975, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bách khoa Đà Nẵng" });

            migrationBuilder.InsertData(
                table: "Universities",
                columns: new[] { "Id", "Abbreviation", "Address", "Established", "Name" },
                values: new object[] { 2, "DUE", "Đà Nẵng", new DateTime(1975, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kinh tế Đà Nẵng" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Universities");
        }
    }
}
