using Microsoft.EntityFrameworkCore;
using practica01.Models;

namespace practica01.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<StaffModel> staffModel { get; set; }
        public DbSet<StaffCategoryModel> staffCategoryModel { get; set; }
        public DbSet<SpecialtyModel> specialtyModel { get; set; }
        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<RoleModel> RoleModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StaffModel>()
                .HasOne(s => s.Specialty)
                .WithMany(sp => sp.StaffMembers)
                .HasForeignKey(s => s.SpecialtyId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<RoleModel>().HasData(
                new RoleModel { Id = 1, Name = "admin", Description = "Administrador del sistema" },
                new RoleModel { Id = 2, Name = "user", Description = "Usuario general" }
            );

            modelBuilder.Entity<UserModel>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
