using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Manager_Application.Models
{
    public class ProjectTask
    {
        public long Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }
    }
}
