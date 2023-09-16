using AutoMapper;
using AutoMapper.Execution;
using System.Text.RegularExpressions;
using Talabat.Core.Entitys;
using Talabt.APIs.Dtos;

namespace Talabt.APIs.Helper
{
    public class ProductImageResolve : IValueResolver<Product, ProductsDto, string>
    {
        
        public string Resolve(Product source, ProductsDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))
            {
                return $"{source.ImageUrl}";
            }
            return string.Empty;
        }
    }
}



