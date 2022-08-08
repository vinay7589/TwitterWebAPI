using Microsoft.EntityFrameworkCore;
using TwitterWebAPI.Model;

namespace TwitterWebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<LikeTweet> LikeTweet { get; set; }
        public DbSet<TweetReply> TweetReply { get; set; }
    }
}
