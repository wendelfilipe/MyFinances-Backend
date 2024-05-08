using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class FiisController : ControllerBase
    {   
        decimal totalFiis;
        decimal totalAssets;
        public IAssetsService assetsService;
        public FiisController(IAssetsService assetsService)
        {
            this.assetsService = assetsService;
        }
        [HttpGet("GetPerCentFiisByWalletId/{walletId}")]
        public async Task<ActionResult> GetPerCentFiisByWalletId(int walletId)
        {
            var fiis = await assetsService.GetFiisByWalletId(walletId);
            var assets = await assetsService.GetAllAssetsDTOByWalletIdAsync(walletId);
            if(fiis.Any())
            {
                foreach(var fii in fiis)
                {
                    var totalEachFii = fii.Amount * fii.CurrentPrice;
                    totalFiis += totalEachFii;
                }
                foreach(var asset in assets)
                {
                    var totalEachAsset = asset.Amount * asset.CurrentPrice;
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
            var fiis = await assetsService.GetFiisByWalletId(walletId);
            return Ok(fiis);
        }
    }
}