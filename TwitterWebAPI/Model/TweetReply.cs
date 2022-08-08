using System.ComponentModel.DataAnnotations;

namespace TwitterWebAPI.Model
{
    public class TweetReply
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Reply { get; set; }
        public User? User { get; set; }
        public Tweet? Tweet { get; set; }
    }
}
