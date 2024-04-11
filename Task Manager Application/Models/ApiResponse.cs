namespace Task_Manager_Application.Models
{
    public class ApiResponse<T>
    {
        public int Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }

}
