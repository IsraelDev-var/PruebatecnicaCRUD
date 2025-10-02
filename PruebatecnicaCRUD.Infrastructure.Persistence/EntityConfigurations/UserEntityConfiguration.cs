using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebatecnicaCRUD.Core.Domain.Entities;

namespace PruebatecnicaCRUD.Infrastructure.Persistence.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Fluent api
            #region Basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Users");
            #endregion

        }
    }
}
