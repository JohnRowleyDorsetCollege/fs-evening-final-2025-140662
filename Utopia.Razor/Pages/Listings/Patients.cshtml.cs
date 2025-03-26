using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Utopia.Razor.Data;
using Utopia.Razor.Models;

namespace Utopia.Razor.Pages.Listings
{
    public class PatientsModel : PageModel
    {
        private readonly Utopia.Razor.Data.ApplicationDbContext _context;

        public PatientsModel(Utopia.Razor.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<DoctorPatient> DoctorPatient { get;set; } = default!;

        public async Task OnGetAsync()
        {
            DoctorPatient = await _context.DoctorPatient
                .Include(d => d.Doctor)
                .Include(d => d.Patient).ToListAsync();
        }
    }
}
