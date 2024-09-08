using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZedShop.DataLayer.Migrations
{
    public partial class UpdateOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CityId",
                table: "Orders",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProvinceId",
                table: "Orders",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cities_CityId",
                table: "Orders",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Provinces_ProvinceId",
                table: "Orders",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cities_CityId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Provinces_ProvinceId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CityId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProvinceId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Orders",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Orders",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }
    }
}
