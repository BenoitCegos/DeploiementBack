using System;
using System.Collections.Generic;
using BDDNET.Controllers;
using BDDNET.Models;
using Microsoft.EntityFrameworkCore;

namespace BDDNET.Models;

//creation de ma class BiblioContext
public partial class BiblioContext : DbContext
{
    public BiblioContext()
    {
    }

    public BiblioContext(DbContextOptions<BiblioContext> options)
        : base(options)
    {
    }

    //Creation de mes deux DBSET Auteurs et Livres pour mes deux tables Auteur et Livre
    public virtual DbSet<Auteur> Auteurs { get; set; }

    public virtual DbSet<Livre> Livres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:monserveursql2.database.windows.net,1433;Initial Catalog=bddTest;Persist Security Info=False;User ID=bendufBDDSQLServer;Password=Zorglub12!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auteur>(entity =>
        {
            entity.HasKey(e => e.IdAuteur);

            entity.Property(e => e.IdAuteur).HasColumnName("id_auteur");
            entity.Property(e => e.NomAuteur)
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("nom_auteur");
            entity.Property(e => e.PrenomAuteur)
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("prenom_auteur");
        });

        modelBuilder.Entity<Livre>(entity =>
        {
            entity.HasKey(e => e.IdLivre);

            entity.Property(e => e.IdLivre).HasColumnName("id_livre");
            entity.Property(e => e.IdAuteur).HasColumnName("id_auteur");
            entity.Property(e => e.ResumerLivre)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("resumer_livre");
            entity.Property(e => e.TitreLivre)
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("titre_livre");

            entity.HasOne(d => d.IdAuteurNavigation).WithMany(p => p.Livres)
                .HasForeignKey(d => d.IdAuteur)
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
