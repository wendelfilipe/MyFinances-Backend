using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Application.Services.EntityServices;
using Backend.Domain.Entites.UserEntites;
using Backend.Domain.Interfaces;
using Backend.Domain.Interfaces.UserInterface;

namespace Backend.Application.Services
{
    public class UserService : EntityService<User, UserDTO>, IUserService
    {
        private readonly IEntityRepository<User> entityRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public UserService(IEntityRepository<User> entityRepository, IMapper mapper, IUserRepository userRepository) : base(entityRepository, mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.entityRepository = entityRepository;
        }

        public async Task<UserDTO> GetUserDTOByEmailAsync(string email)
        {
            var userEntity = await userRepository.GetByEmailAsync(email);
            return mapper.Map<UserDTO>(userEntity);
        }
    }
}