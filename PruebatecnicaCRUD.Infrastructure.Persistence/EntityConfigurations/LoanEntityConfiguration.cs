
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebatecnicaCRUD.Core.Domain.Entities;

namespace PruebatecnicaCRUD.Infrastructure.Persistence.EntityConfigurations
{
    public class LoanEntityConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            //Fluent api
            #region Basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Loan");

            #endregion

            #region Property configuratiopn
            builder.Property(x => x.LoanDate).IsRequired();
            // indices
            builder.HasIndex(x => x.ReturnDate);
            builder.HasIndex(x => x.BookId);
            #endregion
        }
    }
}
