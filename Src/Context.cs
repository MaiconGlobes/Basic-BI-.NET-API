using ASUpdateServices.Src.Utils;
using BaseCodeAPI.Src.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace ASUpdateServices.Src
{
    internal class Context : DbContext
    {
        public DbSet<UserModel> PARAMETRO { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($@"Data Source=C:\ProgramData\Vexor\AutoSmart\Database\Debug\database.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserModel>().Property(parametro => parametro.Name).IsRequired();

            modelBuilder.Entity<UserModel>().HasData(PopularDadosUtils.Instancia().PopularParametros());
        }
    }
}
