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
    public class TrackingTypeConfiguration : IEntityTypeConfiguration<TrackingType>
    {
        public void Configure(EntityTypeBuilder<TrackingType> builder)
        {
            builder.ToTable("TrackingType");
            builder.HasKey(x => x.ID);

            builder.HasMany(x => x.Sources)
                .WithOne(x => x.TrackingType)
                .HasForeignKey(x => x.TrackingTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
