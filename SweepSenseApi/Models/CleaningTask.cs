namespace SweepSenseApi.Models
{
    public class CleaningTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string RoomId { get; set; }
        public string LocationName { get; set; } 
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
