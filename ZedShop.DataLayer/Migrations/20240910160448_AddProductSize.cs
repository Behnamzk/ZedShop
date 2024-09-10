using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZedShop.DataLayer.Migrations
{
    public partial class AddProductSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductSizeId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductSizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Heigth = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductSizeId",
                table: "Products",
                column: "ProductSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductSizes_ProductSizeId",
                table: "Products",
                column: "ProductSizeId",
                principalTable: "ProductSizes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductSizes_ProductSizeId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductSizes");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductSizeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductSizeId",
                table: "Products");
        }
    }
}
