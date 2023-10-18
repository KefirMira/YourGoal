using System;

namespace YourGoal.Models
{
    public class Tracker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateDelete { get; set; }
        public User User { get; set; }
        public Folder Folder { get; set; }
    }
}