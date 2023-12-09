using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskAssignmentSystem.Models;

namespace TaskAssignmentSystem
{
	public class TaskContext : DbContext
	{
	
        public DbSet<Models.Task> Task { get; set; }
        public DbSet<Employee> Employee { get; set; }

        public TaskContext()
        {

        }

        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeId);

            modelBuilder.Entity<Models.Task>()
                .HasKey(t => t.TaskId);

            modelBuilder.Entity<Models.TaskGetModel>()
                .HasNoKey();

            modelBuilder.Entity<Models.Task>()
                .HasOne(t => t.Employee)
                .WithMany(e => e.Tasks)
                .HasForeignKey(t => t.EmployeeId);


        }

    }

}

