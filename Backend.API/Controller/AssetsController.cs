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
using Newtonsoft.Json.Linq;

namespace Backend.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private static readonly HttpClient http = new HttpClient();
        private readonly IAssetsService assetsService;
        private readonly IUserAssetsService userAssetsService;
        public AssetsController(IAssetsService assetsService, IHttpContextAccessor httpContextAccessor, IUserAssetsService userAssetsService)
        {
            this.assetsService = assetsService;
            this.httpContextAccessor = httpContextAccessor;
            this.userAssetsService = userAssetsService;
        }

        [HttpGet("UpdateAssetsAsync")]
        public async Task UpdateAssetsAsync()
        {
            var assets = await assetsService.GetAllAsync();
            foreach (var asset in assets)
            {
                var dateYesterday = DateTime.UtcNow.AddHours(-24);
                if (asset.Updated_at < dateYesterday && asset.CodName != "Selic")
                {
                    try
                    {
                        HttpResponseMessage response =
                            await http.GetAsync(
                                $"https://brapi.dev/api/quote/{asset.CodName}?token=tSC4Zp6TZfoC6u7qeDGtdh");
                        response.EnsureSuccessStatusCode();
                        string responseBory = await response.Content.ReadAsStringAsync();

                        JObject jsonResponse = JObject.Parse(responseBory);
                        var regularMarketOpenResult = jsonResponse["results"];
                        if(regularMarketOpenResult[0] == null)
                            continue;
                        var regularMarketOpen = regularMarketOpenResult[0]["regularMarketOpen"].ToString();
                        
                        asset.CurrentPrice = decimal.Parse(regularMarketOpen);
                        asset.Updated_at = DateTime.UtcNow;

                        await assetsService.UpdateAsync(asset);

                    }
                    catch(HttpRequestException e)
                    {
                        Console.WriteLine($"Request error: {e.Message}");
                    }
                }
            }
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
                    if(userAsset.AssetsId == asset.Id)
                    {
                        currentPrice = asset.CurrentPrice;
                        break;
                    }
                }
                
                if (userAsset.SourceTypeAssets == SourceTypeAssets.Fixed)
                {
                    currentPrice = userAsset.AveregePrice;
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
                    if(userAsset.AssetsId == asset.Id)
                    {
                        currentPrice = asset.CurrentPrice;
                        break;
                    }
                }
                
                if (userAsset.SourceTypeAssets == SourceTypeAssets.Fixed)
                {
                    currentPrice = userAsset.AveregePrice;
                }
                var totalEachAsset = Math.Round(userAsset.Amount * currentPrice,2);
                patrimony += totalEachAsset;
            }

            return Ok(patrimony);
            
        }

        [HttpPost("PostCreateAssetAsync")]
        public async Task<ActionResult> PostCreateAssetsAsync(CreateAssetRequestDTO createAssetRequestDTO)
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
                try
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

                    return Ok("Ativo Atualizado com sucesso");
                }
                catch(Exception e)
                {
                    return Ok(e.Message);
                }
               
            }
            else
            {
                try
                {
                    assetsDTO.Created_at = DateTime.UtcNow;
                    assetsDTO.Updated_at = DateTime.UtcNow;
                    assetsDTO.Deleted_at = null;

                    if (assetExist == null)
                    {
                        await assetsService.CreateAsync(assetsDTO);
                    }

                    var createdAssets = await assetsService.GetAllAsync();
                    var createdAssetExist = createdAssets.FirstOrDefault(a => a.CodName == assetsDTO.CodName);

                    userAssetsDTO.AveregePrice = userAssetsDTO.BuyPrice;
                    userAssetsDTO.AssetsId = createdAssetExist.Id;
                    userAssetsDTO.Deleted_at = null;
                    userAssetsDTO.Updated_at = DateTime.UtcNow;
                    userAssetsDTO.Created_at = DateTime.UtcNow;

                    await userAssetsService.CreateAsync(userAssetsDTO);

                    return Ok("Ativo criado com sucesso");
                }
                catch(Exception e)
                {
                    return Ok(e.Message);
                }
                
            
            }
            
        }
    }
}