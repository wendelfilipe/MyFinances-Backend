using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Application.Services.EntityServices;
using Backend.Domain.Entites.WalletEntites;
using Backend.Domain.Interfaces;
using Backend.Domain.Interfaces.WalletInterface;

namespace Backend.Application.Services
{
    public class WalletService : EntityService<WalletDTO>, IWalletService
    {
        public WalletService(IEntityRepository<WalletDTO> entityRepository, IMapper mapper, IWalletRepository walletRepository) : base(entityRepository, mapper)
        {
        }
    }
}