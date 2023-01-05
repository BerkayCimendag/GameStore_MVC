using Microsoft.EntityFrameworkCore;

namespace GameStore_MVC.data
{
    public class UygulamaDbContext:DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext>options):base(options)
        {

        }

        public DbSet<Game> Games => Set<Game>();

    }
}
