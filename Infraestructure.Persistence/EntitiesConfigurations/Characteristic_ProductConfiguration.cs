using Core.Domain.Entities.Characteristics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.EntitiesConfigurations
{
    public class Characteristic_ProductConfiguration : IEntityTypeConfiguration<Characteristic_Product>
    {
        public void Configure(EntityTypeBuilder<Characteristic_Product> builder)
        {
            builder.ToTable("Characteristic_Product");

            builder.HasKey(x => x.ID);
        }
    }
}
