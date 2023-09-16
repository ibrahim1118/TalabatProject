using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Talabat.Core.Entitys;
using Talabat.Core.IRepositiry;
using Talabt.APIs.Dtos;
using Talabt.APIs.Erorr;

namespace Talabt.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepositiry basketRepositiry;
        private readonly IMapper mapper;

        public BasketController(IBasketRepositiry basketRepositiry,IMapper mapper)
        {
            this.basketRepositiry = basketRepositiry;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket =await basketRepositiry.GetBasket(id);
            if (basket == null)
                return new CustomerBasket(id);
            else
                return Ok(basket); 
        }
        [HttpPost] 
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basketDto)
        {
            var basket = mapper.Map<CustomerBasket>(basketDto); 
            var baske = await basketRepositiry.UpdataOrCreae(basket);
            if (baske is null)
                return BadRequest(new ApiRespones(400)); 
           
            return Ok(baske);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            return  await basketRepositiry.DeleteBasket(id); 
        }
        


    }
}
