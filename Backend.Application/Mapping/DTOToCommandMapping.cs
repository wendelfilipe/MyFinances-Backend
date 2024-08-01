using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.DTOs;
using Backend.Application.Wallets.Commands;
using Backend.Domain.Entites.WalletEntites;

namespace Backend.Application.Mapping
{
    public class DTOToCommandMapping : Profile
    {
        public DTOToCommandMapping()
        {
            CreateMap<WalletDTO, WalletCreateCommand>();
            CreateMap<WalletDTO, WalletRemoveCommand>();
        }
    }
}