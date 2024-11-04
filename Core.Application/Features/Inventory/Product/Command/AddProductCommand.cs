using Core.Application.Dtos.Inventory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Inventory.Product.Command
{
    public class AddProductCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public double SalePrice { get; set; }
        public double BuyPrice { get; set; }
        public int Stock { get; set; }

        public List<int> Categories { get; set; }
        public List<CreateCharacteristicsDto> Characteristic { get; set; }

        public CreateSourceDto? Source { get; set; }
    }
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, bool>
    {

        public AddProductCommandHandler()
        {

        }

        public Task<bool> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
