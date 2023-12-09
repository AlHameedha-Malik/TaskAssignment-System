using System;
namespace TaskAssignmentSystem.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public List<Task> Tasks { get; set; }
    }
}

