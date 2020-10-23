using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dfo.Main.Domain.Dto;
using Dfo.Main.Domain.Extensions;
using Dfo.Main.Domain.Interfaces.Events;
using Dfo.Main.Domain.Interfaces.Repositories;
using Dfo.Main.Domain.Interfaces.Services;
using Dfo.Main.Domain.Models;

namespace Dfo.Main.Domain.Services
{
    public class UserService : ServiceMediator, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IMediatorHandler mediator, IUserRepository userRepository) : base(mediator)
        {
            _userRepository = userRepository;
        }

        public async Task<Tuple<bool, string>> AddUser(UserDto dto)
        {
            var model = dto.MergeToDestination<User>();

            await _userRepository.Add(model);

            return Tuple.Create(false, string.Empty);
        }

        public async Task<Tuple<bool, UserDto, string>> GetUser(int id)
        {
            var user = await _userRepository.GetById(id);

            var resultDto = user.MergeToDestination<UserDto>();

            return Tuple.Create(false, resultDto, string.Empty);
        }

        public async Task<Tuple<bool, List<UserDto>, string>> GetUsers()
        {
            var users = await _userRepository.GetAll();

            var resultDto = users.MergeToDestination<List<UserDto>>();

            return Tuple.Create(false, resultDto, string.Empty);
        }

        public async Task<Tuple<bool, string>> UpdateUser(UserDto dto)
        {
            var user = await _userRepository.GetById(dto.Id);

            if (user == null) return Tuple.Create(true, "User not found!");

            var model = dto.MergeToDestination<User>();

            await _userRepository.Update(model);

            return Tuple.Create(false, string.Empty);
        }
    }
}
