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
        public IAssetsService assetsService;
        public StocksController(IAssetsService assetsService)
        {
            this.assetsService = assetsService;
        }

        [HttpGet]
        public Task<IEnumerable<AssetsDTO>> GetAllStocksByWalletId(int walletId)
        {
            var stocks = assetsService.GetStocksByWalletIdAndTypeAssets(walletId);
            return stocks;
        }
    }
}