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
    public class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.ToTable("PaymentType");

            builder.HasKey(x => x.ID);

            builder.HasMany(x => x.Invoices)
                .WithOne(x => x.PaymentType)
                .HasForeignKey(x => x.PaymentTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
