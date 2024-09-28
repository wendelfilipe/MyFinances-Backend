using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Domain.Entites.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FiisController : ControllerBase
    {   
        private readonly IAssetsService assetsService;
        private readonly IUserAssetsService userAssetsService;
        public FiisController(IAssetsService assetsService, IUserAssetsService userAssetsService)
        {
            this.assetsService = assetsService;
            this.userAssetsService = userAssetsService;
        }
        [HttpGet("GetPerCentFiisByWalletId/{walletId}")]
        public async Task<ActionResult> GetPerCentFiisByWalletId(int walletId)
        {
             //filter all assets by type Fiis
            var assets = await assetsService.GetAllAssetsAsync();
            var assetsFiis = assets.Where(a => a.SourceTypeAssets == SourceTypeAssets.Fiis);

            //filter all userAssets by type Fiis of user
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(walletId);
            var userAssetsFiis = userAssets.Where(ua => ua.SourceTypeAssets == SourceTypeAssets.Fiis);

            //filter ids 
            var userFiisAssetIds = userAssetsFiis.Select(ua => ua.AssetsId);
            var userAssetsIds = userAssets.Select(us => us.AssetsId);

            //filter all assets of user and 
            var filterAssetsFiis = assetsFiis.Where(fii => userFiisAssetIds.Contains(fii.Id));
            
            //all Assets of user
            var allAssetsOfUser = assets.Where(asset => userAssetsIds.Contains(asset.Id));

            decimal totalFiis = 0;
            decimal totalAssets = 0;

            long amountFiis = 0;
            long amountAsset = 0;
            decimal currentPrice = 0.00m;

            if(userAssetsFiis.Any())
            {
                foreach(var userAssetFii in userAssetsFiis)
                {
                    foreach(var assetFiis in filterAssetsFiis)
                    {
                        if(userAssetFii.AssetsId == assetFiis.Id)
                        {
                            amountFiis = userAssetFii.Amount;
                            var totalEachFiis = amountFiis * assetFiis.CurrentPrice;
                            totalFiis += totalEachFiis;
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

                var perCent = Math.Round((totalFiis * 100)/totalAssets, 2);

                return Ok(perCent);
            }
            else
            {
                return Ok("Não possui nenhum fundo imobiliario");
            }
            
        }
        [HttpGet("GetAllFiisByWalletIdAsync/{walletId}")]
        public async Task<ActionResult> GetAllFiisByWalletIdAsync(int walletId)
        {
            
            var userAsset = await userAssetsService.GetAllUserAssetsByWalletId(walletId);
            var userFiis = userAsset.Where(ua => ua.SourceTypeAssets == SourceTypeAssets.Fiis);
            var fiiIds = userFiis.Select(s => s.AssetsId);
            var fiiAssets = await assetsService.GetAllByIdsAsync(fiiIds);
            return Ok(new { fiiAssets , userFiis });
        }
    }
}