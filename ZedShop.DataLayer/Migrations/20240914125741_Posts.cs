using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZedShop.DataLayer.Migrations
{
    public partial class Posts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostDeliveryId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripton = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
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
                    BasePrice = table.Column<double>(type: "float", nullable: false),
                    PostTypeId = table.Column<int>(type: "int", nullable: false)
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
                    BoxName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EffectOnPrice = table.Column<double>(type: "float", nullable: false),
                    BoxPrice = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<float>(type: "real", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Lenght = table.Column<float>(type: "real", nullable: false),
                    PostTypeId = table.Column<int>(type: "int", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    EffectOnPrice = table.Column<double>(type: "float", nullable: false),
                    PostTypeId = table.Column<int>(type: "int", nullable: false)
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
                name: "PostWeights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UntilWeightName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UntilWeightNumber = table.Column<int>(type: "int", nullable: false),
                    EffectOnPrice = table.Column<double>(type: "float", nullable: false),
                    PostTypeId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "PostDeliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    PostBoxId = table.Column<int>(type: "int", nullable: true),
                    PostWeightId = table.Column<int>(type: "int", nullable: true),
                    PostDistanceId = table.Column<int>(type: "int", nullable: true),
                    PostBasicId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostDeliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostDeliveries_PostBasics_PostBasicId",
                        column: x => x.PostBasicId,
                        principalTable: "PostBasics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostDeliveries_PostBoxes_PostBoxId",
                        column: x => x.PostBoxId,
                        principalTable: "PostBoxes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostDeliveries_PostDistances_PostDistanceId",
                        column: x => x.PostDistanceId,
                        principalTable: "PostDistances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostDeliveries_PostWeights_PostWeightId",
                        column: x => x.PostWeightId,
                        principalTable: "PostWeights",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PostDeliveryId",
                table: "Orders",
                column: "PostDeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_PostBasics_PostTypeId",
                table: "PostBasics",
                column: "PostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostBoxes_PostTypeId",
                table: "PostBoxes",
                column: "PostTypeId");

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
                name: "IX_PostDeliveries_PostWeightId",
                table: "PostDeliveries",
                column: "PostWeightId");

            migrationBuilder.CreateIndex(
                name: "IX_PostDistances_PostTypeId",
                table: "PostDistances",
                column: "PostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostWeights_PostTypeId",
                table: "PostWeights",
                column: "PostTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PostDeliveries_PostDeliveryId",
                table: "Orders",
                column: "PostDeliveryId",
                principalTable: "PostDeliveries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PostDeliveries_PostDeliveryId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "PostDeliveries");

            migrationBuilder.DropTable(
                name: "PostBasics");

            migrationBuilder.DropTable(
                name: "PostBoxes");

            migrationBuilder.DropTable(
                name: "PostDistances");

            migrationBuilder.DropTable(
                name: "PostWeights");

            migrationBuilder.DropTable(
                name: "PostTypes");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PostDeliveryId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PostDeliveryId",
                table: "Orders");
        }
    }
}
