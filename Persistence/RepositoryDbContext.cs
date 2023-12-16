using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Persistence
{
    public sealed class RepositoryDbContext : IdentityDbContext<User>
    {
        public RepositoryDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
            .HasOne<User>().WithMany().HasForeignKey(user => user.UserId);

            modelBuilder.Entity<Category>().HasData(
                new Category() { Id=1, Name="Frontend Development" },
                new Category() { Id=2, Name="Backend Development" },
                new Category() { Id=3, Name="Angular Development" },
                new Category() { Id=4, Name=".Net Development" },
                new Category() { Id=5, Name="Java Development" },
                new Category() { Id=6, Name="JavaScript Development" },
                new Category() { Id=7, Name="Spring Boot Development" },
                new Category() { Id=8, Name="Python Development" },
                new Category() { Id=9, Name="PHP Development" },
                new Category() { Id=10, Name="C++ Development" }
                );
            modelBuilder.Entity<Client>().HasData(
                new Client() { Id = 1, Name = "Softcom", Adress = "Bulevar Oslobodjenja 113", City = "Beograd", Country="Srbija", PostalCode="11000" },
                new Client() { Id = 2, Name = "Greenstate Development", Adress = "Mihajla Pupina 22", City = "Beograd", Country="Srbija", PostalCode="11000" },
                new Client() { Id = 3, Name = "Livonia", Adress = "Kosovksa 135", City = "Novi Sad", Country="Srbija", PostalCode="21000" }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
