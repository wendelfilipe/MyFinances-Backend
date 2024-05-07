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
        decimal totalStocks = 0;
        decimal totalAssets = 0;

        public IAssetsService assetsService;
        public StocksController(IAssetsService assetsService)
        {
            this.assetsService = assetsService;
        }

        [HttpGet("GetPerCentStocksByWalletId/{walletId}")]
        public async Task<ActionResult> GetPerCentStocksByWalletId(int walletId)
        {
            var stocks = await assetsService.GetStocksByWalletId(walletId);
            var assets = await assetsService.GetAllAssetsDTOByWalletIdAsync(walletId);

            if(stocks.Any())
            {
                foreach(var stock in stocks)
            {
                var totalEachStock = stock.Amount * stock.BuyPrice;
                totalStocks += totalEachStock;
            }
            foreach(var asset in assets)
            {
                var totalEachAsset = asset.Amount * asset.BuyPrice;
                totalAssets += totalEachAsset;
            }
        

            var perCentStock = (totalStocks * 100)/ totalAssets;

            return Ok(perCentStock);
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
            return Ok(stocks);
        }
    }
}