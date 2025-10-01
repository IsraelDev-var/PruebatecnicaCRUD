
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebatecnicaCRUD.Core.Domain.Entities;

namespace PruebatecnicaCRUD.Infrastructure.Persistence.EntityConfigurations
{
    public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            //Fluent api
            #region Basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Author");
            #endregion

            #region Property configuratiopn
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Nationality).IsRequired();
            #endregion

            #region
            builder.HasMany<Book>(b => b.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

        }
    }
}
