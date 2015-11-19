using System.Data.Entity;

namespace DaCoder.Data
{
    /// <summary>
    /// Encapsulates the SQLite database access.
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// This class will use a default named connectionstring of 'DefaultSQLite' from the application's configuration file ("App.config").
        /// </summary>
        public DataContext() : base("DefaultSQLite")
        {
        }

        /// <summary>
        /// Gets a collection of language entities.
        /// </summary>
        public DbSet<Language> Languages { get; set; }

        /// <summary>
        /// Gets a collection of Keywords entities.
        /// </summary>
        public DbSet<Keyword> Keywords { get; set; }
    }
}