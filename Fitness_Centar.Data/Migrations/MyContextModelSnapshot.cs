﻿// <auto-generated />
using System;
using Fitness_Centar.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fitness_Centar.Data.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fitness_Centar.Data.Models.Clan", b =>
                {
                    b.Property<int>("ClanId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa");

                    b.Property<int>("BrojClanskeKartice");

                    b.Property<DateTime>("DatumRodjenja");

                    b.Property<DateTime>("DatumUclanjivanja");

                    b.Property<string>("Email");

                    b.Property<int>("GradId");

                    b.Property<string>("Ime")
                        .IsRequired();

                    b.Property<string>("JMBG")
                        .IsRequired();

                    b.Property<string>("KorisnickoIme")
                        .IsRequired();

                    b.Property<string>("Lozinka")
                        .IsRequired();

                    b.Property<string>("Prezime")
                        .IsRequired();

                    b.Property<string>("Spol");

                    b.Property<string>("Telefon")
                        .IsRequired();

                    b.Property<int>("VrstaClanarineId");

                    b.HasKey("ClanId");

                    b.HasIndex("GradId");

                    b.HasIndex("VrstaClanarineId");

                    b.ToTable("Clan");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.DropInInfo", b =>
                {
                    b.Property<int>("DropInInfoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojDolazaka");

                    b.Property<int>("ClanId");

                    b.Property<DateTime>("DatumIsteka");

                    b.Property<int>("PreostaliBrojDolazaka");

                    b.HasKey("DropInInfoId");

                    b.HasIndex("ClanId");

                    b.ToTable("DropInInfo");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Followers", b =>
                {
                    b.Property<int>("FollowersId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PratiteljClanId");

                    b.Property<int>("ZapraceniClanId");

                    b.HasKey("FollowersId");

                    b.HasIndex("PratiteljClanId");

                    b.HasIndex("ZapraceniClanId");

                    b.ToTable("Followers");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Grad", b =>
                {
                    b.Property<int>("GradId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired();

                    b.HasKey("GradId");

                    b.ToTable("Grad");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.HistorijaDolazaka", b =>
                {
                    b.Property<int>("HistorijaDolazakaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClanId");

                    b.Property<DateTime>("DatumDolaska");

                    b.Property<string>("VrijemeDolaska");

                    b.HasKey("HistorijaDolazakaId");

                    b.HasIndex("ClanId");

                    b.ToTable("HistorijaDolazaka");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.HistorijaIzmjenaObavijesti", b =>
                {
                    b.Property<int>("HistorijaIzmjenaObavijestiId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumIzmjene");

                    b.Property<int>("ObavijestId");

                    b.Property<string>("StariNaslov");

                    b.Property<string>("StariSadrzaj");

                    b.HasKey("HistorijaIzmjenaObavijestiId");

                    b.HasIndex("ObavijestId");

                    b.ToTable("HistorijaIzmjenaObavijesti");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.KomentariObjavaClanova", b =>
                {
                    b.Property<int>("KomentariObjavaClanovaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClanId");

                    b.Property<DateTime>("DatumObjaveKomentara");

                    b.Property<int>("ObjaveClanovaId");

                    b.Property<string>("SadrzajKomentara");

                    b.HasKey("KomentariObjavaClanovaId");

                    b.HasIndex("ClanId");

                    b.HasIndex("ObjaveClanovaId");

                    b.ToTable("KomentariObjavaClanova");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Korisnik", b =>
                {
                    b.Property<int>("KorisnikId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClanId");

                    b.Property<int?>("TrenerId");

                    b.Property<int?>("ZaposlenikId");

                    b.HasKey("KorisnikId");

                    b.HasIndex("ClanId");

                    b.HasIndex("TrenerId");

                    b.HasIndex("ZaposlenikId");

                    b.ToTable("Korisnik");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Lajkovi", b =>
                {
                    b.Property<int>("LajkoviId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClanId");

                    b.Property<int>("ObjaveClanovaId");

                    b.HasKey("LajkoviId");

                    b.HasIndex("ClanId");

                    b.HasIndex("ObjaveClanovaId");

                    b.ToTable("Lajkovi");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.LicniClanovi", b =>
                {
                    b.Property<int>("LicniClanoviId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClanId");

                    b.Property<int>("TrenerId");

                    b.HasKey("LicniClanoviId");

                    b.HasIndex("ClanId");

                    b.HasIndex("TrenerId");

                    b.ToTable("LicniClanovi");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Notifikacija", b =>
                {
                    b.Property<int>("NotifikacijaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DestClanId");

                    b.Property<int>("ObjavaId");

                    b.Property<bool>("Read");

                    b.Property<string>("Sadrzaj");

                    b.Property<bool>("Seen");

                    b.Property<int>("SourceClanId");

                    b.HasKey("NotifikacijaId");

                    b.ToTable("Notifikacija");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Obavijest", b =>
                {
                    b.Property<int>("ObavijestId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumObjave");

                    b.Property<string>("Naslov")
                        .IsRequired();

                    b.Property<string>("Sadrzaj")
                        .IsRequired();

                    b.Property<int>("ZaposlenikId");

                    b.HasKey("ObavijestId");

                    b.HasIndex("ZaposlenikId");

                    b.ToTable("Obavijest");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.ObjaveClanova", b =>
                {
                    b.Property<int>("ObjaveClanovaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClanId");

                    b.Property<DateTime>("DatumObjave");

                    b.Property<string>("Sadrzaj");

                    b.HasKey("ObjaveClanovaId");

                    b.HasIndex("ClanId");

                    b.ToTable("ObjaveClanova");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.ObracunPlata", b =>
                {
                    b.Property<int>("ObracunPlataId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojRadnihDana");

                    b.Property<float>("BrojRadnihSati");

                    b.Property<float>("IznosPlate");

                    b.Property<int>("ObracunskiPeriodId");

                    b.Property<float>("Satnica");

                    b.Property<int>("ZaposlenikId");

                    b.HasKey("ObracunPlataId");

                    b.HasIndex("ObracunskiPeriodId");

                    b.HasIndex("ZaposlenikId");

                    b.ToTable("ObracunPlata");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.ObracunskiPeriod", b =>
                {
                    b.Property<int>("ObracunskiPeriodId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired();

                    b.HasKey("ObracunskiPeriodId");

                    b.ToTable("ObracunskiPeriod");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.PlanTreninga", b =>
                {
                    b.Property<int>("PlanTreningaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojPonavljanja");

                    b.Property<int>("BrojSerija");

                    b.Property<int>("ClanId");

                    b.Property<string>("NazivVjezbe")
                        .IsRequired();

                    b.Property<string>("Opis");

                    b.Property<int>("Trajanje");

                    b.HasKey("PlanTreningaId");

                    b.HasIndex("ClanId");

                    b.ToTable("PlanTreninga");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.RezervacijaGrupnihTermina", b =>
                {
                    b.Property<int>("RezervacijaGrupnihTerminaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClanId");

                    b.Property<int>("TerminId");

                    b.HasKey("RezervacijaGrupnihTerminaId");

                    b.HasIndex("ClanId");

                    b.HasIndex("TerminId");

                    b.ToTable("RezervacijaGrupnihTermina");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Sala", b =>
                {
                    b.Property<int>("SalaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojMjesta");

                    b.Property<string>("Naziv")
                        .IsRequired();

                    b.HasKey("SalaId");

                    b.ToTable("Sala");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Termin", b =>
                {
                    b.Property<int>("TerminId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumTermina");

                    b.Property<int>("SalaId");

                    b.Property<int>("TrenerId");

                    b.Property<string>("VrijemeKraj")
                        .IsRequired();

                    b.Property<string>("VrijemePocetak")
                        .IsRequired();

                    b.Property<int>("VrstaTreningaId");

                    b.HasKey("TerminId");

                    b.HasIndex("SalaId");

                    b.HasIndex("TrenerId");

                    b.HasIndex("VrstaTreningaId");

                    b.ToTable("Termin");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.TjelesniPodaci", b =>
                {
                    b.Property<int>("TjelesniPodaciId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClanId");

                    b.Property<string>("Mjere");

                    b.Property<float>("Tezina");

                    b.Property<float>("Visina");

                    b.HasKey("TjelesniPodaciId");

                    b.HasIndex("ClanId");

                    b.ToTable("TjelesniPodaci");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Trener", b =>
                {
                    b.Property<int>("TrenerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GodineIskustva");

                    b.Property<int>("ZaposlenikId");

                    b.HasKey("TrenerId");

                    b.HasIndex("ZaposlenikId");

                    b.ToTable("Trener");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.UplataClanarine", b =>
                {
                    b.Property<int>("UplataClanarineId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClanId");

                    b.Property<DateTime>("DatumUplate");

                    b.Property<decimal>("Iznos");

                    b.HasKey("UplataClanarineId");

                    b.HasIndex("ClanId");

                    b.ToTable("UplataClanarine");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.VrstaClanarine", b =>
                {
                    b.Property<int>("VrstaClanarineId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cijena");

                    b.Property<string>("Naziv")
                        .IsRequired();

                    b.Property<string>("Opis");

                    b.HasKey("VrstaClanarineId");

                    b.ToTable("VrstaClanarine");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.VrstaTreninga", b =>
                {
                    b.Property<int>("VrstaTreningaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired();

                    b.HasKey("VrstaTreningaId");

                    b.ToTable("VrstaTreninga");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Zaposlenik", b =>
                {
                    b.Property<int>("ZaposlenikId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .IsRequired();

                    b.Property<DateTime>("DatumRodjenja");

                    b.Property<DateTime>("DatumZaposlenja");

                    b.Property<string>("Email");

                    b.Property<int>("GradId");

                    b.Property<string>("Ime")
                        .IsRequired();

                    b.Property<bool>("IsMenadzer");

                    b.Property<bool>("IsRecepcionar");

                    b.Property<bool>("IsTrener");

                    b.Property<string>("JMBG")
                        .IsRequired();

                    b.Property<string>("KorisnickoIme")
                        .IsRequired();

                    b.Property<string>("KrajRada")
                        .IsRequired();

                    b.Property<string>("Lozinka")
                        .IsRequired();

                    b.Property<string>("PocetakRada")
                        .IsRequired();

                    b.Property<string>("Prezime")
                        .IsRequired();

                    b.Property<string>("Spol");

                    b.Property<string>("Telefon")
                        .IsRequired();

                    b.HasKey("ZaposlenikId");

                    b.HasIndex("GradId");

                    b.ToTable("Zaposlenik");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Clan", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Fitness_Centar.Data.Models.VrstaClanarine", "VrstaClanarine")
                        .WithMany()
                        .HasForeignKey("VrstaClanarineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.DropInInfo", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Clan", "Clan")
                        .WithMany()
                        .HasForeignKey("ClanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Followers", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Clan", "PratiteljClan")
                        .WithMany()
                        .HasForeignKey("PratiteljClanId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Fitness_Centar.Data.Models.Clan", "ZapraceniClan")
                        .WithMany()
                        .HasForeignKey("ZapraceniClanId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.HistorijaDolazaka", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Clan", "Clan")
                        .WithMany()
                        .HasForeignKey("ClanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.HistorijaIzmjenaObavijesti", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Obavijest", "Obavijest")
                        .WithMany()
                        .HasForeignKey("ObavijestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.KomentariObjavaClanova", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Clan", "Clan")
                        .WithMany()
                        .HasForeignKey("ClanId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fitness_Centar.Data.Models.ObjaveClanova", "ObjaveClanova")
                        .WithMany()
                        .HasForeignKey("ObjaveClanovaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Korisnik", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Clan", "Clan")
                        .WithMany()
                        .HasForeignKey("ClanId");

                    b.HasOne("Fitness_Centar.Data.Models.Trener", "Trener")
                        .WithMany()
                        .HasForeignKey("TrenerId");

                    b.HasOne("Fitness_Centar.Data.Models.Zaposlenik", "Zaposlenik")
                        .WithMany()
                        .HasForeignKey("ZaposlenikId");
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Lajkovi", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Clan", "Clan")
                        .WithMany()
                        .HasForeignKey("ClanId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Fitness_Centar.Data.Models.ObjaveClanova", "ObjaveClanova")
                        .WithMany()
                        .HasForeignKey("ObjaveClanovaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.LicniClanovi", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Clan", "Clan")
                        .WithMany()
                        .HasForeignKey("ClanId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fitness_Centar.Data.Models.Trener", "Trener")
                        .WithMany()
                        .HasForeignKey("TrenerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Obavijest", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Zaposlenik", "Zaposlenik")
                        .WithMany()
                        .HasForeignKey("ZaposlenikId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.ObjaveClanova", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Clan", "Clan")
                        .WithMany()
                        .HasForeignKey("ClanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.ObracunPlata", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.ObracunskiPeriod", "ObracunskiPeriod")
                        .WithMany()
                        .HasForeignKey("ObracunskiPeriodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fitness_Centar.Data.Models.Zaposlenik", "Zaposlenik")
                        .WithMany()
                        .HasForeignKey("ZaposlenikId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.PlanTreninga", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Clan", "Clan")
                        .WithMany()
                        .HasForeignKey("ClanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.RezervacijaGrupnihTermina", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Clan", "Clan")
                        .WithMany()
                        .HasForeignKey("ClanId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fitness_Centar.Data.Models.Termin", "Termin")
                        .WithMany()
                        .HasForeignKey("TerminId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Termin", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Sala", "Sala")
                        .WithMany()
                        .HasForeignKey("SalaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fitness_Centar.Data.Models.Trener", "Trener")
                        .WithMany()
                        .HasForeignKey("TrenerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fitness_Centar.Data.Models.VrstaTreninga", "VrstaTreninga")
                        .WithMany()
                        .HasForeignKey("VrstaTreningaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.TjelesniPodaci", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Clan", "Clan")
                        .WithMany()
                        .HasForeignKey("ClanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Trener", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Zaposlenik", "Zaposlenik")
                        .WithMany()
                        .HasForeignKey("ZaposlenikId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.UplataClanarine", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Clan", "Clan")
                        .WithMany()
                        .HasForeignKey("ClanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fitness_Centar.Data.Models.Zaposlenik", b =>
                {
                    b.HasOne("Fitness_Centar.Data.Models.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
