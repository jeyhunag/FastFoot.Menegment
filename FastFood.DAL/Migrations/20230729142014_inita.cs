using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFood.DAL.Migrations
{
    public partial class inita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "foods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestaurantsId = table.Column<int>(type: "int", nullable: true),
                    FoodsId = table.Column<int>(type: "int", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaigns_foods_FoodsId",
                        column: x => x.FoodsId,
                        principalTable: "foods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Campaigns_restaurants_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "restaurants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SiteInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FavIcon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CitiesId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteInfos_cities_CitiesId",
                        column: x => x.CitiesId,
                        principalTable: "cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_FoodsId",
                table: "Campaigns",
                column: "FoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_RestaurantsId",
                table: "Campaigns",
                column: "RestaurantsId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteInfos_CitiesId",
                table: "SiteInfos",
                column: "CitiesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "SiteInfos");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "foods");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "categories");
        }
    }
}
