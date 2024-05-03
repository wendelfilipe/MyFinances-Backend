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
    public class WalletService : EntityService<Wallet, WalletDTO>, IWalletService
    {
        private readonly IEntityRepository<Wallet> entityRepository;
        private readonly IWalletRepository walletRepository;
        private readonly IMapper mapper;
        public WalletService(IEntityRepository<Wallet> entityRepository, IMapper mapper, IWalletRepository walletRepository) : base(entityRepository, mapper)
        {
            this.walletRepository = walletRepository;
            this.mapper = mapper;
            this.entityRepository = entityRepository;
        }

        public async Task<IEnumerable<WalletDTO>> GetAllWalletDTOByUserId(int userId)
        {
            var walletsEntity = await walletRepository.GetAllWalletsByUserId(userId);
            return mapper.Map<IEnumerable<WalletDTO>>(walletsEntity);
        }
    }
}