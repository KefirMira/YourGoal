using System;

namespace YourGoal.Models
{
    public class Tracker
    {
        public int Id { get; set; }
        public int HabitId { get; set; }
        public DateTime DateOfCompletion { get; set; }
        public bool Accomplishment { get; set; }
    }
}