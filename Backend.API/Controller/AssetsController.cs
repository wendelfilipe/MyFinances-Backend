using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Domain.Interfaces.AssetsInterface;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAssetsService assetsService;
        public AssetsController(IAssetsService assetsService, IHttpContextAccessor httpContextAccessor)
        {
            this.assetsService = assetsService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<AssetsDTO>> GetAllAssetsDTOAsync()
        {
            var httpContext = httpContextAccessor.HttpContext;
            if(httpContext.Request.Cookies.ContainsKey("walletIdCookie"))
            {
                var walletIdJson = httpContext.Request.Cookies["walletIdCookie"];
                var walletId = JsonSerializer.Deserialize<int>(walletIdJson);



                return await assetsService.GetAllAssetsDTOByWalletIdAsync(walletId);
            }
            else
            {
                throw new Exception("Walled is invalid");
            }
            
        }


    }
}