using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Online_Store.Migrations
{
    public partial class Cost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

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
                value: new DateTime(2022, 2, 16, 23, 12, 17, 906, DateTimeKind.Local).AddTicks(8698));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureSecond",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PictureSubSecond",
                table: "Products");

            migrationBuilder.AlterColumn<double>(
                name: "Cost",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("088075c9-5a9b-4583-b0e4-279886d46a5d"),
                column: "DataBorn",
                value: new DateTime(2022, 2, 2, 16, 18, 33, 151, DateTimeKind.Local).AddTicks(8424));
        }
    }
}
