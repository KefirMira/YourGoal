using System.Collections.Generic;

namespace YourGoal.Models
{
    public class TaskOnGoal
    {
        public int Id { get; set; }
        public Task Task { get; set; }
        public Goal Goal { get; set; }
    }
}