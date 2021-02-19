using Infra.Services.PDFServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class HomeController : Controller
    {
         
        [HttpGet]
        [Route("/")]
        [AllowAnonymous]
        public  ActionResult Home ()
        {
            var pdf = new PDFWriter();
            pdf.Build("teste", "teste");
            return Redirect("/swagger/index.html");
        }
         
    }
}