using System.Data.Entity;

namespace DaCoder.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultSQLite")
        {
        }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Keyword> Keywords { get; set; }
    }
}