using Microsoft.EntityFrameworkCore;
using PacientesApi.Models;

namespace PacientesApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) 
        {
            // Garante que o banco seja criado ao instanciar o contexto
            Database.EnsureCreated();
        }

        public DbSet<Paciente> Pacientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Paciente>(entity =>
            {
                // Configuração do índice único para CPF
                entity.HasIndex(p => p.CPF).IsUnique();
                
                // Configurações das propriedades
                entity.Property(p => p.Nome)
                    .IsRequired()
                    .HasMaxLength(100);
                    
                entity.Property(p => p.CPF)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsFixedLength(); // Garante exatamente 11 caracteres
                    
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