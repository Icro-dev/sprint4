using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cinema.Migrations
{
    public partial class RoomInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Rooms_RoomId",
                table: "Shows");

            migrationBuilder.DropIndex(
                name: "IX_Shows_RoomId",
                table: "Shows");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Shows",
                newName: "Room");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Room",
                table: "Shows",
                newName: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_RoomId",
                table: "Shows",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Rooms_RoomId",
                table: "Shows",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
