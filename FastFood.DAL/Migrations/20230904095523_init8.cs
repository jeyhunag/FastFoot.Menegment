using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFood.DAL.Migrations
{
    public partial class init8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_restaurants_AspNetUsers_UserId1",
                table: "restaurants");

            migrationBuilder.DropIndex(
                name: "IX_restaurants_UserId1",
                table: "restaurants");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "restaurants");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "restaurants",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_restaurants_UserId",
                table: "restaurants",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_restaurants_AspNetUsers_UserId",
                table: "restaurants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_restaurants_AspNetUsers_UserId",
                table: "restaurants");

            migrationBuilder.DropIndex(
                name: "IX_restaurants_UserId",
                table: "restaurants");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "restaurants",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "restaurants",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_restaurants_UserId1",
                table: "restaurants",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_restaurants_AspNetUsers_UserId1",
                table: "restaurants",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
