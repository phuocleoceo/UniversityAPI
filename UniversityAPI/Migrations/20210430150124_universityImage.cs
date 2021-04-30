using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityAPI.Migrations
{
    public partial class universityImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Universities",
                type: "varbinary(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Universities");

            migrationBuilder.UpdateData(
                table: "PathWays",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 4, 22, 22, 14, 45, 783, DateTimeKind.Local).AddTicks(5307));

            migrationBuilder.UpdateData(
                table: "PathWays",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 4, 22, 22, 14, 45, 785, DateTimeKind.Local).AddTicks(1407));

            migrationBuilder.UpdateData(
                table: "PathWays",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2021, 4, 22, 22, 14, 45, 785, DateTimeKind.Local).AddTicks(1456));
        }
    }
}
