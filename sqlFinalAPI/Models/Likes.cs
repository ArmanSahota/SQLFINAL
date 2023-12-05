namespace sqlFinalAPI.Models
{
    public class Likes
    {
        public int LikeID { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
