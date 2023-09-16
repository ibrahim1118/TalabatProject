﻿using Talabat.Core.Entitys;

namespace Talabt.APIs.Dtos
{
    public class ProductsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int ProductBrandId { get; set; }
        public string productBrand { get; set; }

        public int ProductTypeId { get; set; }
        public string productType { get; set; }
    }
}
