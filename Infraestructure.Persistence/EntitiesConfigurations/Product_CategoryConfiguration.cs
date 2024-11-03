using Core.Domain.Entities.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.EntitiesConfigurations
{
    internal class Product_CategoryConfiguration : IEntityTypeConfiguration<Product_Category>
    {
        public void Configure(EntityTypeBuilder<Product_Category> builder)
        {
            builder.ToTable("Product_Category");

            builder.HasKey(x => x.ID);
        }
    }
}
