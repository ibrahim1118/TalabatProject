using Adminpanal.Models;
using AutoMapper;
using Talabat.Core.Entitys;

namespace Adminpanal.Hellper
{
    public class MappingProFile :Profile
    {
        public MappingProFile()
        {
            CreateMap<Product, ProductVM>().ReverseMap();
        }
    }
}
