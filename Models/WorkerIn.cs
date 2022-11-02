using System;
using System.ComponentModel.DataAnnotations;

namespace FindWorkRazor.Models
{
    public class WorkerIn
    {
        [Required]
        public string name { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        public string salt { get; set; }
    }
}

