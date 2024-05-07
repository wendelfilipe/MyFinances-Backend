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
    public class InternacionalAssetsController : ControllerBase
    {
        decimal totalInterAsset;
        decimal totalAssets;
        public IAssetsService assetsService;
        public InternacionalAssetsController(IAssetsService assetsService)
        {
            this.assetsService = assetsService;
        }
        [HttpGet("GetPerCentInternacionalAssetsByWalletId/{walletId}")]
        public async Task<ActionResult> GetPerCentInternacionalAssetsByWalletId(int walletId)
        {
            var interAssets = await assetsService.GetFiisByWalletId(walletId);
            var assets = await assetsService.GetAllAssetsDTOByWalletIdAsync(walletId);
            if(interAssets.Any())
            {
                foreach(var interAsset in interAssets)
                {
                    var totalEachInterAsset = interAsset.Amount * interAsset.BuyPrice;
                    totalInterAsset += totalEachInterAsset;
                }
                foreach(var asset in assets)
                {
                    var totalEachAsset = asset.Amount * asset.BuyPrice;
                    totalAssets += totalEachAsset;
                }

                var perCent = (totalInterAsset * 100)/totalAssets;

                return Ok(perCent);         
            }
            else
            {
                return Ok("Não possui nenhum ativos internacional");
            }
            
        }
        [HttpGet("GetAllInterAssetsByWalletIdAsync/{walletId}")]
        public async Task<ActionResult> GetAllInterAssetsByWalletIdAsync(int walletId)
        {
            var interAsset = await assetsService.GetInternacionalAssetsByWalletId(walletId);
            return Ok(interAsset);
        }
    }
}