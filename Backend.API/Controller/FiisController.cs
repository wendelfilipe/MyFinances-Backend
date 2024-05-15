using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Domain.Entites.Enums;
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
            var assets = await assetsService.GetAllAsync();
            var fiis = assets.Where(a => a.SourceTypeAssets == SourceTypeAssets.InteralcionalAssets);
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(walletId);
            var assetIds = userAssets.Select(ua => ua.AssetsId);
            var userFiis = userAssets.Where(ua => ua.SourceTypeAssets == SourceTypeAssets.InteralcionalAssets);
            var fiisIds = userFiis.Select(ui => ui.AssetsId);
            var userFiiByAssets = fiis.Where(interAsset => fiisIds.Contains(interAsset.Id));
            var allAssetsByAssetsIds = assets.Where(asset => assetIds.Contains(asset.Id));

            decimal totalFiis = 0;
            decimal totalAssets = 0;

            long amountFiis = 0;
            long amountAsset = 0;
            decimal currentPrice = 0.00m;

            if(userFiiByAssets.Any())
            {
                foreach(var userFii in userFiis)
                {
                    foreach(var userFiiByAsset in userFiiByAssets )
                    {
                        if(userFii.Id == userFiiByAsset.Id)
                        {
                            amountFiis = userFii.Amount;
                            var totalEachFii = userFiiByAsset.CurrentPrice * amountFiis;
                            totalFiis += totalEachFii;
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
            var fiiIds = userFiis.Select(s => s.Id);
            var fiiAssets = assetsService.GetAllByIdsAsync(fiiIds);
            return Ok(new { fiiAssets , userFiis });
        }
    }
}