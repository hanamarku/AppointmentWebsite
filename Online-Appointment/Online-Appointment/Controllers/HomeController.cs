using System.Data.Entity;
using Online_Appointment.Models;
using Online_Appointment.Services;
using System.Linq;
using System.Web.Mvc;

namespace Online_Appointment.Controllers
{
    public class HomeController : Controller
    {

        public ApplicationDbContext context = new ApplicationDbContext();

        private ILogger _logger;
        public HomeController(ILogger logger)
        {
            _logger = logger;
        }
        public ActionResult Index()
        {
            ViewBag.departments = context.Departments.ToList();
            ViewBag.doctors = context.Doctors.Include(d => d.Departament).ToList();
            //_logger.Info("kkkttt", "kkk", "kkk", "kkk");
            LoggerService.GetInstance().Info("test");
            return View();
        }
        // public ActionResult HomeIndex()
        // {
        //     return View();
        // }
        
        public ActionResult DepDetails(int? id)
        {
            using (var context = new ApplicationDbContext())
            {
                var data = context.Doctors.Include(d => d.ApplicationUser);
                return View(context.Departments.Where(x => x.DepId == id).Include(d => d.Doctors).FirstOrDefault());
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}