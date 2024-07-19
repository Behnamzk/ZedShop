using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZedShop.DataLayer.Migrations
{
    public partial class OpinionsUpdates3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommentDate",
                table: "Opinions",
                newName: "OpinionDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OpinionDate",
                table: "Opinions",
                newName: "CommentDate");
        }
    }
}
