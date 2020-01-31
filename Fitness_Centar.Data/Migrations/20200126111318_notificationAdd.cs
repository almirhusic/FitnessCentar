using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitness_Centar.Data.Migrations
{
    public partial class notificationAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifikacija",
                columns: table => new
                {
                    NotifikacijaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Sadrzaj = table.Column<string>(nullable: true),
                    SourceClanId = table.Column<int>(nullable: false),
                    DestClanId = table.Column<int>(nullable: false),
                    ObjavaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifikacija", x => x.NotifikacijaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifikacija");
        }
    }
}
