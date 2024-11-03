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
    public class Source_ProductsConfiguration : IEntityTypeConfiguration<Source_Product>
    {
        public void Configure(EntityTypeBuilder<Source_Product> builder)
        {
            builder.ToTable("Source_Product");
            builder.HasKey(x => x.ID);
        }
    }
}
