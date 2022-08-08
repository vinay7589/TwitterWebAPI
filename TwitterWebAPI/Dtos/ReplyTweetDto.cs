namespace TwitterWebAPI.Dtos
{
    public class ReplyTweetDto
    {
        public string Reply { get; set; }
        public int TweetId { get; set; }
        public int UserId { get; set; }
    }
}
