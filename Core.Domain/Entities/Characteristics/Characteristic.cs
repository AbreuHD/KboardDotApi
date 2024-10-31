using Core.Domain.Entities.Auditable;

namespace Core.Domain.Entities.Characteristics
{
    public class Characteristic : AuditableBase
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public ICollection<Characteristic_Product> Characteristic_Products { get; set; }
    }
}
