using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
    public class AssetsController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAssetsService assetsService;
        private readonly IUserAssetsService userAssetsService;
        private readonly Timer timer;
        public AssetsController(IAssetsService assetsService, IHttpContextAccessor httpContextAccessor, IUserAssetsService userAssetsService)
        {
            this.assetsService = assetsService;
            this.httpContextAccessor = httpContextAccessor;
            this. userAssetsService = userAssetsService;
            this.timer = new Timer(RunDailyTask, null, TimeSpan.Zero, TimeSpan.FromHours(24));;
        }

        private void RunDailyTask(object state)
        {
            // Your method to run daily at 3 am
            
        }

        public void UpdateAssetsDaily()
        {
            
        }

        


        [HttpGet("GetAllAssetsDTOAsync")]
        public async Task<IEnumerable<AssetsDTO>> GetAllAssetsDTOAsync()
        {
           
            var assets = await assetsService.GetAllAsync();
            if(assets == null )
                throw new Exception("Do not exist assets");


            return assets;
            
        }
        [HttpGet("GetTotalAssetByWalletIdAsync/{walletId}")]
        public async Task<ActionResult> GetTotalAssetByWalletIdAsync(int walletId)
        {
            decimal totalAssets = 0;
            decimal currentPrice = 0.00m;
            var assets = await assetsService.GetAllAsync();
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(walletId);
            foreach(var userAsset in userAssets)
            {
                foreach(var asset in assets)
                {
                    if(userAsset.Id == asset.Id)
                    {
                        currentPrice = asset.CurrentPrice;
                        break;
                    }
                }
                var totalEachAsset = userAsset.Amount * currentPrice;
                totalAssets += totalEachAsset;
            }

            return Ok(totalAssets);
        }

        [HttpGet("GetPatrimonyAsync/{walletId}")]
        public async Task<ActionResult> GetPatrimonyAsync(int walletId)
        {
            decimal patrimony = 0.00m;
            decimal currentPrice = 0;

            var assets = await assetsService.GetAllAsync();
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(walletId);

            if(assets == null )
                throw new Exception("Do not exist assets");

            foreach(var userAsset in userAssets)
            {
                foreach(var asset in assets)
                {
                    if(userAsset.Id == asset.Id)
                    {
                        currentPrice = asset.CurrentPrice;
                        break;
                    }
                }
                var totalEachAsset = Math.Round(userAsset.Amount * currentPrice,2);
                patrimony += totalEachAsset;
            }

            return Ok(patrimony);
            
        }

        [HttpPost("PostCreateAssetAsync")]
        public async Task PostCreateAssetsAsync(CreateAssetRequestDTO createAssetRequestDTO)
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
                StartDate = createAssetRequestDTO.StartDate
            };
            var assets = await assetsService.GetAllAsync();
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(userAssetsDTO.WalletId);
            var assetExist = assets.FirstOrDefault(a => a.CodName == assetsDTO.CodName);
            if(assetExist != null)
            {
                userAssetExist = userAssets.FirstOrDefault(ua => ua.AssetsId == assetExist.Id);
            }

                
            if(assetExist != null && userAssetExist != null)
            {
                assetExist.CurrentPrice = assetsDTO.CurrentPrice;
                assetExist.Updated_at = DateTime.UtcNow;
               
                await assetsService.UpdateAsync(assetExist);

                var sumAmount = userAssetExist.Amount + userAssetsDTO.Amount;
                var sumAverege = ((userAssetExist.AveregePrice * userAssetExist.Amount) + (userAssetsDTO.BuyPrice * userAssetsDTO.Amount));
                userAssetExist.AveregePrice =  Math.Round(sumAverege/sumAmount, 2);

                userAssetExist.Amount = sumAmount;
                userAssetExist.BuyPrice = userAssetsDTO.BuyPrice;
                
                await userAssetsService.UpdateAsync(userAssetExist);
            }
            else
            {
                assetsDTO.Created_at = DateTime.UtcNow;
                assetsDTO.Updated_at = DateTime.UtcNow;
                assetsDTO.Deleted_at = null;

                await assetsService.CreateAsync(assetsDTO);

                var createdAssets = await assetsService.GetAllAsync();
                var createdAssetExist = createdAssets.FirstOrDefault(a => a.CodName == assetsDTO.CodName);

                userAssetsDTO.AveregePrice = userAssetsDTO.BuyPrice;
                userAssetsDTO.AssetsId = createdAssetExist.Id;
                userAssetsDTO.Deleted_at = null;
                userAssetsDTO.Updated_at = DateTime.UtcNow;
                userAssetsDTO.Created_at = DateTime.UtcNow;

                await userAssetsService.CreateAsync(userAssetsDTO);
            
            }
            
        }
    }
}