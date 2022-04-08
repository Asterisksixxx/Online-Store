using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Online_Store.Migrations
{
    public partial class FinalUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Points",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "PromocodId",
                table: "SubSections",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Promocods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiskontPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActivateFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocods", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("088075c9-5a9b-4583-b0e4-279886d46a5d"),
                column: "DataBorn",
                value: new DateTime(2022, 3, 28, 21, 7, 46, 698, DateTimeKind.Local).AddTicks(4050));

            migrationBuilder.CreateIndex(
                name: "IX_SubSections_PromocodId",
                table: "SubSections",
                column: "PromocodId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubSections_Promocods_PromocodId",
                table: "SubSections",
                column: "PromocodId",
                principalTable: "Promocods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubSections_Promocods_PromocodId",
                table: "SubSections");

            migrationBuilder.DropTable(
                name: "Promocods");

            migrationBuilder.DropIndex(
                name: "IX_SubSections_PromocodId",
                table: "SubSections");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PromocodId",
                table: "SubSections");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("088075c9-5a9b-4583-b0e4-279886d46a5d"),
                column: "DataBorn",
                value: new DateTime(2022, 3, 7, 14, 38, 59, 125, DateTimeKind.Local).AddTicks(6948));
        }
    }
}
