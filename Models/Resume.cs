using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindWorkRazor.Models
{
    public class Resume
    {
        [Key]
        public int? Id { get; set; }

        public int age { get; set; }

        public string? desireWork { get; set; }

        public string? desireSalary { get; set; }

        public string? employmentDegree { get; set; }

        public string? education { get; set; }

        public string? educationDegree { get; set; }

        public int graduationYear { get; set; }

        public int expirience { get; set; }

        public DateTime publishTime { get; set; }

        public string? skills { get; set; }

        public string? languages { get; set; }

        public string? Location { get; set; } = string.Empty;

        public string? userId { get; set; }

        public string? photoName { get; set; }

        [NotMapped]
        public IFormFile? photoFile { get; set; }

        public string? photoSrc { get; set; }
    }
}

