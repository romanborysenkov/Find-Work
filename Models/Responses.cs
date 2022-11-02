using System;
using System.ComponentModel.DataAnnotations;

namespace FindWorkRazor.Models
{
    public class Responses
    {
        [Key]
        public int Id { get; set; }
        public string? WorkerId { get; set; }
        public int? VacancyId { get; set; }
    }
}

