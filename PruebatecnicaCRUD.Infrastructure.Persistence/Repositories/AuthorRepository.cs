

using PruebatecnicaCRUD.Core.Domain.Entities;
using PruebatecnicaCRUD.Core.Domain.Interfaces;
using PruebatecnicaCRUD.Infrastructure.Persistence.Contexts;

namespace PruebatecnicaCRUD.Infrastructure.Persistence.Repositories
{
    public class AuthorRepository(PruebaAppContext 
        context) : GenericRepository<Author>(context), IAuthorRepository
    {
    }
}
