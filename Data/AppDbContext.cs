using Microsoft.EntityFrameworkCore;
namespace CollegeManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student { id = 1, name = "manju", email = "manju@gmail.com" },
                new Student { id = 2, name = "akash", email = "akash@gmail.com" },
                new Student { id = 3, name = "ram", email = "ram@gmail.com" }
            );

            modelBuilder.Entity<Student>().HasKey(s => s.id);
            modelBuilder.Entity<Student>().Property(s => s.id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Student>().Property(s => s.name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Student>().Property(s => s.email).IsRequired(false).HasMaxLength(50);
            
        }
    }
}
