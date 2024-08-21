
namespace App_Api_Jwt_Training.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
		public DbSet<Department>departments { get; set; }
		public DbSet<Student>students { get; set; }
    }
}
