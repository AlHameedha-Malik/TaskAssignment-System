using System;
namespace TaskAssignmentSystem.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }

}

