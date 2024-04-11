using Task_Manager_Application.Models;

namespace Task_Manager_Application.Interfaces
{
    public interface ITaskService
    {
        Task<ApiResponse<List<ProjectTask>>> GetAll();
        Task<ApiResponse<ProjectTask>> Get(long id);
        Task<ApiResponse<ProjectTask>> Add(ProjectTask obj);
        Task<ApiResponse<ProjectTask>> Update(long id, ProjectTask obj);
        Task<ApiResponse<bool>> Delete(long id);
        Task<bool> TaskExists(long id);
    }
}
