using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaVidaTecnologia.Models
{
    public class NovaVidaContext : DbContext
    {
        public NovaVidaContext(DbContextOptions<NovaVidaContext> options) : base(options) { }
        public DbSet<Professores> Professores { get; set; }
        public DbSet<Alunos> Alunos { get; set; }
        public DbSet<LogImportacao> LogImportacao { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Professores>(entity =>
            {
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CriadoEm)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Alunos>(entity =>
            {
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PagamentoMensal)
                    .IsRequired()
                    .HasColumnType("decimal(18,6)");

                entity.Property(e => e.DataVencimento)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CriadoEm)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasIndex("ProfessorId");


            });

            modelBuilder.Entity<LogImportacao>(entity =>
            {
                entity.Property(e => e.NomeArquivo)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);


                entity.Property(e => e.DataImportacao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });
        }
    }
}
