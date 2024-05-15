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
            var assets = await assetsService.GetAllAsync();
            var stocks = assets.Where(a => a.SourceTypeAssets == SourceTypeAssets.Stocks);
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(walletId);
            var assetIds = userAssets.Select(ua => ua.AssetsId);
            var userStocks = userAssets.Where(ua => ua.SourceTypeAssets == SourceTypeAssets.Stocks);
            var userStocksIds = userStocks.Select(us => us.Id);
            var userStocksByAssets = stocks.Where(stock => userStocksIds.Contains(stock.Id));
            var allAssetsByAssetsIds = assets.Where(asset => assetIds.Contains(asset.Id));

            decimal totalStocks = 0;
            decimal totalAssets = 0;

            long amountStock = 0;
            long amountAsset = 0;
            decimal currentPrice = 0.00m;

            if(userStocksByAssets.Any())
            {
                foreach(var userStock in userStocks)
                {
                    foreach(var userStocksByAsset in userStocksByAssets )
                    {
                        if(userStock.Id == userStocksByAsset.Id)
                        {
                            amountStock = userStock.Amount;
                            var totalEachStock = amountStock * userStocksByAsset.CurrentPrice;
                            totalStocks += totalEachStock;
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
            var stockIds = userAssetsStock.Select(s => s.Id);
            var stockAssets = assetsService.GetAllByIdsAsync(stockIds);
            return Ok(new { stockAssets , userAssetsStock });
        }
    }
}