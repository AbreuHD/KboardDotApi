using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities.Category;
using Core.Domain.Entities.Characteristics;
using Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Repositories
{
    public class Characteristics_ProductRepository : GenericRepository<Characteristic_Product>, ICharacteristics_ProductRepository
    {
        private readonly KboardDotContext context;

        public Characteristics_ProductRepository(KboardDotContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }
    }
}
