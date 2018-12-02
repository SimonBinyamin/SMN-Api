using SMN.Data.DBModels;
using Microsoft.EntityFrameworkCore;
using SMN.Data.Data;

namespace SMN.Data
{
    public class SMNContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<TopicCategory> TopicCategory { get; set; }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<Browse> Browse { get; set; }
        public DbSet<Cellphone> Cellphone { get; set; }
        public DbSet<TopicCellphone> TopicCellphone { get; set; }
        public DbSet<TopicRole> TopicRole { get; set; }
        public DbSet<Keyword> Keyword { get; set; }
        public DbSet<TopicKeyword> TopicKeyword { get; set; }


        private string ConnectionString { get; set; }

        public SMNContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.UserRoleId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
            });

            modelBuilder.Entity<TopicCategory>(entity =>
            {
                entity.HasKey(e => e.TopicCategoryId);
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.HasKey(e => e.TopicId);
            });

            modelBuilder.Entity<Browse>(entity =>
            {
                entity.HasKey(e => e.BrowseId);
            });

            modelBuilder.Entity<Cellphone>(entity =>
            {
                entity.HasKey(e => e.CellphoneId);
            });

            modelBuilder.Entity<TopicCellphone>(entity =>
            {
                entity.HasKey(e => e.TopicCellphoneId);
            });

            modelBuilder.Entity<TopicRole>(entity =>
            {
                entity.HasKey(e => e.TopicRoleId);
            });

            modelBuilder.Entity<Keyword>(entity =>
            {
                entity.HasKey(e => e.KeywordId);
            });

            modelBuilder.Entity<TopicKeyword>(entity =>
            {
                entity.HasKey(e => e.TopicKeywordId);
            });
        }

        public void EnsureDatabaseCreated()
        {
            if (this.Database.EnsureCreated())
            {
                this.SaveChanges();
                AddInitialData();
            }
        }

        private void AddInitialData()
        {
            this.InsertCategorysData();
            this.InsertUsersData();
            this.InsertRolesData();
            this.InsertUserrolesData();
            this.InsertBrowseValues();
            this.InsertCellphoneData();
        }
    }
}
