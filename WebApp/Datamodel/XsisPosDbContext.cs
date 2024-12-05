using Microsoft.EntityFrameworkCore;

namespace WebApp.Datamodel
{
    public class XsisPosDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();

            modelBuilder.Entity<Category>(ent =>
            {
                ent.HasIndex(c => c.Initial).IsUnique();
                ent.Property(c => c.Initial).HasMaxLength(10);

                ent.HasIndex(c => c.Name).IsUnique();
                ent.Property(c => c.Name).HasMaxLength(50);

                ent.Property(c => c.Description).HasMaxLength(250);
            });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            IConfigurationRoot builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(builder.GetConnectionString("XsisPosConn"));
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasData(
                    new Category() { Id = 1, Initial = "Food", Name = "Fresh Food", Description = "Fresh from the oven" },
                    new Category() { Id = 2, Initial = "Drink", Name = "Fresh Drink", Description = "Healhty drink" }
                );
        }
    }
}
