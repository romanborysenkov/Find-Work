using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using FindWorkRazor.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FindWorkRazor.Data
{
    public class DataContext: IdentityDbContext
        {
            public DataContext(DbContextOptions<DataContext>options):base(options)
            {  }
       
           public DbSet<Worker>? workers { get; set; }
           public DbSet<Vacancy> vacancies { get; set; }
           
           public DbSet<Responses> responses { get; set; }
        }
}