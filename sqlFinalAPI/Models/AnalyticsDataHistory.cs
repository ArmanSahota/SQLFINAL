namespace sqlFinalAPI.Models
{
    public class AnalyticsDataHistory
    {
        public int HistoryID { get; set; }
        public int AnalyticsID { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
        public int ViewsCount { get; set; }
        public int SharesCount { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ActionTaken { get; set; } // e.g., "UPDATE", "DELETE"
    }

}
