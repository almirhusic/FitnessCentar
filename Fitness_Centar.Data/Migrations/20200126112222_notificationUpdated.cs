using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitness_Centar.Data.Migrations
{
    public partial class notificationUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Read",
                table: "Notifikacija",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Seen",
                table: "Notifikacija",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Read",
                table: "Notifikacija");

            migrationBuilder.DropColumn(
                name: "Seen",
                table: "Notifikacija");
        }
    }
}
