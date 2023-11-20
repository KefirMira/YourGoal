using System;

namespace YourGoal.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateDelete { get; set; }
        public Folder Folder { get; set; }
        public User User { get; set; }
        public Priority Priority { get; set; }
        public bool? Accomplishment { get; set; }

    }
}