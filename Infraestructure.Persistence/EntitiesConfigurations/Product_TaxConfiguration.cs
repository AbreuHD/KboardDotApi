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
    public class Product_TaxConfiguration : IEntityTypeConfiguration<Product_Tax>
    {
        public void Configure(EntityTypeBuilder<Product_Tax> builder)
        {
            builder.ToTable("Product_Tax");
            builder.HasKey(x => x.ID);
        }
    }
}
