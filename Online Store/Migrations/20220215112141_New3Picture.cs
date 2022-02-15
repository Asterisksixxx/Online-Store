using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Online_Store.Migrations
{
    public partial class New3Picture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureSecond",
                table: "Products",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureSubSecond",
                table: "Products",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("088075c9-5a9b-4583-b0e4-279886d46a5d"),
                column: "DataBorn",
                value: new DateTime(2022, 2, 15, 14, 21, 40, 964, DateTimeKind.Local).AddTicks(4926));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureSecond",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PictureSubSecond",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("088075c9-5a9b-4583-b0e4-279886d46a5d"),
                column: "DataBorn",
                value: new DateTime(2022, 2, 2, 16, 18, 33, 151, DateTimeKind.Local).AddTicks(8424));
        }
    }
}
