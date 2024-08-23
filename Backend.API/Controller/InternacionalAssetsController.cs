using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.Interfaces;
using Backend.Domain.Entites.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
            //filter all assets by type InterAssets
            var assets = await assetsService.GetAllAssetsAsync();
            var assetsInterAssets = assets.Where(a => a.SourceTypeAssets == SourceTypeAssets.InteralcionalAssets);

            //filter all userAssets by type InterAssets of user
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(walletId);
            var userInterAssets = userAssets.Where(ua => ua.SourceTypeAssets == SourceTypeAssets.InteralcionalAssets);

            //filter ids 
            var userInterAssetsIds = userInterAssets.Select(ua => ua.AssetsId);
            var userAssetsIds = userAssets.Select(us => us.AssetsId);

            //filter all assets of user and 
            var filterInterAssets = assetsInterAssets.Where(ia => userInterAssetsIds.Contains(ia.Id));
            
            //all Assets of user
            var allAssetsOfUser = assets.Where(asset => userAssetsIds.Contains(asset.Id));

            decimal totalInterAssets = 0;
            decimal totalAssets = 0;

            long amountInterAssets = 0;
            long amountAsset = 0;
            decimal currentPrice = 0.00m;

            if(userInterAssets.Any())
            {
                foreach(var userAssetInterAssets in userInterAssets)
                {
                    foreach(var assetInterAssets in filterInterAssets)
                    {
                        if(userAssetInterAssets.AssetsId == assetInterAssets.Id)
                        {
                            amountInterAssets = userAssetInterAssets.Amount;
                            var totalEachInterAssets = amountInterAssets * assetInterAssets.CurrentPrice;
                            totalInterAssets += totalEachInterAssets;
                            break;
                        }
                    }
                }
                foreach(var userAsset in userAssets)
                {
                    foreach(var assetOfUser in allAssetsOfUser)
                    {
                        if(userAsset.AssetsId == assetOfUser.Id)
                        {
                            currentPrice = assetOfUser.CurrentPrice;
                            break;
                        }
                    }
                    
                    if (userAsset.SourceTypeAssets == SourceTypeAssets.Fixed)
                    {
                        currentPrice = userAsset.AveregePrice;
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
            var interAssets = await assetsService.GetAllByIdsAsync(interAssetIds);
            return Ok(new {interAssets, userInterAssets});
        }
    }
}