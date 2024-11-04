using Core.Domain.Entities.Auditable;

namespace Core.Domain.Entities.Characteristics
{
    public class Characteristic_Product : AuditableBase
    {
        public int CharacteristicsId { get; set; }
        public Characteristic Characteristics { get; set; }

        public int ProductId { get; set; }
        public Product.Product Product { get; set; }
    }
}
