﻿using Ecom.core.Dtos.Photo;

namespace Ecom.core.Dtos.Products
{
    public record ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public string CategoryName { get; set; }
        public List<PhotoDto> Photos { get; set; }
    };
}
