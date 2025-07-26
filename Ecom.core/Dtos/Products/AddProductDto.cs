using Microsoft.AspNetCore.Http;

namespace Ecom.core.Dtos.Products
{
    public record AddProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public int CategoryId { get; set; }

        public IFormFileCollection Photo { get; set; }
    }
}
