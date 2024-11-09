using Core.Domain.Entities.Characteristics;
using Core.Domain.Entities.Leads;
using KboardDotApi.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Repositories
{
    public interface ILeadRepository : IGenericRepository<Leads>
    {
        Task<List<Leads>> SearchLead(string? Name, int Page, int Quantity);
    }
}
