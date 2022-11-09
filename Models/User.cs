using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindWorkRazor.Models
{
    public class User
    {
        public string? Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string firstname { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string secondname { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string? salt { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare(nameof(salt), ErrorMessage = "Password and confirmation password did not match")]
        public string? saltedhashedpassword { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string? phone { get; set; } = string.Empty;

        public bool isEmployer { get; set; }

    }
}

