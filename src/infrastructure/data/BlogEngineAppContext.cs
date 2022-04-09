using BlogEngineApp.core.entities;
using BlogEngineApp.core.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BlogEngineApp.infrastructure.data
{
    public class BlogEngineAppContext : DbContext

    {
        private IDbContextTransaction transactionContext;

        public BlogEngineAppContext(DbContextOptions<BlogEngineAppContext> options) : base(options) { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasIndex(b => b.Status);

            modelBuilder.Entity<Blog>()
                .Property(b => b.Status)
                .HasConversion<string>()
                .HasColumnType("char(20)");

            modelBuilder.Entity<User>()
                .Property(b => b.Role)
                .HasConversion<string>()
                .HasColumnType("char(20)");

            LoadInitialData(modelBuilder);
        }

        public virtual void BeginTransaction()
        {
            transactionContext = Database.BeginTransaction();
        }

        public virtual void CommitTransaction()
        {
            try
            {
                SaveChanges();
                transactionContext.Commit();
            }
            finally
            {
                transactionContext.Dispose();
            }
        }

        public virtual void RollbackTransaction()
        {
            if (transactionContext != null)
            {
                transactionContext.Rollback();
                transactionContext.Dispose();
            }
        }

        private static void LoadInitialData(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserSeed());
        }

    }
}