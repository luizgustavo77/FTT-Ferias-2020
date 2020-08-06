using Apresentacao.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Apresentacao.Entities.Data
{
    public class DBContext : DbContext
    {
        public DbSet<Login> Logins { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(new appSettings().Get("conexao"));
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Pessoa>()
        //                .HasMany(c => c.projetos)
        //                .WithOne(u => u.idgerente)
        //                .HasForeignKey(s => s.id);
        //    modelBuilder.Entity<Membros>()
        //                .HasKey(x => new { x.idpessoa, x.idprojeto });
        //}
    }
}
