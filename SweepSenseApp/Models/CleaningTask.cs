using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepSenseApp.Models
{
    public class CleaningTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; } 
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
