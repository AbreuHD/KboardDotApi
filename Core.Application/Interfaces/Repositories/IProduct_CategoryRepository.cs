﻿using Core.Domain.Entities.Category;
using KboardDotApi.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Repositories
{
    public interface IProduct_CategoryRepository : IGenericRepository<Product_Category>
    {
    }
}