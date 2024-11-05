using Core.Domain.Entities.Category;
using Core.Domain.Entities.Characteristics;
using Core.Domain.Entities.Invoice;
using Core.Domain.Entities.Source;
using Core.Domain.Entities.Taxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Dtos.Inventory.Response
{
    public class DetailsProductResponseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public double SalePrice { get; set; }
        public double BuyPrice { get; set; }
        public int Stock { get; set; }

        public List<CategoryResponseDto> Categories { get; set; }
        public List<CharacteristicResponseDto> Characteristics { get; set; }
        public SourceResponseDto Source { get; set; }
        public List<TaxResponseDto> Taxs { get; set; }
    }
}
