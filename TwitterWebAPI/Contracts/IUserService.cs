using TwitterWebAPI.Dtos;
using TwitterWebAPI.Model;

namespace TwitterWebAPI.Contracts
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();

        Task<ServiceResponse<GetUserDto>> SearchUserByName(string userName);
    }
}
