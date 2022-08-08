using System.ComponentModel.DataAnnotations;

namespace TwitterWebAPI.Model
{
    public class Tweet
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Detail { get; set; }
        public User? User { get; set; }
        public List<LikeTweet> LikeTweets { get; set; }
        public List<TweetReply> TweetReplys { get; set; }

    }
}
