using System;
using System.Collections.Generic;

namespace YourGoal.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Folder Folder { get; set; }
        public string RelationshipTasks { get; set; }
        public int RemainingDays { get; set; }
        //public List<Task> Tasks { get; set; }
    }
}