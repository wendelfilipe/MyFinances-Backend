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
    public class FiisController : ControllerBase
    {   
        decimal totalFiis;
        decimal totalAssets;
        public IAssetsService assetsService;
        public FiisController(IAssetsService assetsService)
        {
            this.assetsService = assetsService;
        }
        [HttpGet("GetPerCentFiisByWalletId/{walletId}")]
        public async Task<ActionResult> GetPerCentFiisByWalletId(int walletId)
        {
            var fiis = await assetsService.GetFiisByWalletId(walletId);
            var assets = await assetsService.GetAllAssetsDTOByWalletIdAsync(walletId);
            foreach(var fii in fiis)
            {
                var totalEachFii = fii.Amount * fii.BuyPrice;
                totalFiis += totalEachFii;
            }
            foreach(var asset in assets)
            {
                var totalEachAsset = asset.Amount * asset.BuyPrice;
                totalAssets += totalEachAsset;
            }

            var perCent = (totalFiis * 100)/totalAssets;

            return Ok(perCent);         
        }
    }
}