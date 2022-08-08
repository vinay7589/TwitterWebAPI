using TwitterWebAPI.Dtos;
using TwitterWebAPI.Model;

namespace TwitterWebAPI.Contracts
{
    public interface ITweetService
    {
        Task<ServiceResponse<List<GetTweetDao>>> GetAllTweets();

        Task<ServiceResponse<List<GetTweetDao>>> GetAllUserTweets(int userId);

        Task<ServiceResponse<int>> AddTweet(AddTweetDto tweetDto);

        Task<ServiceResponse<int>> UpdateTweet(UpdateTweetDto tweetDto);

        Task<ServiceResponse<bool>> DeleteTweet(int id);

        Task<ServiceResponse<int>> LikeTweet(LikeTweetDto tweetDto);

        Task<ServiceResponse<int>> ReplyTweet(ReplyTweetDto tweetDto);
    }
}
