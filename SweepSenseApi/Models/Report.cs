namespace SweepSenseApi.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
