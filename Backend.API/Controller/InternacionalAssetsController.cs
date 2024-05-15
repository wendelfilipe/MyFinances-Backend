using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.Interfaces;
using Backend.Domain.Entites.Enums;
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
            var assets = await assetsService.GetAllAsync();
            var interAssets = assets.Where(a => a.SourceTypeAssets == SourceTypeAssets.InteralcionalAssets);
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(walletId);
            var assetIds = userAssets.Select(ua => ua.AssetsId);
            var userInterAssets = userAssets.Where(ua => ua.SourceTypeAssets == SourceTypeAssets.InteralcionalAssets);
            var interAssetIds = userInterAssets.Select(ui => ui.AssetsId);
            var userInterAssetByAssets = interAssets.Where(interAsset => interAssetIds.Contains(interAsset.Id));
            var allAssetsByAssetsIds = assets.Where(asset => assetIds.Contains(asset.Id));

            decimal totalInterAssets = 0;
            decimal totalAssets = 0;

            long amountInterAsset = 0;
            long amountAsset = 0;
            decimal currentPrice = 0.00m;

            if(userInterAssetByAssets.Any())
            {
                foreach(var userInterAsset in userInterAssets)
                {
                    foreach(var userInterAssetByAsset in userInterAssetByAssets )
                    {
                        if(userInterAsset.Id == userInterAssetByAsset.Id)
                        {
                            amountInterAsset = userInterAsset.Amount;
                            var totalEachInterAsset = userInterAssetByAsset.CurrentPrice * amountInterAsset;
                            totalInterAssets += totalEachInterAsset;
                            break;
                        }
                    }
                }
                foreach(var userAsset in userAssets)
                {
                    foreach(var allAssetByUserAssets in allAssetsByAssetsIds)
                    {
                        if(userAsset.Id == allAssetByUserAssets.Id)
                        {
                            currentPrice = allAssetByUserAssets.CurrentPrice;
                            break;
                        }
                    }
                    var totalEachAsset = userAsset.Amount * currentPrice;
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
            var userAsset = await userAssetsService.GetAllUserAssetsByWalletId(walletId);
            var userInterAssets = userAsset.Where(ua => ua.SourceTypeAssets == SourceTypeAssets.InteralcionalAssets);
            var interAssetIds = userInterAssets.Select(s => s.AssetsId);
            var interAssets = assetsService.GetAllByIdsAsync(interAssetIds);
            return Ok(new {interAssets, userInterAssets});
        }
    }
}