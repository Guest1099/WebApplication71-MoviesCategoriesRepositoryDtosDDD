using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication71.DTOs;
using WebApplication71.DTOs.Account;
using WebApplication71.DTOs.Users;
using WebApplication71.Models;

namespace WebApplication71.Services.Abs
{
    public interface IAccountService
    {
        Task<ResultViewModel<GetUserDto>> GetUserById(string userId);
        Task<ResultViewModel<GetUserDto>> GetUserByEmail(string email);
        Task<ResultViewModel<CreateAccountDto>> CreateAccount(CreateAccountDto model);
        Task<ResultViewModel<UpdateAccountDto>> UpdateAccount(UpdateAccountDto model);
        Task<ResultViewModel<bool>> DeleteAccountByEmail(string email);
        Task<ResultViewModel<ChangeEmailDto>> ChangeEmail(ChangeEmailDto model);
        Task<ResultViewModel<ChangePasswordDto>> ChangePassword(ChangePasswordDto model);

        Task<ResultViewModel<ForgotPasswordDto>> ForgotPassword(ForgotPasswordDto model);
        Task<ResultViewModel<ResetPasswordDto>> ResetPassword (ResetPasswordDto model);

        Task<ResultViewModel<List<string>>> GetUserRoles(string email);
        Task<ResultViewModel<List<ApplicationUser>>> GetUsersInRole(string roleName);
        Task<ResultViewModel<bool>> UserInRole(string email, string roleName);
        Task<ResultViewModel<bool>> LoggedUserIsAdmin(string email);
        Task<ResultViewModel<LoginDto>> Login(LoginDto model);
        Task Logout(string email);
    }
}
