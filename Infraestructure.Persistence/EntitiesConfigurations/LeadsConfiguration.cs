using Core.Domain.Entities.Leads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.EntitiesConfigurations
{
    public class LeadsConfiguration : IEntityTypeConfiguration<Leads>
    {
        public void Configure(EntityTypeBuilder<Leads> builder)
        {
            builder.ToTable("Leads");

            builder.HasKey(x => x.ID);

            builder.HasMany(x => x.Invoices)
                .WithOne(x => x.Leads)
                .HasForeignKey(x => x.LeadId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
