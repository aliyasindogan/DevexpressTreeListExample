namespace DevexpressTreeListExample.Models
{
    using System.Data.Entity;

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()

        {
            Database.SetInitializer<DatabaseContext>(new CreateDatabaseIfNotExists<DatabaseContext>());
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<CategoryUserType> CategoryUserTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}