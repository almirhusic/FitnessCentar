using Fitness_Centar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace Fitness_Centar.Data
{
    public class MyContext : DbContext
    {

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            
        }

        public DbSet<Clan> Clanovi { get; set; }
        public DbSet<DropInInfo> DropInInfo { get; set; }
        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<HistorijaDolazaka> HistorijaDolazaka { get; set; }
        public DbSet<HistorijaIzmjenaObavijesti> HistorijaIzmjenaObavijesti { get; set; }
        public DbSet<LicniClanovi> LicniClanovi { get; set; }
        public DbSet<Obavijest> Obavijesti { get; set; }
        public DbSet<ObracunPlata> ObracunPlata { get; set; }
        public DbSet<ObracunskiPeriod> ObracunskiPeriod { get; set; }
        public DbSet<PlanTreninga> PlanTreninga { get; set; }
        public DbSet<RezervacijaGrupnihTermina> RezervacijeGrupnihTermina { get; set; }
        public DbSet<Sala> Sale { get; set; }
        public DbSet<Termin> Termini { get; set; }
        public DbSet<TjelesniPodaci> TjelesniPodaci { get; set; }
        public DbSet<Trener> Treneri { get; set; }
        public DbSet<UplataClanarine> UplataClanarine { get; set; }
        public DbSet<VrstaClanarine> VrstaClanarine { get; set; }
        public DbSet<VrstaTreninga> VrstaTreninga { get; set; }
        public DbSet<Zaposlenik> Zaposlenici { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<ObjaveClanova> ObjaveClanova { get; set; }
        public DbSet<Followers> Followeri { get; set; }
        public DbSet<Lajkovi> Lajkovi { get; set; }
        public DbSet<KomentariObjavaClanova> KomentariObjavaClanova { get; set; }
        public DbSet<Notifikacija> Notifikacije { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.Relational().TableName = entityType.DisplayName();
            }

            modelBuilder.Entity<Clan>().HasOne(pt => pt.Grad).WithMany()
                        .HasForeignKey(pt => pt.GradId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KomentariObjavaClanova>().HasOne(k => k.ObjaveClanova).WithMany()
                        .HasForeignKey(x => x.ObjaveClanovaId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lajkovi>().HasOne(l => l.Clan).WithMany()
                        .HasForeignKey(l => l.ClanId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lajkovi>().HasOne(l => l.ObjaveClanova).WithMany()
                        .HasForeignKey(l => l.ObjaveClanovaId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Followers>().HasOne(f => f.PratiteljClan).WithMany()
                        .HasForeignKey(l => l.PratiteljClanId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Followers>().HasOne(f => f.ZapraceniClan).WithMany()
                        .HasForeignKey(l => l.ZapraceniClanId).OnDelete(DeleteBehavior.Restrict);


            //modelBuilder.Entity<Trener>().HasOne(pt => pt.Zaposlenik).WithMany()
            //            .HasForeignKey(pt => pt.ZaposlenikId).OnDelete(DeleteBehavior.Restrict);

            //Required and Optional Relationships
            //modelBuilder.Entity<Post>()
            //.HasOne(p => p.Blog)
            //.WithMany(b => b.Posts)
            //.IsRequired();

            //One-to-one
            //modelBuilder.Entity<Blog>()
            //.HasOne(p => p.BlogImage)
            //.WithOne(i => i.Blog)
            //.HasForeignKey<BlogImage>(b => b.BlogForeignKey);

            //Many-to-many
            //modelBuilder.Entity<PostTag>()
            //.HasKey(t => new { t.PostId, t.TagId });

            //modelBuilder.Entity<PostTag>()
            //    .HasOne(pt => pt.Post)
            //    .WithMany(p => p.PostTags)
            //    .HasForeignKey(pt => pt.PostId);

            //modelBuilder.Entity<PostTag>()
            //    .HasOne(pt => pt.Tag)
            //    .WithMany(t => t.PostTags)
            //    .HasForeignKey(pt => pt.TagId);
        }
    }
}
