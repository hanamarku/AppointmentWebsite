using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Appointment.Areas.Patient.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient/Patient
        public ActionResult Index()
        {
            return View();
        }
    }
}