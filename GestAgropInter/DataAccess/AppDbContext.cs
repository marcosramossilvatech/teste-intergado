using GestAgropInter.Models;
using Microsoft.EntityFrameworkCore;

namespace GestAgropInter.DataAccess
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Fazenda> Fazenda { get; set; }
        public DbSet<Animal> Animal { get; set; }

    }
}
