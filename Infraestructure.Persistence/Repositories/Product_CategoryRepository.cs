using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities.Category;
using Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Repositories
{
    public class Product_CategoryRepository : GenericRepository<Product_Category>, IProduct_CategoryRepository
    {
        private readonly KboardDotContext context;

        public Product_CategoryRepository(KboardDotContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }
    }
}
