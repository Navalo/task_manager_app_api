using Microsoft.EntityFrameworkCore;
using Task_Manager_Application.Models;

namespace Task_Manager_Application.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) 
            : base(options)
        {
            
        }

        public DbSet<ProjectTask> ProjectTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectTask>()
                .HasKey(t => t.Id);
        }

    }
}
