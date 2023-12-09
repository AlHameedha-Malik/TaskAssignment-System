using Microsoft.AspNetCore.Mvc;
using System.Data;
using TaskAssignmentSystem.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskAssignmentSystem.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet("Employees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            return await _context.Employee.ToListAsync();
        }

        [HttpGet("GetAllTasks")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllTasks()
        {

            var data = _context.Task
        .GroupJoin(
            _context.Employee,
            t => t.EmployeeId,
            e => e.EmployeeId,
            (task, employee) => new
            {
                TaskName = task.TaskName,
                TaskDescription = task.TaskDescription,
                EmployeeName = employee.Select(e => e.EmployeeName).FirstOrDefault()
            })
        .ToList();

            return Ok(data);


        }


        [HttpPost]
        public async Task<IActionResult> PostTask([FromBody] TaskCreate taskInput)
        {
            try
            {
                var employee = await _context.Employee
                    .FirstOrDefaultAsync(e => e.EmployeeName == taskInput.EmployeeName);

                if (employee == null)
                {
                    return NotFound($"Employee with name {taskInput.EmployeeName} not found.");
                }

                
                await _context.Database
                    .ExecuteSqlRawAsync("EXEC InsertTask @p0, @p1, @p2",
                        taskInput.TaskName, taskInput.TaskDescription, taskInput.EmployeeName);

                return Ok("Task inserted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

    }
}

