using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Domain.Entites.Enums;
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

        [HttpGet("GetAllAssetsDTOAsync/{walletId}")]
        public async Task<IEnumerable<AssetsDTO>> GetAllAssetsDTOAsync(int walletId)
        {
           
            var assets = await assetsService.GetAllAssetsDTOByWalletIdAsync(walletId);
            if(assets == null )
                throw new Exception("Do not exist assets");


            return assets;
            
        }

        [HttpPost("PostCreateAssetAsync")]
        public async Task PostCreateAssetsAsync(AssetsDTO assetsDTO)
        {
            var assets = await assetsService.GetAllAssetsDTOByWalletIdAsync(assetsDTO.WalletId);
            var assetExist = assets.FirstOrDefault(a => a.CodName == assetsDTO.CodName);
            if(assetExist != null)
            {
                
                var averegePrice = assetExist.AveregePrice;
                averegePrice =  (averegePrice + (assetsDTO.Amount * assetsDTO.BuyPrice))/2;
                await assetsService.UpdateAsync(assetsDTO);
            }
            else
            {
                await assetsService.CreateAsync(assetsDTO);
            }
            
        }
    }
}