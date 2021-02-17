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
            return Redirect("/swagger/index.html");
        }
         
    }
}