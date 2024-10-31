using Core.Domain.Entities.Auditable;


namespace Core.Domain.Entities.Category
{
    public class Product_Category : AuditableBase
    {
        public int ProductId { get; set; }
        public Product.Product Product { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
