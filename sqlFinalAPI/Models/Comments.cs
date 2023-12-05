namespace sqlFinalAPI.Models
{
    public class Comments
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
