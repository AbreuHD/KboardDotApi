using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities.Taxes;
using Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Repositories
{
    public class TaxRepository : GenericRepository<Tax>, ITaxRepository
    {
        private readonly KboardDotContext context;

        public TaxRepository(KboardDotContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }
    }
}
