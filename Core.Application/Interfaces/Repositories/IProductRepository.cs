using Core.Domain.Entities.Product;
using KboardDotApi.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> SearchProduct(string? Name, int? CategoryId, int Page, int Quantity);
    }
}
