using Microsoft.EntityFrameworkCore;
using WorkerList.Model;

namespace WorkerList.Data
{
    public class AppContext : DbContext
    {
        public DbSet<ModelPerson> Persons { get; set; }
        public AppContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source =88.85.208.208; Initial Catalog = DBTest; User Id = gcc5L01tW; Password = OrH5JcdxP3WQ;");
        }
    }
}
