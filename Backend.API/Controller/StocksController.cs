using System;
using System.Collections.Generic;
using System.Linq;
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
    public class StocksController : ControllerBase
    {  

        private readonly IAssetsService assetsService;
        private readonly IUserAssetsService userAssetsService;
        public StocksController(IAssetsService assetsService, IUserAssetsService userAssetsService)
        {
            this.assetsService = assetsService;
            this.userAssetsService = userAssetsService;
        }

        [HttpGet("GetPerCentStocksByWalletId/{walletId}")]
        public async Task<ActionResult> GetPerCentStocksByWalletId(int walletId)
        {
            //filter all assets by type Stocks
            var assets = await assetsService.GetAllAsync();
            var assetsStock = assets.Where(a => a.SourceTypeAssets == SourceTypeAssets.Stocks);

            //filter all userAssets by type Stocks of user
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(walletId);
            var userAssetsStocks = userAssets.Where(ua => ua.SourceTypeAssets == SourceTypeAssets.Stocks);

            //filter ids 
            var userStocksAssetIds = userAssetsStocks.Select(ua => ua.AssetsId);
            var userAssetsIds = userAssets.Select(us => us.AssetsId);

            //filter all assets of user and 
            var filterAssetsStocks = assetsStock.Where(stock => userStocksAssetIds.Contains(stock.Id));
            
            //all Assets of user
            var allAssetsOfUser = assets.Where(asset => userAssetsIds.Contains(asset.Id));

            decimal totalStocks = 0;
            decimal totalAssets = 0;

            long amountStock = 0;
            long amountAsset = 0;
            decimal currentPrice = 0.00m;

            if(userAssetsStocks.Any())
            {
                foreach(var userAssetStock in userAssetsStocks)
                {
                    foreach(var assetStock in filterAssetsStocks)
                    {
                        if(userAssetStock.AssetsId == assetStock.Id)
                        {
                            amountStock = userAssetStock.Amount;
                            var totalEachStock = amountStock * assetStock.CurrentPrice;
                            totalStocks += totalEachStock;
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
                    var totalEachAsset = userAsset.Amount * currentPrice;
                    totalAssets += totalEachAsset;
                }

                var perCent = Math.Round((totalStocks * 100)/totalAssets, 2);

                return Ok(perCent);
            }  
            else
            {
                return Ok("Não possui nehuma Ação");
            }
            
        }
        [HttpGet("GetAllStocksByWalletIdAsync/{walletId}")]
        public async Task<ActionResult> GetAllStocksByWalletIdAsync(int walletId)
        {
            var userAsset = await userAssetsService.GetAllUserAssetsByWalletId(walletId);
            var userAssetsStock = userAsset.Where(ua => ua.SourceTypeAssets == SourceTypeAssets.Stocks);
            var stockAssetsIds = userAssetsStock.Select(s => s.AssetsId);
            var stockAssets = await assetsService.GetAllByIdsAsync(stockAssetsIds);
            return Ok(new { stockAssets , userAssetsStock });
        }
    }
}