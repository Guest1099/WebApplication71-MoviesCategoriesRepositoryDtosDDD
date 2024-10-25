using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication71.DTOs;
using WebApplication71.DTOs.Roles;
using WebApplication71.DTOs.Users;

namespace WebApplication71.Services.Abs
{
    public interface IRolesService
    {
        Task<ResultViewModel<List<GetRoleDto>>> GetAll();
        Task<ResultViewModel<GetRoleDto>> Get(string roleId);
        Task<ResultViewModel<CreateRoleDto>> Create(CreateRoleDto model);
        Task<ResultViewModel<EditRoleDto>> Update(EditRoleDto model);
        Task<ResultViewModel<bool>> Delete(string roleId);
        Task<List<GetUserDto>> UsersInRole(string roleName);
    }
}
