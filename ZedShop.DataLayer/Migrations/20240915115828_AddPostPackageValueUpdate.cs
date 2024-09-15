using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZedShop.DataLayer.Migrations
{
    public partial class AddPostPackageValueUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostTypeId",
                table: "PostPackageValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PostPackageValues_PostTypeId",
                table: "PostPackageValues",
                column: "PostTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostPackageValues_PostTypes_PostTypeId",
                table: "PostPackageValues",
                column: "PostTypeId",
                principalTable: "PostTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostPackageValues_PostTypes_PostTypeId",
                table: "PostPackageValues");

            migrationBuilder.DropIndex(
                name: "IX_PostPackageValues_PostTypeId",
                table: "PostPackageValues");

            migrationBuilder.DropColumn(
                name: "PostTypeId",
                table: "PostPackageValues");
        }
    }
}
