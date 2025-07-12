using CaloriasApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CaloriasApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Alimento> Alimentos => Set<Alimento>();
        public DbSet<Registro> Registros => Set<Registro>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alimento>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Alimentos)
                .HasForeignKey(a => a.UsuarioId);

            modelBuilder.Entity<Registro>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.Registros)
                .HasForeignKey(r => r.UsuarioId);

            modelBuilder.Entity<Registro>()
                .HasOne(r => r.Alimento)
                .WithMany()
                .HasForeignKey(r => r.AlimentoId);

            modelBuilder.Entity<Usuario>()
            .Property(u => u.RolUsuario)
            .HasConversion<string>();
        }
    }
}
