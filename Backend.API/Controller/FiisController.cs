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
            var fiis = await assetsService.GetFiisByWalletId(walletId);
            var assets = await assetsService.GetAllAssetsDTOByWalletIdAsync(walletId);
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(walletId);

            decimal totalFiis = 0;
            decimal totalAssets = 0;

            long amountFii = 0;
            long amountAsset = 0;

            if(fiis.Any())
            {
                foreach(var fii in fiis)
                {
                    foreach(var userAsset in userAssets)
                    {
                        if(userAsset.Id == fii.Id)
                        {
                            amountFii = userAsset.Amount;
                            break;
                        }
                    }
                    var totalEachFii = amountFii * fii.CurrentPrice;
                    totalFiis += totalEachFii;
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