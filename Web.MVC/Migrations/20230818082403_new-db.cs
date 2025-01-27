using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.MVC.Migrations
{
    public partial class newdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "RoomType",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomType_RoomId",
                table: "RoomType",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomType_Rooms_RoomId",
                table: "RoomType",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomType_Rooms_RoomId",
                table: "RoomType");

            migrationBuilder.DropIndex(
                name: "IX_RoomType_RoomId",
                table: "RoomType");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "RoomType");
        }
    }
}
