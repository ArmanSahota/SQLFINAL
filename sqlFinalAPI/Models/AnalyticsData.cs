namespace sqlFinalAPI.Models
{
    public class AnalyticsData
    {
        public int AnalyticsID { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
        public int ViewsCount { get; set; }
        public DateTime CreationDateTime { get; set; }
        public int ShareCount { get; set; }
    }
}
