using Core.Domain.Entities.Taxes;
using KboardDotApi.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Repositories
{
    public interface ITaxRepository : IGenericRepository<Tax>
    {
    }
}
