using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZedShop.DataLayer.Migrations
{
    public partial class UpdateOrderStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "OrderStatuses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "OrderStatuses");
        }
    }
}
