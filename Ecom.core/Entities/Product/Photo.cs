using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.core.Entities.Product
{
    public class Photo : BaseEntity<int>
    {
        // public int Id { get; set; }
        public string ImageName { get; set; }
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
    }
}
