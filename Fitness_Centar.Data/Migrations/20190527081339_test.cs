using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitness_Centar.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grad",
                columns: table => new
                {
                    GradId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grad", x => x.GradId);
                });

            migrationBuilder.CreateTable(
                name: "ObracunskiPeriod",
                columns: table => new
                {
                    ObracunskiPeriodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObracunskiPeriod", x => x.ObracunskiPeriodId);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    SalaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: false),
                    BrojMjesta = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.SalaId);
                });

            migrationBuilder.CreateTable(
                name: "VrstaClanarine",
                columns: table => new
                {
                    VrstaClanarineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: false),
                    Cijena = table.Column<decimal>(nullable: false),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstaClanarine", x => x.VrstaClanarineId);
                });

            migrationBuilder.CreateTable(
                name: "VrstaTreninga",
                columns: table => new
                {
                    VrstaTreningaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstaTreninga", x => x.VrstaTreningaId);
                });

            migrationBuilder.CreateTable(
                name: "Zaposlenik",
                columns: table => new
                {
                    ZaposlenikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsTrener = table.Column<bool>(nullable: false),
                    IsMenadzer = table.Column<bool>(nullable: false),
                    IsRecepcionar = table.Column<bool>(nullable: false),
                    KorisnickoIme = table.Column<string>(nullable: false),
                    Lozinka = table.Column<string>(nullable: false),
                    Ime = table.Column<string>(nullable: false),
                    Prezime = table.Column<string>(nullable: false),
                    Spol = table.Column<string>(nullable: true),
                    JMBG = table.Column<string>(nullable: false),
                    DatumRodjenja = table.Column<DateTime>(nullable: false),
                    DatumZaposlenja = table.Column<DateTime>(nullable: false),
                    Telefon = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: false),
                    PocetakRada = table.Column<string>(nullable: false),
                    KrajRada = table.Column<string>(nullable: false),
                    GradId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposlenik", x => x.ZaposlenikId);
                    table.ForeignKey(
                        name: "FK_Zaposlenik_Grad_GradId",
                        column: x => x.GradId,
                        principalTable: "Grad",
                        principalColumn: "GradId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clan",
                columns: table => new
                {
                    ClanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(nullable: false),
                    Prezime = table.Column<string>(nullable: false),
                    KorisnickoIme = table.Column<string>(nullable: false),
                    Lozinka = table.Column<string>(nullable: false),
                    JMBG = table.Column<string>(nullable: false),
                    DatumRodjenja = table.Column<DateTime>(nullable: false),
                    Adresa = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Spol = table.Column<string>(nullable: true),
                    DatumUclanjivanja = table.Column<DateTime>(nullable: false),
                    BrojClanskeKartice = table.Column<int>(nullable: false),
                    VrstaClanarineId = table.Column<int>(nullable: false),
                    GradId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clan", x => x.ClanId);
                    table.ForeignKey(
                        name: "FK_Clan_Grad_GradId",
                        column: x => x.GradId,
                        principalTable: "Grad",
                        principalColumn: "GradId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clan_VrstaClanarine_VrstaClanarineId",
                        column: x => x.VrstaClanarineId,
                        principalTable: "VrstaClanarine",
                        principalColumn: "VrstaClanarineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Obavijest",
                columns: table => new
                {
                    ObavijestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naslov = table.Column<string>(nullable: false),
                    DatumObjave = table.Column<DateTime>(nullable: false),
                    Sadrzaj = table.Column<string>(nullable: false),
                    ZaposlenikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavijest", x => x.ObavijestId);
                    table.ForeignKey(
                        name: "FK_Obavijest_Zaposlenik_ZaposlenikId",
                        column: x => x.ZaposlenikId,
                        principalTable: "Zaposlenik",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObracunPlata",
                columns: table => new
                {
                    ObracunPlataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Satnica = table.Column<float>(nullable: false),
                    BrojRadnihSati = table.Column<float>(nullable: false),
                    BrojRadnihDana = table.Column<int>(nullable: false),
                    IznosPlate = table.Column<float>(nullable: false),
                    ObracunskiPeriodId = table.Column<int>(nullable: false),
                    ZaposlenikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObracunPlata", x => x.ObracunPlataId);
                    table.ForeignKey(
                        name: "FK_ObracunPlata_ObracunskiPeriod_ObracunskiPeriodId",
                        column: x => x.ObracunskiPeriodId,
                        principalTable: "ObracunskiPeriod",
                        principalColumn: "ObracunskiPeriodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObracunPlata_Zaposlenik_ZaposlenikId",
                        column: x => x.ZaposlenikId,
                        principalTable: "Zaposlenik",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trener",
                columns: table => new
                {
                    TrenerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GodineIskustva = table.Column<int>(nullable: true),
                    ZaposlenikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trener", x => x.TrenerId);
                    table.ForeignKey(
                        name: "FK_Trener_Zaposlenik_ZaposlenikId",
                        column: x => x.ZaposlenikId,
                        principalTable: "Zaposlenik",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DropInInfo",
                columns: table => new
                {
                    DropInInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojDolazaka = table.Column<int>(nullable: false),
                    PreostaliBrojDolazaka = table.Column<int>(nullable: false),
                    DatumIsteka = table.Column<DateTime>(nullable: false),
                    ClanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DropInInfo", x => x.DropInInfoId);
                    table.ForeignKey(
                        name: "FK_DropInInfo_Clan_ClanId",
                        column: x => x.ClanId,
                        principalTable: "Clan",
                        principalColumn: "ClanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Followers",
                columns: table => new
                {
                    FollowersId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PratiteljClanId = table.Column<int>(nullable: false),
                    PratiocClanId = table.Column<int>(nullable: false),
                    ClanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followers", x => x.FollowersId);
                    table.ForeignKey(
                        name: "FK_Followers_Clan_ClanId",
                        column: x => x.ClanId,
                        principalTable: "Clan",
                        principalColumn: "ClanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistorijaDolazaka",
                columns: table => new
                {
                    HistorijaDolazakaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumDolaska = table.Column<DateTime>(nullable: false),
                    VrijemeDolaska = table.Column<string>(nullable: true),
                    ClanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorijaDolazaka", x => x.HistorijaDolazakaId);
                    table.ForeignKey(
                        name: "FK_HistorijaDolazaka_Clan_ClanId",
                        column: x => x.ClanId,
                        principalTable: "Clan",
                        principalColumn: "ClanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObjaveClanova",
                columns: table => new
                {
                    ObjaveClanovaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Sadrzaj = table.Column<string>(nullable: true),
                    DatumObjave = table.Column<DateTime>(nullable: false),
                    ClanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjaveClanova", x => x.ObjaveClanovaId);
                    table.ForeignKey(
                        name: "FK_ObjaveClanova_Clan_ClanId",
                        column: x => x.ClanId,
                        principalTable: "Clan",
                        principalColumn: "ClanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanTreninga",
                columns: table => new
                {
                    PlanTreningaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivVjezbe = table.Column<string>(nullable: false),
                    Trajanje = table.Column<int>(nullable: false),
                    BrojSerija = table.Column<int>(nullable: false),
                    BrojPonavljanja = table.Column<int>(nullable: false),
                    Opis = table.Column<string>(nullable: true),
                    ClanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanTreninga", x => x.PlanTreningaId);
                    table.ForeignKey(
                        name: "FK_PlanTreninga_Clan_ClanId",
                        column: x => x.ClanId,
                        principalTable: "Clan",
                        principalColumn: "ClanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TjelesniPodaci",
                columns: table => new
                {
                    TjelesniPodaciId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Visina = table.Column<float>(nullable: false),
                    Tezina = table.Column<float>(nullable: false),
                    Mjere = table.Column<string>(nullable: true),
                    ClanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TjelesniPodaci", x => x.TjelesniPodaciId);
                    table.ForeignKey(
                        name: "FK_TjelesniPodaci_Clan_ClanId",
                        column: x => x.ClanId,
                        principalTable: "Clan",
                        principalColumn: "ClanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UplataClanarine",
                columns: table => new
                {
                    UplataClanarineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Iznos = table.Column<decimal>(nullable: false),
                    DatumUplate = table.Column<DateTime>(nullable: false),
                    ClanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UplataClanarine", x => x.UplataClanarineId);
                    table.ForeignKey(
                        name: "FK_UplataClanarine_Clan_ClanId",
                        column: x => x.ClanId,
                        principalTable: "Clan",
                        principalColumn: "ClanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistorijaIzmjenaObavijesti",
                columns: table => new
                {
                    HistorijaIzmjenaObavijestiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumIzmjene = table.Column<DateTime>(nullable: false),
                    StariNaslov = table.Column<string>(nullable: true),
                    StariSadrzaj = table.Column<string>(nullable: true),
                    ObavijestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorijaIzmjenaObavijesti", x => x.HistorijaIzmjenaObavijestiId);
                    table.ForeignKey(
                        name: "FK_HistorijaIzmjenaObavijesti_Obavijest_ObavijestId",
                        column: x => x.ObavijestId,
                        principalTable: "Obavijest",
                        principalColumn: "ObavijestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ZaposlenikId = table.Column<int>(nullable: true),
                    TrenerId = table.Column<int>(nullable: true),
                    ClanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.KorisnikId);
                    table.ForeignKey(
                        name: "FK_Korisnik_Clan_ClanId",
                        column: x => x.ClanId,
                        principalTable: "Clan",
                        principalColumn: "ClanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Korisnik_Trener_TrenerId",
                        column: x => x.TrenerId,
                        principalTable: "Trener",
                        principalColumn: "TrenerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Korisnik_Zaposlenik_ZaposlenikId",
                        column: x => x.ZaposlenikId,
                        principalTable: "Zaposlenik",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LicniClanovi",
                columns: table => new
                {
                    LicniClanoviId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClanId = table.Column<int>(nullable: false),
                    TrenerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicniClanovi", x => x.LicniClanoviId);
                    table.ForeignKey(
                        name: "FK_LicniClanovi_Clan_ClanId",
                        column: x => x.ClanId,
                        principalTable: "Clan",
                        principalColumn: "ClanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LicniClanovi_Trener_TrenerId",
                        column: x => x.TrenerId,
                        principalTable: "Trener",
                        principalColumn: "TrenerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Termin",
                columns: table => new
                {
                    TerminId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumTermina = table.Column<DateTime>(nullable: false),
                    VrijemePocetak = table.Column<string>(nullable: false),
                    VrijemeKraj = table.Column<string>(nullable: false),
                    VrstaTreningaId = table.Column<int>(nullable: false),
                    SalaId = table.Column<int>(nullable: false),
                    TrenerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termin", x => x.TerminId);
                    table.ForeignKey(
                        name: "FK_Termin_Sala_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sala",
                        principalColumn: "SalaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Termin_Trener_TrenerId",
                        column: x => x.TrenerId,
                        principalTable: "Trener",
                        principalColumn: "TrenerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Termin_VrstaTreninga_VrstaTreningaId",
                        column: x => x.VrstaTreningaId,
                        principalTable: "VrstaTreninga",
                        principalColumn: "VrstaTreningaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KomentariObjavaClanova",
                columns: table => new
                {
                    KomentariObjavaClanovaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SadrzajKomentara = table.Column<string>(nullable: true),
                    DatumObjaveKomentara = table.Column<DateTime>(nullable: false),
                    ObjaveClanovaId = table.Column<int>(nullable: false),
                    ClanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KomentariObjavaClanova", x => x.KomentariObjavaClanovaId);
                    table.ForeignKey(
                        name: "FK_KomentariObjavaClanova_Clan_ClanId",
                        column: x => x.ClanId,
                        principalTable: "Clan",
                        principalColumn: "ClanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KomentariObjavaClanova_ObjaveClanova_ObjaveClanovaId",
                        column: x => x.ObjaveClanovaId,
                        principalTable: "ObjaveClanova",
                        principalColumn: "ObjaveClanovaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lajkovi",
                columns: table => new
                {
                    LajkoviId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ObjaveClanovaId = table.Column<int>(nullable: false),
                    ClanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lajkovi", x => x.LajkoviId);
                    table.ForeignKey(
                        name: "FK_Lajkovi_Clan_ClanId",
                        column: x => x.ClanId,
                        principalTable: "Clan",
                        principalColumn: "ClanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lajkovi_ObjaveClanova_ObjaveClanovaId",
                        column: x => x.ObjaveClanovaId,
                        principalTable: "ObjaveClanova",
                        principalColumn: "ObjaveClanovaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RezervacijaGrupnihTermina",
                columns: table => new
                {
                    RezervacijaGrupnihTerminaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClanId = table.Column<int>(nullable: false),
                    TerminId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezervacijaGrupnihTermina", x => x.RezervacijaGrupnihTerminaId);
                    table.ForeignKey(
                        name: "FK_RezervacijaGrupnihTermina_Clan_ClanId",
                        column: x => x.ClanId,
                        principalTable: "Clan",
                        principalColumn: "ClanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RezervacijaGrupnihTermina_Termin_TerminId",
                        column: x => x.TerminId,
                        principalTable: "Termin",
                        principalColumn: "TerminId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clan_GradId",
                table: "Clan",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Clan_VrstaClanarineId",
                table: "Clan",
                column: "VrstaClanarineId");

            migrationBuilder.CreateIndex(
                name: "IX_DropInInfo_ClanId",
                table: "DropInInfo",
                column: "ClanId");

            migrationBuilder.CreateIndex(
                name: "IX_Followers_ClanId",
                table: "Followers",
                column: "ClanId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorijaDolazaka_ClanId",
                table: "HistorijaDolazaka",
                column: "ClanId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorijaIzmjenaObavijesti_ObavijestId",
                table: "HistorijaIzmjenaObavijesti",
                column: "ObavijestId");

            migrationBuilder.CreateIndex(
                name: "IX_KomentariObjavaClanova_ClanId",
                table: "KomentariObjavaClanova",
                column: "ClanId");

            migrationBuilder.CreateIndex(
                name: "IX_KomentariObjavaClanova_ObjaveClanovaId",
                table: "KomentariObjavaClanova",
                column: "ObjaveClanovaId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_ClanId",
                table: "Korisnik",
                column: "ClanId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_TrenerId",
                table: "Korisnik",
                column: "TrenerId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_ZaposlenikId",
                table: "Korisnik",
                column: "ZaposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_Lajkovi_ClanId",
                table: "Lajkovi",
                column: "ClanId");

            migrationBuilder.CreateIndex(
                name: "IX_Lajkovi_ObjaveClanovaId",
                table: "Lajkovi",
                column: "ObjaveClanovaId");

            migrationBuilder.CreateIndex(
                name: "IX_LicniClanovi_ClanId",
                table: "LicniClanovi",
                column: "ClanId");

            migrationBuilder.CreateIndex(
                name: "IX_LicniClanovi_TrenerId",
                table: "LicniClanovi",
                column: "TrenerId");

            migrationBuilder.CreateIndex(
                name: "IX_Obavijest_ZaposlenikId",
                table: "Obavijest",
                column: "ZaposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjaveClanova_ClanId",
                table: "ObjaveClanova",
                column: "ClanId");

            migrationBuilder.CreateIndex(
                name: "IX_ObracunPlata_ObracunskiPeriodId",
                table: "ObracunPlata",
                column: "ObracunskiPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ObracunPlata_ZaposlenikId",
                table: "ObracunPlata",
                column: "ZaposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanTreninga_ClanId",
                table: "PlanTreninga",
                column: "ClanId");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijaGrupnihTermina_ClanId",
                table: "RezervacijaGrupnihTermina",
                column: "ClanId");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijaGrupnihTermina_TerminId",
                table: "RezervacijaGrupnihTermina",
                column: "TerminId");

            migrationBuilder.CreateIndex(
                name: "IX_Termin_SalaId",
                table: "Termin",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Termin_TrenerId",
                table: "Termin",
                column: "TrenerId");

            migrationBuilder.CreateIndex(
                name: "IX_Termin_VrstaTreningaId",
                table: "Termin",
                column: "VrstaTreningaId");

            migrationBuilder.CreateIndex(
                name: "IX_TjelesniPodaci_ClanId",
                table: "TjelesniPodaci",
                column: "ClanId");

            migrationBuilder.CreateIndex(
                name: "IX_Trener_ZaposlenikId",
                table: "Trener",
                column: "ZaposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_UplataClanarine_ClanId",
                table: "UplataClanarine",
                column: "ClanId");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenik_GradId",
                table: "Zaposlenik",
                column: "GradId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DropInInfo");

            migrationBuilder.DropTable(
                name: "Followers");

            migrationBuilder.DropTable(
                name: "HistorijaDolazaka");

            migrationBuilder.DropTable(
                name: "HistorijaIzmjenaObavijesti");

            migrationBuilder.DropTable(
                name: "KomentariObjavaClanova");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Lajkovi");

            migrationBuilder.DropTable(
                name: "LicniClanovi");

            migrationBuilder.DropTable(
                name: "ObracunPlata");

            migrationBuilder.DropTable(
                name: "PlanTreninga");

            migrationBuilder.DropTable(
                name: "RezervacijaGrupnihTermina");

            migrationBuilder.DropTable(
                name: "TjelesniPodaci");

            migrationBuilder.DropTable(
                name: "UplataClanarine");

            migrationBuilder.DropTable(
                name: "Obavijest");

            migrationBuilder.DropTable(
                name: "ObjaveClanova");

            migrationBuilder.DropTable(
                name: "ObracunskiPeriod");

            migrationBuilder.DropTable(
                name: "Termin");

            migrationBuilder.DropTable(
                name: "Clan");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Trener");

            migrationBuilder.DropTable(
                name: "VrstaTreninga");

            migrationBuilder.DropTable(
                name: "VrstaClanarine");

            migrationBuilder.DropTable(
                name: "Zaposlenik");

            migrationBuilder.DropTable(
                name: "Grad");
        }
    }
}
