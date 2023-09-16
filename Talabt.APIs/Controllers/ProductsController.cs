using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Talabat.Core;
using Talabat.Core.Entitys;
using Talabat.Core.IRepositiry;
using Talabat.Core.Spcifications;
using Talabt.APIs.Dtos;
using Talabt.APIs.Erorr;
using Talabt.APIs.Helper;

namespace Talabt.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        [ChashingAttribute(660)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetAllProducts([FromQuery] ProductSpacPrams filter)
        {
            var spac = new  ProductSpacification(filter);
            var products = mapper.Map<IReadOnlyList<ProductsDto>>(await unitOfWork.Repostitry<Product>().GetSpacAllAysnc(spac));
            var spac2 = new ProductCountWihtSpac(filter); 
            var count = await unitOfWork.Repostitry<Product>().GetCountWithSpac(spac2);
            var result = new PaginationRespones<ProductsDto>(filter.PageIndex , filter.PageSize , count , products);
            if (products.Count == 0) 
            {
               return NotFound(new ApiRespones(404));   
            }
            return Ok(result);
        }

        [ChashingAttribute(660)]
        [HttpGet("{id}")]
        public  async Task<ActionResult<Product>> GetProductByid(int? id)
        {

            if (id == null)
                return BadRequest(new ApiRespones(400));
            var spac = new ProductSpacification(id.Value); 
            var product = mapper.Map<ProductsDto>(await unitOfWork.Repostitry<Product>().GetByIdSpacAysnc(spac)); 
            if (product == null)
                return NotFound(new ApiRespones(404)); 
            return Ok(product); 
        }


        [ChashingAttribute(660)]
        [HttpGet("brand")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var brands = await unitOfWork.Repostitry<ProductBrand>().GetAllAysnc();
            return Ok(brands);
        }

        [ChashingAttribute(660)]
        [HttpGet("Type")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllType()
        {
            var types = await unitOfWork.Repostitry<ProductType>().GetAllAysnc();
            return Ok(types);
        }



    }
}
