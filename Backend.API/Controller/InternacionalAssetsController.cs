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
        private readonly IAssetsService assetsService;
        private readonly IUserAssetsService userAssetsService;
        public InternacionalAssetsController(IAssetsService assetsService, IUserAssetsService userAssetsService)
        {
            this.assetsService = assetsService;
            this.userAssetsService = userAssetsService;
        }
        [HttpGet("GetPerCentInternacionalAssetsByWalletId/{walletId}")]
        public async Task<ActionResult> GetPerCentInternacionalAssetsByWalletId(int walletId)
        {
            var interAssets = await assetsService.GetInternacionalAssetsByWalletId(walletId);
            var assets = await assetsService.GetAllAssetsDTOByWalletIdAsync(walletId);
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(walletId);

            decimal totalInterAssets = 0;
            decimal totalAssets = 0;

            long amountInterAsset = 0;
            long amountAsset = 0;

            if(interAssets.Any())
            {
                foreach(var interAsset in interAssets)
                {
                    foreach(var userAsset in userAssets)
                    {
                        if(userAsset.Id == interAsset.Id)
                        {
                            amountInterAsset = userAsset.Amount;
                        }
                    }
                    var totalEachInterAsset = amountInterAsset * interAsset.CurrentPrice;
                    totalInterAssets += totalEachInterAsset;
                }
                foreach(var asset in assets)
                {
                    foreach(var userAsset in userAssets)
                    {
                        if(userAsset.Id == asset.Id)
                        {
                            amountAsset = userAsset.Amount;
                        }
                    }
                    var totalEachAsset = amountAsset * asset.CurrentPrice;
                    totalAssets += totalEachAsset;
                }

                var perCent = Math.Round((totalInterAssets * 100)/totalAssets, 2);

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
            var interAssets = await assetsService.GetInternacionalAssetsByWalletId(walletId);
            var userInterAssets = await userAssetsService.GetAllUserAssetsByWalletId(walletId);
            return Ok(new {interAssets, userInterAssets});
        }
    }
}