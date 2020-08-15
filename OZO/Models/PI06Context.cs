using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OZO.Models
{
    public partial class PI06Context : DbContext
    {

        public PI06Context(DbContextOptions<PI06Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Certifikat> Certifikat { get; set; }
        public virtual DbSet<CertifikatZaposlenik> CertifikatZaposlenik { get; set; }
        public virtual DbSet<NatjecajReferentniTip> NatjecajReferentniTip { get; set; }
        public virtual DbSet<Natječaj> Natječaj { get; set; }
        public virtual DbSet<Oprema> Oprema { get; set; }
        public virtual DbSet<Posao> Posao { get; set; }
        public virtual DbSet<PosaoIzvjestaj> PosaoIzvjestaj { get; set; }
        public virtual DbSet<PosaoOprema> PosaoOprema { get; set; }
        public virtual DbSet<ReferentniTip> ReferentniTip { get; set; }
        public virtual DbSet<Usluga> Usluga { get; set; }
        public virtual DbSet<UslugaReferentniTip> UslugaReferentniTip { get; set; }
        public virtual DbSet<Zaposlenik> Zaposlenik { get; set; }
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Certifikat>(entity =>
            {
                entity.ToTable("CERTIFIKAT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasColumnName("Naziv")
                    .HasMaxLength(50)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<CertifikatZaposlenik>(entity =>
            {
                entity.ToTable("CERTIFIKAT_ZAPOSLENIK");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdCertifikat).HasColumnName("ID_Certifikat");

                entity.Property(e => e.IdZaposlenik).HasColumnName("ID_Zaposlenik");

                entity.HasOne(d => d.IdCertifikatNavigation)
                    .WithMany(p => p.CertifikatZaposlenik)
                    .HasForeignKey(d => d.IdCertifikat)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CERTIFIKAT_ZAPOSLENIK_CERTIFIKAT");

                entity.HasOne(d => d.IdZaposlenikNavigation)
                    .WithMany(p => p.CertifikatZaposlenik)
                    .HasForeignKey(d => d.IdZaposlenik)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CERTIFIKAT_ZAPOSLENIK_ZAPOSLENIK");
            });

            modelBuilder.Entity<NatjecajReferentniTip>(entity =>
            {
                entity.ToTable("NATJECAJ_REFERENTNI_TIP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdNatjecaj).HasColumnName("ID_Natjecaj");

                entity.Property(e => e.IdReferentniTip).HasColumnName("ID_Referentni_Tip");

                entity.HasOne(d => d.IdNatjecajNavigation)
                    .WithMany(p => p.NatjecajReferentniTip)
                    .HasForeignKey(d => d.IdNatjecaj)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_NATJECAJ_REFERENTNI_TIP_NATJEČAJ");

                entity.HasOne(d => d.IdReferentniTipNavigation)
                    .WithMany(p => p.NatjecajReferentniTip)
                    .HasForeignKey(d => d.IdReferentniTip)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_NATJECAJ_REFERENTNI_TIP_REFERENTNI_TIP");
            });

            modelBuilder.Entity<Natječaj>(entity =>
            {
                entity.ToTable("NATJEČAJ");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Kontakt)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lokacija)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Opis)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Poslodavac)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Oprema>(entity =>
            {
                entity.ToTable("OPREMA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdReferentniTip).HasColumnName("ID_Referentni_Tip");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdReferentniTipNavigation)
                    .WithMany(p => p.Oprema)
                    .HasForeignKey(d => d.IdReferentniTip)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_OPREMA_REFERENTNI_TIP");
            });

          

            modelBuilder.Entity<Posao>(entity =>
            {
                entity.ToTable("POSAO");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdNatjecaj).HasColumnName("ID_Natjecaj");

                entity.Property(e => e.IdUsluga).HasColumnName("ID_Usluga");

                entity.Property(e => e.Klijent)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Kontakt)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lokacija)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Opis)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdNatjecajNavigation)
                    .WithMany(p => p.Posao)
                    .HasForeignKey(d => d.IdNatjecaj)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_POSAO_NATJEČAJ");

                entity.HasOne(d => d.IdUslugaNavigation)
                    .WithMany(p => p.Posao)
                    .HasForeignKey(d => d.IdUsluga)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_POSAO_USLUGA");
            });

            modelBuilder.Entity<PosaoIzvjestaj>(entity =>
            {
                entity.ToTable("POSAO_IZVJESTAJ");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdPosao).HasColumnName("ID_Posao");

                entity.HasOne(d => d.IdPosaoNavigation)
                    .WithMany(p => p.PosaoIzvjestaj)
                    .HasForeignKey(d => d.IdPosao)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_POSAO_IZVJESTAJ_POSAO");
            });

            modelBuilder.Entity<PosaoOprema>(entity =>
            {
                entity.ToTable("POSAO_OPREMA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdOprema).HasColumnName("ID_Oprema");

                entity.Property(e => e.IdPosao).HasColumnName("ID_Posao");

                entity.HasOne(d => d.IdOpremaNavigation)
                    .WithMany(p => p.PosaoOprema)
                    .HasForeignKey(d => d.IdOprema)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_POSAO_OPREMA_OPREMA");

                entity.HasOne(d => d.IdPosaoNavigation)
                    .WithMany(p => p.PosaoOprema)
                    .HasForeignKey(d => d.IdPosao)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_POSAO_OPREMA_POSAO");
            });

            modelBuilder.Entity<ReferentniTip>(entity =>
            {
                entity.ToTable("REFERENTNI_TIP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usluga>(entity =>
            {
                entity.ToTable("USLUGA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Klijent)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Kontakt)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lokacija)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Opis)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UslugaReferentniTip>(entity =>
            {
                entity.ToTable("USLUGA_REFERENTNI_TIP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdReferentniTip).HasColumnName("ID_Referentni_Tip");

                entity.Property(e => e.IdUsluga).HasColumnName("ID_Usluga");

                entity.HasOne(d => d.IdReferentniTipNavigation)
                    .WithMany(p => p.UslugaReferentniTip)
                    .HasForeignKey(d => d.IdReferentniTip)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_USLUGA_REFERENTNI_TIP_REFERENTNI_TIP");

                entity.HasOne(d => d.IdUslugaNavigation)
                    .WithMany(p => p.UslugaReferentniTip)
                    .HasForeignKey(d => d.IdUsluga)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_USLUGA_REFERENTNI_TIP_USLUGA");
            });

            modelBuilder.Entity<Zaposlenik>(entity =>
            {
                entity.ToTable("ZAPOSLENIK");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Datum).HasColumnType("date");

                entity.Property(e => e.IdPosao).HasColumnName("ID_Posao");

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NazivŠkole)
               .HasColumnName("Naziv_Škole")
               .HasMaxLength(50)
               .IsUnicode(false);

                entity.Property(e => e.StručnaSprema)
                    .IsRequired()
                    .HasColumnName("Stručna_Sprema")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPosaoNavigation)
                    .WithMany(p => p.Zaposlenik)
                    .HasForeignKey(d => d.IdPosao)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ZAPOSLENIK_POSAO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
