using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            foreach(var assetFixed in assetsFixed)
            {
                var totalEachFixed = assetFixed.Amount * assetFixed.BuyPrice;
                totalFixed += totalEachFixed;
            }
            foreach(var asset in assets)
            {
                var totalEachAsset = asset.Amount * asset.BuyPrice;
                totalAssets += totalEachAsset;
            }

            var perCent = (totalFixed * 100)/totalAssets;

            return Ok(perCent);         
        }
    }
}