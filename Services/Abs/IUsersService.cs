using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication71.DTOs;
using WebApplication71.DTOs.Users;

namespace Application.Services.Abs
{
    public interface IUsersService
    {
        Task<ResultViewModel<List<GetUserDto>>> GetAll();
        Task<ResultViewModel<GetUserDto>> GetUserById(string userId);
        Task<ResultViewModel<GetUserDto>> GetUserByEmail(string email);
        Task<ResultViewModel<CreateUserDto>> Create(CreateUserDto model);
        Task<ResultViewModel<EditUserDto>> Update(EditUserDto model);
        Task<ResultViewModel<bool>> Delete(string userId);
    }
}
