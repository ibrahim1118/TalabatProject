using AutoMapper;
using Talabat.Core.Entitys;
using Talabat.Core.Entitys.Identity;
using Talabat.Core.Entitys.Order_Aggraget;
using Talabt.APIs.Dtos;

namespace Talabt.APIs.Helper
{
    public class MappingProFiles : Profile
    {
        public MappingProFiles()
        {
            CreateMap<Product, ProductsDto>()
            .ForMember(p => p.productBrand, o => o.MapFrom(s => s.productBrand.Name))
            .ForMember(p => p.productType, o => o.MapFrom(s => s.productType.Name))
            .ForMember(p => p.ImageUrl, o => o.MapFrom(s=> "https://localhost:7227/"+s.ImageUrl));

            CreateMap<Addrees, AddreesDto>().ReverseMap();

            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap(); 
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

            CreateMap<AddreesDto, Address>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.deliverMethod, d => d.MapFrom(d => d.DliveryMethod.ShortName)).
                ForMember(o => o.Cost, o => o.MapFrom(o => o.DliveryMethod.Cost));

            CreateMap<OrderItme, OrderItmeDto>()
               .ForMember(d => d.ProdcutId, o => o.MapFrom(o => o.Product.ProdcutId))
               .ForMember(d => d.ProdcutName, o => o.MapFrom(o => o.Product.ProdcutName))
               .ForMember(d => d.ImageUrl, o => o.MapFrom(o => o.Product.ImageUrl)); 
           
        }
    }
}
