

using Microsoft.EntityFrameworkCore;
using PruebatecnicaCRUD.Core.Domain.Entities;
using System.Reflection;

namespace PruebatecnicaCRUD.Infrastructure.Persistence.Contexts
{

    public class PruebaAppContext(DbContextOptions<PruebaAppContext> options) : DbContext(options)
    {

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //Liskov-substitution
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}



