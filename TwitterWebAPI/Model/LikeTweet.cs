namespace TwitterWebAPI.Model
{
    public class LikeTweet
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public Tweet? Tweet { get; set; }    
    }
}
