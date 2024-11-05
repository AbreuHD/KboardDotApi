using Core.Domain.Entities.Auditable;
using Core.Domain.Entities.Category;
using Core.Domain.Entities.Characteristics;
using Core.Domain.Entities.Invoice;
using Core.Domain.Entities.Leads;
using Core.Domain.Entities.Product;
using Core.Domain.Entities.Source;
using Core.Domain.Entities.Taxes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Context
{
    public class KboardDotContext(DbContextOptions<KboardDotContext> options) : DbContext(options)
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Product_Category> Product_CategoryCategory { get; set; }
        public DbSet<Characteristic> Characteristic { get; set; }
        public DbSet<Characteristic_Product> Characteristic_Product { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<Product_Invoice> Product_Invoice { get; set; }
        public DbSet<Leads> Leads { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Source> Source { get; set; }
        public DbSet<Source_Product> Source_Product { get; set; }
        public DbSet<TrackingType> TrackingType { get; set; }
        public DbSet<Product_Tax> Product_Tax { get; set; }
        public DbSet<Tax> Tax { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "System";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(KboardDotContext).Assembly);
        }
    }
}
