using Microsoft.EntityFrameworkCore;
using Task_Manager_Application.Data;
using Task_Manager_Application.Interfaces;
using Task_Manager_Application.Models;

namespace Task_Manager_Application.Services
{
    public class TaskServices : ITaskService
    {
        private readonly TaskDbContext _dbContext;

        public TaskServices(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<ProjectTask>> Add(ProjectTask task)
        {
            var response = new ApiResponse<ProjectTask>();

            try
            {
                _dbContext.ProjectTasks.Add(task);
                await _dbContext.SaveChangesAsync();

                response.Success = 1;
                response.Message = "Task added successfully.";
                response.Data = task;
            }
            catch (Exception ex)
            {
                response.Success = 0;
                response.Message = $"Failed to add task: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponse<bool>> Delete(long id)
        {
            var response = new ApiResponse<bool>();

            try
            {
                var task = await _dbContext.ProjectTasks.FindAsync(id);
                if (task == null)
                {
                    response.Success = 0;
                    response.Message = $"Task with ID {id} not found.";
                    return response;
                }

                _dbContext.ProjectTasks.Remove(task);
                await _dbContext.SaveChangesAsync();

                response.Success = 1;
                response.Message = "Task deleted successfully.";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = 0;
                response.Message = $"Failed to delete task: {ex.Message}";
                response.Data = false;
            }

            return response;
        }

        public async Task<ApiResponse<ProjectTask>> Get(long id)
        {
            var response = new ApiResponse<ProjectTask>();

            try
            {
                var task = await _dbContext.ProjectTasks.FirstOrDefaultAsync(t => t.Id == id);
                if (task == null)
                {
                    response.Success = 0;
                    response.Message = $"Task with ID {id} not found.";
                    return response;
                }

                response.Success = 1;
                response.Message = "Task retrieved successfully.";
                response.Data = task;
            }
            catch (Exception ex)
            {
                response.Success = 0;
                response.Message = $"Failed to retrieve task: {ex.Message}";
            }

            return response;
        }

        public async Task<ApiResponse<List<ProjectTask>>> GetAll()
        {
            var response = new ApiResponse<List<ProjectTask>>();

            try
            {
                var tasks = await _dbContext.ProjectTasks.AsNoTracking().ToListAsync();

                response.Success = 1;
                response.Message = "Tasks retrieved successfully.";
                response.Data = tasks;
            }
            catch (Exception ex)
            {
                response.Success = 0;
                response.Message = $"Failed to retrieve tasks: {ex.Message}";
            }

            return response;
        }

        public async Task<bool> TaskExists(long id)
        {
            return await _dbContext.ProjectTasks.AnyAsync(t => t.Id == id);
        }

        public async Task<ApiResponse<ProjectTask>> Update(long id, ProjectTask task)
        {
            var response = new ApiResponse<ProjectTask>();

            try
            {
                var existingTask = await _dbContext.ProjectTasks.FindAsync(id);
                if (existingTask == null)
                {
                    response.Success = 0;
                    response.Message = $"Task with ID {id} not found.";
                    return response;
                }

                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.DueDate = task.DueDate;

                _dbContext.Entry(task).State = EntityState.Modified;

                await _dbContext.SaveChangesAsync();

                response.Success = 1;
                response.Message = "Task updated successfully.";
                response.Data = task;
            }
            catch (Exception ex)
            {
                response.Success = 0;
                response.Message = $"Failed to update task: {ex.Message}";
            }

            return response;
        }
    }
}
