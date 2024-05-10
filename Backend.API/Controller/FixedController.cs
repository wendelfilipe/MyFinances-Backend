using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class FixedController : ControllerBase
    {
        decimal totalFixed;
        decimal totalAssets;
        public IAssetsService assetsService;
        public FixedController(IAssetsService assetsService)
        {
            this.assetsService = assetsService;
        }
        [HttpGet("GetPerCentFixedsByWalletId/{walletId}")]
        public async Task<ActionResult> GetPerCentFixedsByWalletId(int walletId)
        {
            var assetsFixed = await assetsService.GetFixedByWalletId(walletId);
            var assets = await assetsService.GetAllAssetsDTOByWalletIdAsync(walletId);
            if(assetsFixed.Any())
            {
                foreach(var assetFixed in assetsFixed)
                {
                    var totalEachFixed = assetFixed.Amount * assetFixed.CurrentPrice;
                    totalFixed += totalEachFixed;
                }
                foreach(var asset in assets)
                {
                    var totalEachAsset = asset.Amount * asset.CurrentPrice;
                    totalAssets += totalEachAsset;
                }

                var perCent = Math.Round((totalFixed * 100)/totalAssets, 2);

                return Ok(perCent); 
            }
            else
            {
                return Ok("Não possui renda fixa");
            }
                   
        }
        [HttpGet("GetAllFixedByWalletIdAsync/{walletId}")]
        public async Task<ActionResult> GetAllFixedByWalletIdAsync(int walletId)
        {
            var assetFixed = await assetsService.GetFixedByWalletId(walletId);
            return Ok(assetFixed);
        }
        [HttpPost("PostCreateFixedAsync")]
        public async Task PostCreateFixedAsync(AssetsDTO assetsDTO)
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
                 
                assetExist.BuyPrice = assetExist.BuyPrice + assetsDTO.BuyPrice;
                assetExist.CurrentPrice = assetExist.CurrentPrice + assetsDTO.BuyPrice;
                assetExist.Updated_at = DateTime.UtcNow;

                totalAssets = totalAssets + assetExist.CurrentPrice;

                assetExist.PerCent = Math.Round((assetExist.CurrentPrice*100)/totalAssets, 2);
               
               

                await assetsService.UpdateAsync(assetExist);
            }
            else
            {
               
                assetsDTO.Created_at = DateTime.UtcNow;
                assetsDTO.Updated_at = DateTime.UtcNow;
                assetsDTO.Deleted_at = null;
                assetsDTO.PerCent = Math.Round((assetsDTO.CurrentPrice*100)/totalAssets, 2);
                
              
                await assetsService.CreateAsync(assetsDTO);
            }
        }
    }
}