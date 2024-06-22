using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepSenseApp.Models
{
    public class ScheduleItem
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public DateTime ScheduledTime { get; set; }
        public string Status { get; set; }
        public string CleanerName { get; set; }
    }
}
