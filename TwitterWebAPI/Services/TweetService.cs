using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TwitterWebAPI.Contracts;
using TwitterWebAPI.Data;
using TwitterWebAPI.Dtos;
using TwitterWebAPI.Model;

namespace TwitterWebAPI.Services
{
    public class TweetService : ITweetService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public TweetService(IMapper mapper, DataContext dataContext)
        {
            this._mapper = mapper;
            this._dataContext = dataContext;
        }

        public async Task<ServiceResponse<int>> AddTweet(AddTweetDto tweetDto)
        {
            var serviceResponse = new ServiceResponse<int>();
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == tweetDto.UserId);
            var tweet = _mapper.Map<Tweet>(tweetDto);
            if(user != null)
            {
                tweet.User = user;
            }
            _dataContext.Tweets.Add(tweet);
            await _dataContext.SaveChangesAsync();
            serviceResponse.Data = tweet.Id;
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> DeleteTweet(int id)
        {
            var serviceResponse = new ServiceResponse<bool>();
            Tweet tweet = await _dataContext.Tweets.FirstOrDefaultAsync(t => t.Id == id);
            if (tweet == null)
            {
                serviceResponse.Success = false;
                return serviceResponse;
            }
            else
            {
                _dataContext.Tweets.Remove(tweet);
                await _dataContext.SaveChangesAsync();
                serviceResponse.Data = true;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTweetDao>>> GetAllTweets()
        {
            var serviceResponse = new ServiceResponse<List<GetTweetDao>>();
            var tweets = await _dataContext.Tweets.ToListAsync();
            serviceResponse.Data = tweets.Select(u => _mapper.Map<GetTweetDao>(u)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTweetDao>>> GetAllUserTweets(int userId)
        {
            var serviceResponse = new ServiceResponse<List<GetTweetDao>>();
            var tweets = await _dataContext.Tweets.Where(t => t.User.Id == userId).ToListAsync();
            serviceResponse.Data = tweets.Select(u => _mapper.Map<GetTweetDao>(u)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> UpdateTweet(UpdateTweetDto tweetDto)
        {
            var serviceResponse = new ServiceResponse<int>();
            Tweet updateTweet = await _dataContext.Tweets.FirstOrDefaultAsync(t => t.Id == tweetDto.Id);
            if (updateTweet == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Invalid tweet id";
                return serviceResponse;
            }
            else
            {
                var tweet = _mapper.Map(tweetDto, updateTweet);
                _dataContext.Tweets.Update(tweet);
                await _dataContext.SaveChangesAsync();
                serviceResponse.Data = tweet.Id;
                return serviceResponse;
            }    
        }

        public async Task<ServiceResponse<int>> LikeTweet(LikeTweetDto tweetDto)
        {
            var serviceResponse = new ServiceResponse<int>();
            Tweet tweet = await _dataContext.Tweets.FirstOrDefaultAsync(t => t.Id == tweetDto.TweetId);
            if (tweet == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Invalid tweet id";
                return serviceResponse;
            }

            User user = await _dataContext.Users.FirstOrDefaultAsync(t => t.Id == tweetDto.UserId);
            if (user == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Invalid user id";
                return serviceResponse;
            }
            LikeTweet likeTweet = new LikeTweet()
            {
                Tweet = tweet,
                User = user
            };
            _dataContext.LikeTweet.Add(likeTweet);
            await _dataContext.SaveChangesAsync();
            serviceResponse.Data = likeTweet.Id;
            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> ReplyTweet(ReplyTweetDto tweetDto)
        {
            var serviceResponse = new ServiceResponse<int>();
            Tweet tweet = await _dataContext.Tweets.FirstOrDefaultAsync(t => t.Id == tweetDto.TweetId);
            if (tweet == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Invalid tweet id";
                return serviceResponse;
            }

            User user = await _dataContext.Users.FirstOrDefaultAsync(t => t.Id == tweetDto.UserId);
            if (user == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Invalid user id";
                return serviceResponse;
            }
            TweetReply replyTweet = new TweetReply()
            {
                Tweet = tweet,
                User = user,
                Reply = tweetDto.Reply
            };
            _dataContext.TweetReply.Add(replyTweet);
            await _dataContext.SaveChangesAsync();
            serviceResponse.Data = replyTweet.Id;
            return serviceResponse;
        }
    }
}
