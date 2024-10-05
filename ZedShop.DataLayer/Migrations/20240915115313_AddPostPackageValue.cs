using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZedShop.DataLayer.Migrations
{
    public partial class AddPostPackageValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostPackageValueId",
                table: "PostDeliveries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostPackageValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ToValue = table.Column<long>(type: "bigint", nullable: false),
                    EffectOnPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostPackageValues", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostDeliveries_PostPackageValueId",
                table: "PostDeliveries",
                column: "PostPackageValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostDeliveries_PostPackageValues_PostPackageValueId",
                table: "PostDeliveries",
                column: "PostPackageValueId",
                principalTable: "PostPackageValues",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostDeliveries_PostPackageValues_PostPackageValueId",
                table: "PostDeliveries");

            migrationBuilder.DropTable(
                name: "PostPackageValues");

            migrationBuilder.DropIndex(
                name: "IX_PostDeliveries_PostPackageValueId",
                table: "PostDeliveries");

            migrationBuilder.DropColumn(
                name: "PostPackageValueId",
                table: "PostDeliveries");
        }
    }
}
