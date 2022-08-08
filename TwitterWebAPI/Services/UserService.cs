using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TwitterWebAPI.Contracts;
using TwitterWebAPI.Data;
using TwitterWebAPI.Dtos;
using TwitterWebAPI.Model;

namespace TwitterWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public UserService(IMapper mapper, DataContext dataContext)
        {
            this._mapper = mapper;
            this._dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            var users = await _dataContext.Users.ToListAsync();
            serviceResponse.Data = users.Select(u => _mapper.Map<GetUserDto>(u)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> SearchUserByName(string userName)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Username == userName);
            if (user != null)
            {
                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
                serviceResponse.Success = true;
            }
            else
            {
                serviceResponse.Message = "User does not exist";
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }
    }
}
