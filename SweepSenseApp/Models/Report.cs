using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepSenseApp.Models
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
