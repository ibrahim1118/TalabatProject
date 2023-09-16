using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics.Contracts;
using System.Security.Claims;
using Talabat.Core.Entitys.Identity;
using Talabat.Core.IServices;
using Talabt.APIs.Dtos;
using Talabt.APIs.Erorr;

namespace Talabt.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuccountController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ITokenServce tokenServce;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AuccountController(IMapper mapper,ITokenServce tokenServce, UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
        {
            this.mapper = mapper;
            this.tokenServce = tokenServce;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("longin")]
        public async Task<ActionResult<UserDto>> longin(LoginDto model)
        {
            var user =  await userManager.FindByEmailAsync(model.Email);
            if (user == null) { 
              return Unauthorized(new ApiRespones(401));
            }
            var res = await signInManager.CheckPasswordSignInAsync(user, model.Password,false);
            if (!res.Succeeded)
                return Unauthorized(new ApiRespones(401));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenServce.CreatTokeAysnc(user, userManager)
            });  

        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (ChackEamil(model.Email).Result.Value)
                return BadRequest(new ApiRespones(400)
                {
                    Message = "this Email Already Exiset"
                });
            var user = new AppUser()
            {
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                DisplayName = model.DispalyName,
                PhoneNumber = model.PhoneNumber
            };
            var res = await userManager.CreateAsync(user , model.Password);
            if (!res.Succeeded)
                return BadRequest(new ApiRespones(400));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenServce.CreatTokeAysnc(user, userManager)
            }) ;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenServce.CreatTokeAysnc(user, userManager)
            }); 
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddreesDto>> GetAdderss()
        {
            var emile = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(u=>u.Addrees).FirstOrDefaultAsync(u=>u.Email==emile);
            var adderss = mapper.Map<AddreesDto>(user.Addrees); 
            return Ok(adderss); 
        }

        [Authorize]
        [HttpPost("adderss")]
        public async Task<ActionResult<AddreesDto>> UpdateAddress(AddreesDto dto)
        {
            var emile = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(u => u.Addrees).FirstOrDefaultAsync(u => u.Email == emile);
            var adderss = mapper.Map<Addrees>(dto);
            adderss.id = user.Addrees.id;
            dto.id = user.Addrees.id;   
            user.Addrees = adderss; 
            var res = await userManager.UpdateAsync(user);
            if (!res.Succeeded) {
                return BadRequest(new ApiRespones(400)); 
            }
            return Ok(dto);
        
        }

        [HttpGet("EmailExited")]
        public  async Task<ActionResult<bool>> ChackEamil(string Email)
        {
            return await userManager.FindByEmailAsync(Email) is not null;
        }
    }
}
