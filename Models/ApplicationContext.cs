using Microsoft.EntityFrameworkCore;

namespace PTLab2_Final.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Electronic> Electronics { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
