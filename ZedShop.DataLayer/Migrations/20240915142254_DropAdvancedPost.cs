using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZedShop.DataLayer.Migrations
{
    public partial class DropAdvancedPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostDeliveries_PostBasics_PostBasicId",
                table: "PostDeliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_PostDeliveries_PostBoxes_PostBoxId",
                table: "PostDeliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_PostDeliveries_PostDistances_PostDistanceId",
                table: "PostDeliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_PostDeliveries_PostPackageValues_PostPackageValueId",
                table: "PostDeliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_PostDeliveries_PostWeights_PostWeightId",
                table: "PostDeliveries");

            migrationBuilder.DropTable(
                name: "PostBasics");

            migrationBuilder.DropTable(
                name: "PostBoxes");

            migrationBuilder.DropTable(
                name: "PostDistances");

            migrationBuilder.DropTable(
                name: "PostPackageValues");

            migrationBuilder.DropTable(
                name: "PostWeights");

            migrationBuilder.DropTable(
                name: "PostTypes");

            migrationBuilder.DropIndex(
                name: "IX_PostDeliveries_PostBasicId",
                table: "PostDeliveries");

            migrationBuilder.DropIndex(
                name: "IX_PostDeliveries_PostBoxId",
                table: "PostDeliveries");

            migrationBuilder.DropIndex(
                name: "IX_PostDeliveries_PostDistanceId",
                table: "PostDeliveries");

            migrationBuilder.DropIndex(
                name: "IX_PostDeliveries_PostPackageValueId",
                table: "PostDeliveries");

            migrationBuilder.DropIndex(
                name: "IX_PostDeliveries_PostWeightId",
                table: "PostDeliveries");

            migrationBuilder.DropColumn(
                name: "PostBasicId",
                table: "PostDeliveries");

            migrationBuilder.DropColumn(
                name: "PostBoxId",
                table: "PostDeliveries");

            migrationBuilder.DropColumn(
                name: "PostDistanceId",
                table: "PostDeliveries");

            migrationBuilder.DropColumn(
                name: "PostPackageValueId",
                table: "PostDeliveries");

            migrationBuilder.DropColumn(
                name: "PostWeightId",
                table: "PostDeliveries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostBasicId",
                table: "PostDeliveries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostBoxId",
                table: "PostDeliveries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostDistanceId",
                table: "PostDeliveries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostPackageValueId",
                table: "PostDeliveries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostWeightId",
                table: "PostDeliveries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripton = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostBasics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostTypeId = table.Column<int>(type: "int", nullable: false),
                    BasePrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostBasics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostBasics_PostTypes_PostTypeId",
                        column: x => x.PostTypeId,
                        principalTable: "PostTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostBoxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostTypeId = table.Column<int>(type: "int", nullable: false),
                    BoxName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BoxPrice = table.Column<double>(type: "float", nullable: false),
                    EffectOnPrice = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Lenght = table.Column<float>(type: "real", nullable: false),
                    Width = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostBoxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostBoxes_PostTypes_PostTypeId",
                        column: x => x.PostTypeId,
                        principalTable: "PostTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostDistances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostTypeId = table.Column<int>(type: "int", nullable: false),
                    EffectOnPrice = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostDistances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostDistances_PostTypes_PostTypeId",
                        column: x => x.PostTypeId,
                        principalTable: "PostTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostPackageValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostTypeId = table.Column<int>(type: "int", nullable: false),
                    EffectOnPrice = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ToValue = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostPackageValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostPackageValues_PostTypes_PostTypeId",
                        column: x => x.PostTypeId,
                        principalTable: "PostTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostWeights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostTypeId = table.Column<int>(type: "int", nullable: false),
                    EffectOnPrice = table.Column<double>(type: "float", nullable: false),
                    UntilWeightName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UntilWeightNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostWeights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostWeights_PostTypes_PostTypeId",
                        column: x => x.PostTypeId,
                        principalTable: "PostTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostDeliveries_PostBasicId",
                table: "PostDeliveries",
                column: "PostBasicId");

            migrationBuilder.CreateIndex(
                name: "IX_PostDeliveries_PostBoxId",
                table: "PostDeliveries",
                column: "PostBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_PostDeliveries_PostDistanceId",
                table: "PostDeliveries",
                column: "PostDistanceId");

            migrationBuilder.CreateIndex(
                name: "IX_PostDeliveries_PostPackageValueId",
                table: "PostDeliveries",
                column: "PostPackageValueId");

            migrationBuilder.CreateIndex(
                name: "IX_PostDeliveries_PostWeightId",
                table: "PostDeliveries",
                column: "PostWeightId");

            migrationBuilder.CreateIndex(
                name: "IX_PostBasics_PostTypeId",
                table: "PostBasics",
                column: "PostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostBoxes_PostTypeId",
                table: "PostBoxes",
                column: "PostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostDistances_PostTypeId",
                table: "PostDistances",
                column: "PostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostPackageValues_PostTypeId",
                table: "PostPackageValues",
                column: "PostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostWeights_PostTypeId",
                table: "PostWeights",
                column: "PostTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostDeliveries_PostBasics_PostBasicId",
                table: "PostDeliveries",
                column: "PostBasicId",
                principalTable: "PostBasics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostDeliveries_PostBoxes_PostBoxId",
                table: "PostDeliveries",
                column: "PostBoxId",
                principalTable: "PostBoxes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostDeliveries_PostDistances_PostDistanceId",
                table: "PostDeliveries",
                column: "PostDistanceId",
                principalTable: "PostDistances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostDeliveries_PostPackageValues_PostPackageValueId",
                table: "PostDeliveries",
                column: "PostPackageValueId",
                principalTable: "PostPackageValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostDeliveries_PostWeights_PostWeightId",
                table: "PostDeliveries",
                column: "PostWeightId",
                principalTable: "PostWeights",
                principalColumn: "Id");
        }
    }
}
