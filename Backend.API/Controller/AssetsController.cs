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

        


        [HttpGet("GetAllAssetsDTOAsync/{walletId}")]
        public async Task<IEnumerable<AssetsDTO>> GetAllAssetsDTOAsync(int walletId)
        {
           
            var assets = await assetsService.GetAllAssetsDTOByWalletIdAsync(walletId);
            if(assets == null )
                throw new Exception("Do not exist assets");


            return assets;
            
        }
        [HttpGet("GetTotalAssetByWalletIdAsync/{walletId}")]
        public async Task<ActionResult> GetTotalAssetByWalletIdAsync(int walletId)
        {
            decimal totalAssets = 0;
            long amount = 0;
            var assets = await assetsService.GetAllAssetsDTOByWalletIdAsync(walletId);
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(walletId);
            foreach(var asset in assets)
            {
                foreach(var userAsset in userAssets)
                {
                    if(userAsset.Id == asset.Id)
                    {
                        amount = userAsset.Amount;
                    }
                }
                var totalEachAsset = amount * asset.CurrentPrice;
                totalAssets += totalEachAsset;
            }

            return Ok(totalAssets);
        }

        [HttpGet("GetPatrimonyAsync/{walletId}")]
        public async Task<ActionResult> GetPatrimonyAsync(int walletId)
        {
            decimal patrimony = 0.00m;
            long amount = 0;

            var assets = await assetsService.GetAllAssetsDTOByWalletIdAsync(walletId);
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(walletId);

            if(assets == null )
                throw new Exception("Do not exist assets");

            foreach(var asset in assets)
            {
                foreach(var userAsset in userAssets)
                {
                    if(userAsset.Id == asset.Id)
                    {
                        amount = userAsset.Amount;
                        break;
                    }
                }
                var totalEachAsset = Math.Round(amount * asset.CurrentPrice,2);
                patrimony += totalEachAsset;
            }

            return Ok(patrimony);
            
        }

        [HttpPost("PostCreateAssetAsync")]
        public async Task PostCreateAssetsAsync(AssetsDTO assetsDTO, UserAssetsDTO userAssetsDTO)
        {
            UserAssetsDTO? userAssetExist = null;
            var assets = await assetsService.GetAllAssetsDTOByWalletIdAsync(assetsDTO.WalletId);
            var userAssets = await userAssetsService.GetAllUserAssetsByWalletId(userAssetsDTO.WalletId);
            var assetExist = assets.FirstOrDefault(a => a.CodName == assetsDTO.CodName);
            if(assetExist != null)
            {
                userAssetExist = userAssets.FirstOrDefault(ua => ua.Id == assetExist.Id);
            }

                
            if(assetExist != null && userAssetExist != null)
            {
                assetsDTO.CurrentPrice = assetsDTO.CurrentPrice;
                assetsDTO.Updated_at = DateTime.UtcNow;
               
                await assetsService.UpdateAsync(assetExist);

                var sumAmount = userAssetExist.Amount + userAssetsDTO.Amount;
                var sumAverege = ((userAssetExist.AveregePrice * userAssetExist.Amount) + (userAssetsDTO.BuyPrice * userAssetsDTO.Amount));
                userAssetExist.AveregePrice =  sumAverege/sumAmount;

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

                userAssetsDTO.AveregePrice = userAssetsDTO.BuyPrice;

                await userAssetsService.CreateAsync(userAssetsDTO);
            
            }
            
        }
    }
}