using Microsoft.EntityFrameworkCore;
using PacientesApi.Models;

namespace PacientesApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<Paciente> Pacientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Paciente>(entity =>
            {

                entity.HasIndex(p => p.CPF).IsUnique();
                entity.Property(p => p.Nome)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(p => p.CPF)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsFixedLength(); 
                entity.Property(p => p.Email)
                    .HasMaxLength(100);
                entity.Property(p => p.Telefone)
                    .HasMaxLength(20);
                entity.Property(p => p.TipoSanguineo)
                    .HasMaxLength(3);
            });
        }
    }
}
