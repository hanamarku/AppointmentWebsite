using System.Web.Mvc;

namespace Online_Appointment.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        // GET: Users
        //public ActionResult Index()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var user = User.Identity;
        //        ViewBag.UserName = user.UserName;
        //    }
        //    return View();
        //}


    }
}