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
            var stocks = await assetsService.GetStocksByWalletId(walletId);
            var assets = await assetsService.GetAllAssetsDTOByWalletIdAsync(walletId);
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(walletId);

            decimal totalStocks = 0;
            decimal totalAssets = 0;

            long amountStock = 0;
            long amountAsset = 0;

            if(stocks.Any())
            {
                foreach(var stock in stocks)
                {
                    foreach(var userAsset in userAssets)
                    {
                        if(userAsset.Id == stock.Id)
                        {
                            amountStock = userAsset.Amount;
                        }
                    }
                    var totalEachStock = amountStock * stock.CurrentPrice;
                    totalStocks += totalEachStock;
                }
                foreach(var asset in assets)
                {
                    foreach(var userAsset in userAssets)
                    {
                        if(userAsset.Id == asset.Id)
                        {
                            amountAsset = userAsset.Amount;
                            break;
                        }
                    }
                    var totalEachAsset = amountAsset * asset.CurrentPrice;
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
            var stocks = await assetsService.GetStocksByWalletId(walletId);
            var userAssetStock = await userAssetsService.GetAllUserAssetsByWalletId(walletId);
            return Ok(new { stocks, userAssetStock });
        }
    }
}