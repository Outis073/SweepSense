using System.ComponentModel.DataAnnotations;

namespace SweepSenseApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public ICollection<CleaningTask> CleaningTasks { get; set; } = new List<CleaningTask>();
    }
}
