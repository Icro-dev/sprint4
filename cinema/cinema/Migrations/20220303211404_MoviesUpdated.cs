using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cinema.Migrations
{
    public partial class MoviesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdvisedAge",
                table: "Movies",
                newName: "ReleaseYear");

            migrationBuilder.AddColumn<string>(
                name: "Cast",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryOfOrigin",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cast",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CountryOfOrigin",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "ReleaseYear",
                table: "Movies",
                newName: "AdvisedAge");
        }
    }
}
