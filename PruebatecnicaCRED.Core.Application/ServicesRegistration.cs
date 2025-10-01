using Microsoft.Extensions.DependencyInjection;
using PruebatecnicaCRUD.Core.Application.Interfaces;
using PruebatecnicaCRUD.Core.Application.Services;


namespace PruebatecnicaCRUD.Core.Application
{
    public static class ServicesRegistration
    {
        //Extension method - Decorator pattern
        public static void AddApplicationLayerIoc(this IServiceCollection services)
        {
            #region Services IOC
            
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<ILoanService, LoanService>();
      
            #endregion
        }
    }
}
