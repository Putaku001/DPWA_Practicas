using Microsoft.EntityFrameworkCore;
using practica11.Models;

namespace practica11.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<RoleModel> RoleModel { get; set; }
    }
}
