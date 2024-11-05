using Core.Domain.Entities.Taxes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.EntitiesConfigurations
{
    public class TaxConfiguration : IEntityTypeConfiguration<Tax>
    {
        public void Configure(EntityTypeBuilder<Tax> builder)
        {
            builder.ToTable("Tax");

            builder.HasKey(x => x.ID);

            builder.HasMany(x => x.Product_Tax)
                .WithOne(x => x.Tax)
                .HasForeignKey(x => x.TaxId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
