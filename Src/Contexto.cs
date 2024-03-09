using ASUpdateServices.Src.Utils;
using BaseCodeAPI.Src.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace ASUpdateServices.Src
{
    internal class Contexto : DbContext
    {
        public DbSet<ParametroModel> PARAMETRO { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($@"Data Source=C:\ProgramData\Vexor\AutoSmart\Database\Debug\database.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ParametroModel>().Property(parametro => parametro.Telefone_suporte).IsRequired();

            modelBuilder.Entity<ParametroModel>().HasData(PopularDadosUtils.Instancia().PopularParametros());
        }
    }
}
