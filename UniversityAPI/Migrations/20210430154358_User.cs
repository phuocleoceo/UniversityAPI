using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityAPI.Migrations
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "PathWays",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 4, 30, 22, 43, 58, 202, DateTimeKind.Local).AddTicks(9451));

            migrationBuilder.UpdateData(
                table: "PathWays",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 4, 30, 22, 43, 58, 203, DateTimeKind.Local).AddTicks(9963));

            migrationBuilder.UpdateData(
                table: "PathWays",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2021, 4, 30, 22, 43, 58, 203, DateTimeKind.Local).AddTicks(9995));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Role", "UserName" },
                values: new object[] { 1, "1", "Admin", "phuoc" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.UpdateData(
                table: "PathWays",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 4, 30, 22, 1, 23, 607, DateTimeKind.Local).AddTicks(6295));

            migrationBuilder.UpdateData(
                table: "PathWays",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 4, 30, 22, 1, 23, 608, DateTimeKind.Local).AddTicks(7660));

            migrationBuilder.UpdateData(
                table: "PathWays",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2021, 4, 30, 22, 1, 23, 608, DateTimeKind.Local).AddTicks(7695));
        }
    }
}
