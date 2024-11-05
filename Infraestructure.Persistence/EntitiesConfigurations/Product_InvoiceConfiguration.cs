using Core.Domain.Entities.Invoice;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.EntitiesConfigurations
{
    public class Product_InvoiceConfiguration : IEntityTypeConfiguration<Product_Invoice>
    {
        public void Configure(EntityTypeBuilder<Product_Invoice> builder)
        {
            builder.ToTable("Product_Invoice");

            builder.HasKey(x => x.ID);
        }
    }
}
