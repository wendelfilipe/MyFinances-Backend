using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.DTOs;
using Backend.Domain.Entites.AssetsEntites;
using Backend.Domain.Entites.UserAssetsEntity;
using Backend.Domain.Entites.UserEntites;
using Backend.Domain.Entites.WalletEntites;

namespace Backend.Application.Mapping
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Wallet, WalletDTO>().ReverseMap();
            CreateMap<Assets, AssetsDTO>().ReverseMap();
            CreateMap<UserAssets, UserAssetsDTO>().ReverseMap();
        }
    }
}