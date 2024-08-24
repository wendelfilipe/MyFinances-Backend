using AutoMapper;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Application.Wallets.Commands;
using Backend.Application.Wallets.Queries;
using MediatR;

namespace Backend.Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        public WalletService(IMapper mapper, IMediator mediator)
        {  
            this.mapper = mapper;
            this.mediator = mediator;
        }
        public async Task CreateAsync(WalletDTO walletDTO)
        {
            var walletCreateCommand = mapper.Map<WalletCreateCommand>(walletDTO);
            await mediator.Send(walletCreateCommand);
        }

        public async Task DeteleAsync(int id)
        {
            var walletRemoveCommand = new WalletRemoveCommand(id);
            if(walletRemoveCommand == null)
                throw new Exception("Wallet not found, when was being deleted");

            await mediator.Send(walletRemoveCommand);
        }

        public async Task<IEnumerable<WalletDTO>> GetAllWalletDTOByUserId(string userId)
        {
            var getAllWalletDTOByUserId = new GetAllWalletsDTOByUserIdQuery(userId);
            if(getAllWalletDTOByUserId == null)
                throw new Exception("Wallet not found, when was being got by userId");
            
            var result = await mediator.Send(getAllWalletDTOByUserId);

            return mapper.Map<IEnumerable<WalletDTO>>(result);
        }

        public async Task UpdateAsync(WalletDTO walletDTO)
        {
            var walletUpdateCommand = mapper.Map<WalletUpdateCommand>(walletDTO);
            if(walletUpdateCommand == null)
                throw new Exception("Wallet not found, when was being updated");

            await mediator.Send(walletUpdateCommand);
        }
    }
}