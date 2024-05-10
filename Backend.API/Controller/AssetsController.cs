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
        [HttpGet("GetTotalAssetByWalletIdAsync/{walletId}")]
        public async Task<ActionResult> GetTotalAssetByWalletIdAsync(int walletId)
        {
            decimal totalAssets = 0;
            var assets = await assetsService.GetAllAssetsDTOByWalletIdAsync(walletId);
            foreach(var asset in assets)
            {
                var totalEachAsset = asset.Amount * asset.CurrentPrice;
                totalAssets += totalEachAsset;
            }

            return Ok(totalAssets);
        }

        [HttpGet("GetPatrimonyAsync/{walletId}")]
        public async Task<ActionResult> GetPatrimonyAsync(int walletId)
        {
            decimal patrimony = 0.00m;
            var assets = await assetsService.GetAllAssetsDTOByWalletIdAsync(walletId);
            if(assets == null )
                throw new Exception("Do not exist assets");

            foreach(var asset in assets)
            {
                var totalEachAsset = Math.Round(asset.Amount * asset.CurrentPrice,2);
                patrimony += totalEachAsset;
            }

            return Ok(patrimony);
            
        }

        [HttpPost("PostCreateAssetAsync")]
        public async Task PostCreateAssetsAsync(AssetsDTO assetsDTO)
        {
            var assets = await assetsService.GetAllAssetsDTOByWalletIdAsync(assetsDTO.WalletId);
            var assetExist = assets.FirstOrDefault(a => a.CodName == assetsDTO.CodName);

            decimal totalAssets = 0;
                foreach(var asset in assets)
                {
                    var totalEachAsset = asset.Amount * asset.CurrentPrice;
                    totalAssets = totalAssets + totalEachAsset;
                }

                totalAssets = totalAssets + (assetsDTO.Amount * assetsDTO.CurrentPrice);

            if(assetExist != null)
            {
                var sumAmount = assetExist.Amount + assetsDTO.Amount;
                var sumAverege = ((assetExist.AveregePrice * assetExist.Amount) + (assetsDTO.BuyPrice * assetsDTO.Amount)); 
                assetExist.AveregePrice =  sumAverege/sumAmount;
                assetExist.Amount = sumAmount;
                assetExist.BuyPrice = assetsDTO.BuyPrice;
                assetExist.CurrentPrice = assetsDTO.CurrentPrice;
                assetExist.Updated_at = DateTime.UtcNow;               
                assetExist.PerCent = Math.Round(((assetExist.CurrentPrice * assetExist.Amount)*100)/totalAssets, 2);
                

                await assetsService.UpdateAsync(assetExist);
            }
            else
            {
                assetsDTO.Created_at = DateTime.UtcNow;
                assetsDTO.Updated_at = DateTime.UtcNow;
                assetsDTO.Deleted_at = null;
                assetsDTO.AveregePrice = assetsDTO.BuyPrice;
                assetsDTO.PerCent = Math.Round(((assetsDTO.Amount * assetsDTO.CurrentPrice)*100)/totalAssets, 2);
                
              
                await assetsService.CreateAsync(assetsDTO);
            }
            
        }
    }
}