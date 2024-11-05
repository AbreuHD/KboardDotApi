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
    internal class CharacteristicConfiguration : IEntityTypeConfiguration<Characteristic>
    {
        public void Configure(EntityTypeBuilder<Characteristic> builder)
        {
            builder.ToTable("Characteristic");

            builder.HasKey(x => x.ID);

            builder.HasMany(x => x.Characteristic_Products)
                .WithOne(x => x.Characteristics)
                .HasForeignKey(x => x.CharacteristicsId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
