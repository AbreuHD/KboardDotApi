﻿using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities.Category;
using Core.Domain.Entities.Source;
using Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Repositories
{
    public class Source_ProductRepository : GenericRepository<Source_Product>, ISource_ProductRepository
    {
        private readonly KboardDotContext context;

        public Source_ProductRepository(KboardDotContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }
    }
}