﻿namespace SweepSenseApi.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RoomId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; } 
        public DateTime Date { get; set; }
    }
}
