using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Backend.API.model;
using Backend.Application.Interfaces;
using Backend.Domain.Account;
using Backend.Infra.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Backend.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate authenticate;
        private readonly IConfiguration configuration;
        private readonly IUserIdentityService userIdentityService;
        private readonly UserManager<ApplicationUser> userManager;
        public TokenController(
            IAuthenticate authenticate, 
            IConfiguration configuration,
            IUserIdentityService userIdentityService,
            UserManager<ApplicationUser> userManager
        )
        {
            this.authenticate = authenticate ?? throw new ArgumentException(nameof(authenticate));
            this.configuration = configuration;
            this.userIdentityService = userIdentityService;
            this.userManager = userManager;
        }

        [HttpPost("CreateUser")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [AllowAnonymous]
        public async Task<ActionResult> CreateUser(RegisterModel registerModel)
        {
            var result = await authenticate.RegisterUser(registerModel.Email, registerModel.Password);

            if(result)
            {
                return Ok($"User {registerModel.Email} was created successfully");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid create attempt");
                return BadRequest(ModelState);
            }
        }

        [HttpGet("GetUserId")]
        public async Task<ActionResult> GetUserId()
        {
            var userId = await userIdentityService.GetUserId(User);
            return Ok(userId);
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel loginModel)
        {
            var result = await authenticate.Authenticate(loginModel.Email, loginModel.Password);

            if(result)
            {
                var user = await userManager.FindByEmailAsync((loginModel.Email));
                if (user != null)
                {
                    return GenerateToken(user);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                    return Ok(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return Ok(ModelState);
            }
        }

        private UserToken GenerateToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("email", user.Email),
                new Claim("valorDaChave", "dfjgdjaksdbjdafu5895wpweofmsdnfgk45"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //gerar chave
            var privateKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])
            );

            //gerar a assinatura
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            //definir o tempo de expiração
            var expiration = DateTime.UtcNow.AddMinutes(10);

            //gerar token
            JwtSecurityToken token = new JwtSecurityToken(
                // emissor 
                issuer: configuration["Jwt:Issuer"],
                //audience
                audience: configuration["Jwt:Audience"],
                //claims
                claims: claims,
                //data de expiração
                expires: expiration,
                // assinatura digital
                signingCredentials: credentials
            );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

    }
}