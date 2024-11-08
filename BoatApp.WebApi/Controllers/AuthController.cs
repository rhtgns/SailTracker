using SailTracker.Business.Operations.User;
using SailTracker.Business.Operations.User.Dtos;
using SailTracker.WebApi.Jwt;
using SailTracker.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;


namespace SailTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Models.RegisterRequest request) // Yazım hatası düzeltildi
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addUserDto = new AddUserDto
            {
                Email = request.Email,
                FirstName = request.FirstNAme,
                LastName = request.LastName,
                Password = request.Password,
                BirthDate = request.BirthDate
            };

            var result = await _userService.AddUser(addUserDto);

            if (result.IsSucceeded) // Yazım hatası düzeltildi
                return Ok();
            else
                return BadRequest(result.Message);
        }

        [HttpPost("login")]
        public IActionResult Login(Models.LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _userService.LoginUser(new LoginUserDto { Email = request.Email, Password = request.Password });

            if (!result.IsSucceeded) // Yazım hatası düzeltildi
                return BadRequest($"{result.Message}");

            var user = result.Data;

            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var token = JwtHelper.GenerateJwtToken(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.Firstname,
                LastName = user.Lastname,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!)
            });

            return Ok(new LoginResponse
            {
                Message = "Giriş Başarılı",
                Token = token
            });
        }
    }
}

