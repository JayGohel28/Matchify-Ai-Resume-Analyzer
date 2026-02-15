using System;
using System.ComponentModel.DataAnnotations;

namespace MatchifyAI.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        public string ExperienceLevel { get; set; }

        [Required]
        public string RequiredSkills { get; set; }

        [Required]
        public string JobDescription { get; set; }

        public string JobLocation { get; set; }

        public string EmploymentType { get; set; }

        public string CompanyName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
