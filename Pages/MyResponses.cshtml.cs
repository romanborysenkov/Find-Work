using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindWorkRazor.Data;
using FindWorkRazor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FindWorkRazor.Pages
{
    public class MyResponsesModel : PageModel
    {
        private readonly DataContext _context;

        public MyResponsesModel(DataContext context)
        {
            _context = context;
        }
     

        public List<Vacancy> finalList { get; set; } = new List<Vacancy>();//{ get; set; }

       public User worker = new User();

        public async void OnGet()
        {
            worker = await _context.users.FirstAsync(s => s.email == User.Identity.Name);

          var  vacancies =await _context.responses.Where(x=>x.WorkerId==worker.Id).ToListAsync();

           // vacancies =await vacancies2.ToListAsync();

            for (int i = 0; i < vacancies.Count; i++)
            {
                finalList.Add(await  _context.vacancies.FirstAsync(x => x.vacancyId == vacancies[i].VacancyId));
            }
               // finalList.Add(await _context.vacancies.FirstAsync(x => x.vacancyId == vacancies[i].VacancyId));
            
           
            //vacancies =  _context.responses.Where(x=>x.WorkerId==worker.workerId).ToListAsync();

        }
    }
}
