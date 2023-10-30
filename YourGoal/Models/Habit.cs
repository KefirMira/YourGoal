using System;

namespace YourGoal.Models
{
    public class Habit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public User User { get; set; }
    }
}