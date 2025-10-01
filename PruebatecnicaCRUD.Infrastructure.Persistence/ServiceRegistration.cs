using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebatecnicaCRUD.Core.Domain.Interfaces;
using PruebatecnicaCRUD.Infrastructure.Persistence.Contexts;
using PruebatecnicaCRUD.Infrastructure.Persistence.Repositories;

namespace PruebatecnicaCRUD.Infrastructure.Persistence
{
    public static class ServicesRegistration
    {
        //Extension method - Decorator pattern
        public static void AddPersistenceLayerIoc(this IServiceCollection services, IConfiguration config)
        {
            #region Contexts
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<PruebaAppContext>(opt =>
                                              opt.UseInMemoryDatabase("AppDb"));
            }
            else
            {
                var connectionString = config.GetConnectionString("DefaultConnection");
                services.AddDbContext<PruebaAppContext>(opt =>
                opt.UseSqlServer(connectionString,
                m => m.MigrationsAssembly(typeof(PruebaAppContext).Assembly.FullName))
                , ServiceLifetime.Transient); // ServiceLifetime.Transient: para evitar cuello de botella
            }
            #endregion

            #region Repositories IOC
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<ILoanRepository, LoanRepository>();

            #endregion
        }
    }
}
