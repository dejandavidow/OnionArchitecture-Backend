
using Microsoft.EntityFrameworkCore;
using Persistence.Models;
namespace Persistence
{
    public sealed class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<PersistenceCategory> Categories {get;set;}
        public DbSet<PersistenceClient> Clients{get;set;}
        public DbSet<PersistenceMember> Members {get;set;}
        public DbSet<PersistenceProject> Projects{get;set;}
        public DbSet<PersistenceTimeSheet> TimeSheets{get;set;}
        }
}
