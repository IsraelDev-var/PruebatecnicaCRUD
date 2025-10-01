
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebatecnicaCRUD.Core.Domain.Entities;

namespace PruebatecnicaCRUD.Infrastructure.Persistence.EntityConfigurations
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //Fluent api
            #region Basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Book");
            #endregion

            #region Property configuratiopn
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.YearPublished).IsRequired();

            #endregion

            #region
            builder.HasMany<Loan>(b => b.Loans)
                .WithOne(b => b.Book)
                .HasForeignKey(b => b.BookId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion



        }
    }
}
