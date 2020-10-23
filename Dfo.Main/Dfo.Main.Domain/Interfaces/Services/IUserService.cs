using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dfo.Main.Domain.Dto;

namespace Dfo.Main.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<Tuple<bool, string>> AddUser(UserDto dto);
        Task<Tuple<bool, string>> UpdateUser(UserDto dto);
        Task<Tuple<bool, List<UserDto>, string>> GetUsers(string name);
        Task<Tuple<bool, UserDto, string>> GetUser(int id);
    }
}
