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
    public class FixedController : ControllerBase
    {
        decimal totalFixed;
        decimal totalAssets;
        private readonly IAssetsService assetsService;
        private readonly IUserAssetsService userAssetsService;
        public FixedController(IAssetsService assetsService, IUserAssetsService userAssetsService)
        {
            this.assetsService = assetsService;
            this.userAssetsService = userAssetsService;
        }
        [HttpGet("GetPerCentFixedsByWalletId/{walletId}")]
        public async Task<ActionResult> GetPerCentFixedsByWalletId(int walletId)
        {
            var assets = await assetsService.GetAllAsync();
            var assetsFixed = assets.Where(a => a.SourceTypeAssets == SourceTypeAssets.Fixed);
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(walletId);

            decimal totalFixed = 0;
            decimal totalAssetsFixed = 0;

            long amountFixed = 0;
            long amountAsset = 0;

            if(assetsFixed.Any())
            {
                foreach(var assetFixed in assetsFixed)
                {
                    foreach(var userAsset in userAssets)
                    {
                        if(userAsset.AssetsId == assetFixed.Id)
                        {
                            amountFixed = userAsset.Amount;
                        }
                    }
                    var totalEachFixed = amountFixed * assetFixed.CurrentPrice;
                    totalAssetsFixed += totalEachFixed;
                }
                foreach(var asset in assets)
                {
                    foreach(var userAsset in userAssets)
                    {
                        if(userAsset.AssetsId == asset.Id)
                        {
                            amountAsset = userAsset.Amount;
                        }
                    }
                    var totalEachAsset = amountAsset * asset.CurrentPrice;
                    totalAssets += totalEachAsset;
                }

                var perCent = Math.Round((totalAssetsFixed * 100)/totalAssets, 2);

                return Ok(perCent);         
            }
            else
            {
                return Ok("Não possui renda fixa");
            }              
        }

        [HttpGet("GetAllFixedByWalletIdAsync/{walletId}")]
        public async Task<ActionResult> GetAllFixedByWalletIdAsync(int walletId)
        {
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(walletId);
            var userAssetsFixed = userAssets.Where(ua => ua.SourceTypeAssets == SourceTypeAssets.Fixed);
            var assetsIds = userAssetsFixed.Select(ua => ua.AssetsId);
            var assetsFixed = await assetsService.GetAllByIdsAsync(assetsIds);
            return Ok( new { assetsFixed, userAssetsFixed });
        }

        [HttpPost("PostCreateFixedAsync")]
        public async Task PostCreateFixedAsync(CreateAssetRequestDTO createAssetRequestDTO)
        {
            UserAssetsDTO? userAssetExist = null;

            AssetsDTO assetsDTO = new()
            {
                CodName = createAssetRequestDTO.CodName,
                CurrentPrice = createAssetRequestDTO.CurrentPrice,
                SourceCreate = createAssetRequestDTO.SourceCreate,
                SourceTypeAssets = createAssetRequestDTO.SourceTypeAssets
            };

            UserAssetsDTO userAssetsDTO = new()
            {
                WalletId = createAssetRequestDTO.WalletId,
                BuyPrice = createAssetRequestDTO.BuyPrice,
                Amount = createAssetRequestDTO.Amount,
                PerCentCDI = createAssetRequestDTO.PerCentCDI,
                AveregePrice = createAssetRequestDTO.AveregePrice,
                SourceCreate = createAssetRequestDTO.SourceCreate,
                SourceTypeAssets = createAssetRequestDTO.SourceTypeAssets,
                StartDate = createAssetRequestDTO.StartDate,
                EndDate = createAssetRequestDTO.EndDate
            };
            
            var assets = await assetsService.GetAllAsync();
            var assetExist = assets.FirstOrDefault(a => a.CodName == assetsDTO.CodName);
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(userAssetsDTO.WalletId);
            if(assetExist != null)
            {
                userAssetExist = userAssets.FirstOrDefault(ua => ua.AssetsId == assetExist.Id && ua.PerCentCDI == userAssetsDTO.PerCentCDI);
            }
                
            if(assetExist != null && userAssetExist != null)
            {
                assetExist.Updated_at = DateTime.UtcNow;
                assetExist.CurrentPrice += userAssetsDTO.BuyPrice;
               
                await assetsService.UpdateAsync(assetExist);

                userAssetExist.BuyPrice += userAssetsDTO.BuyPrice;
                userAssetExist.StartDate = userAssetsDTO.StartDate;
                userAssetExist.EndDate = userAssetsDTO.EndDate;

                await userAssetsService.UpdateAsync(userAssetExist);
            }
            else
            {
               
                assetsDTO.Created_at = DateTime.UtcNow;
                assetsDTO.Updated_at = DateTime.UtcNow;
                assetsDTO.Deleted_at = null;
                assetsDTO.CurrentPrice = userAssetsDTO.BuyPrice;
    
                await assetsService.CreateAsync(assetsDTO);

                var createdAssets = await assetsService.GetAllAsync();
                var createdAssetExist = createdAssets.FirstOrDefault(a => a.CodName == assetsDTO.CodName);

                userAssetsDTO.AssetsId = createdAssetExist.Id;
                userAssetsDTO.Created_at = DateTime.UtcNow;
                userAssetsDTO.Updated_at = DateTime.UtcNow;
                userAssetsDTO.Deleted_at = null;

                await userAssetsService.CreateAsync(userAssetsDTO);
            }
        }
    }
}