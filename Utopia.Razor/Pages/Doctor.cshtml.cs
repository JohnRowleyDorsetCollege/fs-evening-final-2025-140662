using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Utopia.Razor.Pages
{

    [Authorize(Roles = "Doctor")]
    public class DoctorModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
