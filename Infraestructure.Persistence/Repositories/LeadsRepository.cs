using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities.Leads;
using Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Repositories
{
    public class LeadRepository : GenericRepository<Leads>, ILeadRepository
    {
        private readonly KboardDotContext context;

        public LeadRepository(KboardDotContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }

        public async Task<List<Leads>> SearchLead(string? Name, int Page, int Quantity)
        {
            if (Page == 0) Page = 1;
            if (Quantity == 0) Quantity = 20;

            // Si no se proporciona nombre, solo realiza la paginación
            if (string.IsNullOrEmpty(Name))
            {
                return await context.Set<Leads>()
                    .Skip((Page - 1) * Quantity).Take(Quantity).ToListAsync();
            }

            // Separar palabras clave de búsqueda
            var searchKeywords = Name.ToLower().Split(' ');
            var responseLeads = new List<Leads>();

            // Buscar cada palabra clave en los nombres de los leads
            foreach (var keyword in searchKeywords)
            {
                responseLeads.AddRange(await context.Set<Leads>()
                    .Where(x => x.Name.ToLower().Contains(keyword) || x.LastName.ToLower().Contains(keyword))
                    .ToListAsync());
            }

            // Aplicar paginación y eliminar duplicados
            return responseLeads.Skip((Page - 1) * Quantity).Take(Quantity).Distinct().ToList();
        }
    }

}
