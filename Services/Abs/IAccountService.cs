using System.Threading.Tasks;
using WebApplication71.DTOs;
using WebApplication71.DTOs.Users;

namespace WebApplication71.Services.Abs
{
    public interface IAccountService
    {
        Task<ResultViewModel<GetUserDto>> GetUserById(string userId);
        Task<ResultViewModel<GetUserDto>> GetUserByEmail(string email);
        Task<ResultViewModel<CreateUserDto>> CreateAccount(CreateUserDto model);
        Task<ResultViewModel<EditUserDto>> UpdateAccount(EditUserDto model);
        Task<ResultViewModel<bool>> DeleteAccount(string userId);
        Task<ResultViewModel<bool>> Login(string userId);
        Task<ResultViewModel<bool>> Logout(string userId);
    }
}
