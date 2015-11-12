using System.Data.Entity;

namespace DaCoder.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultSQLite")
        {
        }

        public DbSet<TestModel> TestModels { get; set; }
    }
}