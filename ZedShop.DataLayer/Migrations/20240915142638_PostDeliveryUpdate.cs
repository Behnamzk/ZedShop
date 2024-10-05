using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZedShop.DataLayer.Migrations
{
    public partial class PostDeliveryUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PostDeliveries",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "PostDeliveries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostDeliveries_OrderId",
                table: "PostDeliveries",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostDeliveries_Orders_OrderId",
                table: "PostDeliveries",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostDeliveries_Orders_OrderId",
                table: "PostDeliveries");

            migrationBuilder.DropIndex(
                name: "IX_PostDeliveries_OrderId",
                table: "PostDeliveries");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PostDeliveries");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "PostDeliveries");
        }
    }
}
