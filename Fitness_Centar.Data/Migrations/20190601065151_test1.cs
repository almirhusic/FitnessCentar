using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitness_Centar.Data.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Followers",
                columns: table => new
                {
                    FollowersId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PratiteljClanId = table.Column<int>(nullable: false),
                    ZapraceniClanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followers", x => x.FollowersId);
                    table.ForeignKey(
                        name: "FK_Followers_Clan_PratiteljClanId",
                        column: x => x.PratiteljClanId,
                        principalTable: "Clan",
                        principalColumn: "ClanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Followers_Clan_ZapraceniClanId",
                        column: x => x.ZapraceniClanId,
                        principalTable: "Clan",
                        principalColumn: "ClanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Followers_PratiteljClanId",
                table: "Followers",
                column: "PratiteljClanId");

            migrationBuilder.CreateIndex(
                name: "IX_Followers_ZapraceniClanId",
                table: "Followers",
                column: "ZapraceniClanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Followers");
        }
    }
}
