using Core.Domain.Entities.Source;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.EntitiesConfigurations
{
    public class SourceConfiguration : IEntityTypeConfiguration<Source_ProductsConfiguration>
    {
        public void Configure(EntityTypeBuilder<Source_ProductsConfiguration> builder)
        {
            builder.ToTable("Source");
            builder.HasKey(x => x.ID);

            builder.HasMany(x => x.Source_Products)
                .WithOne(x => x.Source)
                .HasForeignKey(x => x.SourceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
