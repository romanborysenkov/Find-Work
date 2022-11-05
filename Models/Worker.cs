using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FindWorkRazor.Models
{
    public class Worker
    {
       
        public string? workerId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string firstname { get; set; } = string.Empty;

         [Column(TypeName ="nvarchar(50)")]
        [Required]
        public string secondname { get; set; } = string.Empty;

        [Required]
        public int age { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string? salt {get; set;}=string.Empty;
       
        [DataType(DataType.Password)]
        [Compare(nameof(salt), ErrorMessage = "Password and confirmation password did not match")]
        public string? saltedhashedpassword { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string? workerphone { get; set; } = string.Empty;

        public string? desirework { get; set; } = string.Empty;

        public int desiresalary { get; set; }

        public string? employmentDegree { get; set; }


        public string? education { get; set; } = string.Empty;

        public string? educationDegree { get; set; }

        public int? graduationYear { get; set; }

        public int expirience { get; set; }

        public string? skills { get; set; } = string.Empty;

        public string? languages { get; set; }

        public string? Location { get; set; } = string.Empty;

        public string? photoName { get; set; }

        [NotMapped]
        public IFormFile? photoFile { get; set; }

        public string? photoSrc { get; set; }

         //   [Column(TypeName = "nvarchar(50)")]
    }
}

