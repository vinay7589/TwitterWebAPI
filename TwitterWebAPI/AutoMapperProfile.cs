using AutoMapper;
using TwitterWebAPI.Dtos;
using TwitterWebAPI.Model;

namespace TwitterWebAPI
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<AddTweetDto, Tweet>();
            CreateMap<UpdateTweetDto, Tweet>();
            CreateMap<Tweet, GetTweetDao>();
        }
    }
}
