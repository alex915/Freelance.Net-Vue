using Auth;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using ProyectoOesia.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoOesia.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtSettings _jwtSettings;

        private readonly ILogger<AuthController> _logger;
        private readonly ApplicationDbContext _context;

        public AuthController(
                UserManager<User> userManager,
                RoleManager<IdentityRole> roleManager,
                IOptionsSnapshot<JwtSettings> jwtSettings,
                ILogger<AuthController> logger,
                ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string email, string password)
        {
            var user = _userManager.Users
                .SingleOrDefault(u => u.UserName == email);
            if (user is null)
            {
                _logger.LogError("User not found");
                return NotFound("User not found");
            }

            var userSigninResult = await _userManager.CheckPasswordAsync(user, password);

            if (userSigninResult)
            {
                _logger.LogInformation("User login ok");
                return Ok(GenerateJwt(user, await _userManager.GetRolesAsync(user)));
            }
            _logger.LogError("User cannot login");
            return BadRequest("Email or password incorrect.");
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] UserDto userDto)
        {
            var user = new User();
            user.Email = userDto.Email;
            user.UserName = userDto.Email;
            user.LastName = userDto.LastName;
            user.FirstName = userDto.FirstName;
            user.PhoneNumber = userDto.Phone;
            user.Ext = userDto.Ext;

            var userCreateResult = await _userManager.CreateAsync(user, userDto.Password);
            if (userCreateResult.Succeeded)
            {
                if (userDto.Company)
                {
                    await _userManager.AddToRoleAsync(user, "Worker");
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
                _logger.LogInformation("User registered");
                return Created(string.Empty, string.Empty);
            }
            _logger.LogError("Imposible to register " + user.Email);
            return Problem(userCreateResult.Errors.First().Description, null, 500);
        }

        [HttpGet("User")]
        public async Task<IActionResult> GetUser()
        {
            var user = await _userManager.GetUserAsync(User);

            if (User.IsInRole("Worker"))
            {

                user = await _context.Users.Include(x => x.Company).Where(x => x.Id.Equals(user.Id)).FirstOrDefaultAsync();
                if (user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Id = -1
                    };
                }
            }


            return Ok(user);
        }

        private string GenerateJwt(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
