using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityAPI.Migrations
{
    public partial class newData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Universities",
                columns: new[] { "Id", "Abbreviation", "Address", "Established", "Name" },
                values: new object[,]
                {
                    { 3, "UFL", "Đà Nẵng", new DateTime(2002, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ngoại Ngữ Đà Nẵng" },
                    { 4, "UTE", "Đà Nẵng", new DateTime(2017, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sư phạm kĩ thuật Đà Nẵng" },
                    { 5, "UED", "Đà Nẵng", new DateTime(1994, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sư phạm Đà Nẵng" },
                    { 6, "SMP", "Đà Nẵng", new DateTime(2007, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Khoa Y Dược Đà Nẵng" },
                    { 7, "VKU", "Đà Nẵng", new DateTime(2020, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "CNTT Việt-Hàn Đà Nẵng" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
