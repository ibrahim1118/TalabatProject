using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Talabat.Core;
using Talabat.Core.Entitys.Order_Aggraget;
using Talabat.Core.IServices;
using Talabt.APIs.Dtos;
using Talabt.APIs.Erorr;

namespace Talabt.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public OrdersController(IOrderService orderService, IMapper mapper
            ,IUnitOfWork unitOfWork)
        {
            this.orderService = orderService;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreatOrder(OrderDto orderDto)
        {
            var Emil = User.FindFirstValue(ClaimTypes.Email);
            var address = mapper.Map<Address>(orderDto.ShippingAddrees);
            var order = await orderService.CreateOrderAsync(Emil, orderDto.BasketId, orderDto.DeliveryMethodId, address); 
            if (order == null) {
                return BadRequest(new ApiRespones(400)); 
            }
            return Ok(mapper.Map<OrderToReturnDto>(order));   

        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GerOrdersForUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email); 

            var Order = await orderService.GetOrdersForUser (Email);    
            return Ok(mapper.Map<IReadOnlyList<OrderToReturnDto>>(Order));
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GerOrderByIdforUser(int id)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var order = await orderService.GetOrderByIdforUser(Email, id);
            if (order==null)
                 return NotFound(new ApiRespones(404));
            return Ok(mapper.Map<OrderToReturnDto>(order));   
        }
        [HttpGet("DeliverMethod")]

        public async Task<ActionResult<IReadOnlyList<DliveryMethod>>> GetAllDeliverMethod()
        {
            return Ok(await unitOfWork.Repostitry<DliveryMethod>().GetAllAysnc()); 
        }

    }
}
