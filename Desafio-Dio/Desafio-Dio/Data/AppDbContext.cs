using Desafio_Dio.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Dio.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Funcionario> Funcionarios {  get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Funcionario>()
                .Property(p => p.Salario)
                .HasPrecision(18, 2);
        }


    }
}
