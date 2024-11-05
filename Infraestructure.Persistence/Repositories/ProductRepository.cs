using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities.Category;
using Core.Domain.Entities.Product;
using Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Infraestructure.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly KboardDotContext context;

        public ProductRepository(KboardDotContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }

        public async Task<List<Product>> SearchProduct(string? Name, int? CategoryId, int Page, int Quantity)
        {
            if (Page == 0) Page = 1;
            if (Quantity == 0) Quantity = 20;

            if (string.IsNullOrEmpty(Name))
            {
                if (CategoryId == null)
                {
                    return await context.Set<Product>()
                        .Skip((Page - 1) * Quantity).Take(Quantity).ToListAsync();
                }
                else
                {
                    return await context.Set<Product>()
                    .Where(x => x.Product_Categories.Any(y => y.CategoryId == CategoryId))
                    .Skip((Page - 1) * Quantity).Take(Quantity).ToListAsync();
                }
            }

            var searchKeywords = Name.ToLower().Split(' ');
            var responseProducts = new List<Product>();

            foreach (var keyword in searchKeywords)
            {
                if(CategoryId == null)
                {
                    responseProducts.AddRange(await context.Set<Product>()
                        .Where(x => x.Name.ToLower().Contains(keyword))
                        .Include(x => x.Product_Categories)
                        .ToListAsync());
                }
                else
                {
                    responseProducts.AddRange(await context.Set<Product>()
                        .Where(x => x.Name.ToLower().Contains(keyword))
                        .Include(x => x.Product_Categories)
                        .Where(x => x.Product_Categories.Any(y => y.CategoryId == CategoryId))
                        .ToListAsync());
                }

            }
            return responseProducts.Skip((Page - 1) * Quantity).Take(Quantity).Distinct().ToList();
        }
    }   
}
