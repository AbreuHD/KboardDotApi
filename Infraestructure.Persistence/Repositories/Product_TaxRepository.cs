using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities.Characteristics;
using Core.Domain.Entities.Taxes;
using Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Repositories
{
    public class Product_TaxRepository : GenericRepository<Product_Tax>, IProduct_TaxRepository
    {
        private readonly KboardDotContext context;

        public Product_TaxRepository(KboardDotContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }
    }
}
