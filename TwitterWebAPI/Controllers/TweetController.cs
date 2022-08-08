using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterWebAPI.Contracts;
using TwitterWebAPI.Dtos;

namespace TwitterWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        private readonly ITweetService _tweetService;
        public TweetController(ITweetService tweetService)
        {
            this._tweetService = tweetService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _tweetService.GetAllTweets();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("userId")]
        public async Task<IActionResult> Get(int userId)
        {
            var response = await _tweetService.GetAllUserTweets(userId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTweet(AddTweetDto tweet)
        {
            var response = await _tweetService.AddTweet(tweet);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteTweet(int id)
        {
            var response = await _tweetService.DeleteTweet(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTweet(UpdateTweetDto tweet)
        {
            var response = await _tweetService.UpdateTweet(tweet);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("like")]
        public async Task<IActionResult> Like(LikeTweetDto tweet)
        {
            var response = await _tweetService.LikeTweet(tweet);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("reply")]
        public async Task<IActionResult> Reply(ReplyTweetDto tweet)
        {
            var response = await _tweetService.ReplyTweet(tweet);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
