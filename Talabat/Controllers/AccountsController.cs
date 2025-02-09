using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Service;
using Talabat.DTO;
using Talabat.Errors;
using Talabat.Extensions;

namespace Talabat.Controllers
{

    public class AccountsController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenSevice _tokensevice;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager,ITokenSevice tokensevice, 
            SignInManager<AppUser> signInManager,IMapper mapper)
        {
            _userManager = userManager;
            _tokensevice = tokensevice;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost("login")]

        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
        var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password,false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token =await _tokensevice.CreateTokenAsync(user, _userManager),
            });

        }


        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> Register(RegistrtDto model)
        {
            if(CheckEmailExists(model.EmailAddress).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors =new string[]{"this email is already in user !!"} });
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.EmailAddress,
                UserName = model.EmailAddress.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
            };
            var reusult = await _userManager.CreateAsync(user,model.Password);

            if (!reusult.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(new UserDto()
            {
                DisplayName = model.DisplayName,
                Email = model.EmailAddress,
                Token = await _tokensevice.CreateTokenAsync(user, _userManager),
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
          var email=  User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokensevice.CreateTokenAsync(user,_userManager),
            });
        }
        [Authorize]
        [HttpGet("address")]
        async Task<ActionResult<AddressDto>> GetUserAddress()
        {

            var user = await _userManager.FindUserWithAddressByEmailAsync(User);
            var address =  _mapper.Map<Address, AddressDto>(user.Address);
            return Ok(address);
        }

        [Authorize]
        [HttpPut("address")]

        public async Task<ActionResult<AddressDto>>UpdateUserAddress(AddressDto updateaddress)
        {
            var address= _mapper.Map<AddressDto,Address>(updateaddress);
           
            var user = await _userManager.FindUserWithAddressByEmailAsync(User);
            user.Address = address;
            user.Address.FirstName = address.FirstName;
            user.Address.LastName = address.LastName;
            user.Address.City = address.City;
            user.Address.Country = address.Country; 
            user.Address.Street = address.Street;
           
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(updateaddress);
        }

        [HttpGet("emailexists")]

        public async Task<ActionResult<bool>>CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;

        }



    }   
    }
