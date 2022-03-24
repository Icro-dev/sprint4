using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cinema.Migrations
{
    public partial class AddedTicketOrderArray : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Orders_TicketOrderId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_TicketOrderId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketOrderId",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "SerializedTicketIds",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerializedTicketIds",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "TicketOrderId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketOrderId",
                table: "Tickets",
                column: "TicketOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Orders_TicketOrderId",
                table: "Tickets",
                column: "TicketOrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
