using Microsoft.EntityFrameworkCore;

namespace Absalyamov_WEB2.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User>Users { get; set; }
        public DbSet<PlayerCard> PlayerCards { get; set; }
        public DbSet<UserCardRelationship> UserCardRelationships { get; set; }
        public DbSet<Rating>Ratings { get; set; }
    }
}
