using Microsoft.AspNet.Identity;
using Online_Appointment.Models;
using Online_Appointment.Services;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Online_Appointment.Areas.Patient.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        public ApplicationDbContext context = new ApplicationDbContext();
        // GET: Patient/Patient
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var patientUser = context.Users.Single(u => u.Id == user);
            return View(patientUser);
        }

        public ActionResult AppointmentList()
        {
            //var roleId = context.Roles.Where(m => m.Name == "Patient").Select(m => m.Id).SingleOrDefault();
            //var patientUser = context.Users.Where(m => m.Roles.Any(r => r.RoleId == roleId)).ToList();
            //var result = context.Appointements.Include(d => d.Doctor).Where(p => p.PatientId == patientUser.Id).ToList();
            string user = User.Identity.GetUserId();
            var patientUser = context.Users.Single(u => u.Id == user);
            var result = context.Appointments.Include(d => d.Doctor).Where(p => p.PatientId == patientUser.Id).ToList();
            return View(result);
        }

        public ActionResult AddAppointment()
        {
            var doctors = context.Doctors.Include("ApplicationUser").ToList();
            ViewBag.users = doctors.Select(x =>
            new
            {
                x.Id,
                UserName = x.ApplicationUser.Firstname + " " + x.ApplicationUser.Lastname
            });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAppointment(Appointment model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var user = User.Identity.GetUserId();
                    var patient = context.Users.Single(a => a.Id == user);
                    Appointment app = new Appointment();
                    app.PatientId = patient.Id;
                    app.DoctorId = model.DoctorId;

                    app.AppointmentDate = model.AppointmentDate;
                    app.Time = model.Time;
                    app.Description = model.Description;
                    app.Status = false;
                    app.CreatorId = patient.Id;
                    app.UpdatedOn = System.DateTime.Now;
                    app.DeletedOn = System.DateTime.Now;
                    context.Appointments.Add(app);
                    context.SaveChanges();
                    TempData["AlertMessage"] = "Appointment added successfully !";
                    LoggerService.GetInstance().Info("Useri : " + Session["username"] + " dhe ip: " + Request.UserHostAddress + " ka krijuar nje appointment me id : " + app.Id);
                }
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occured !";
                LoggerService.GetInstance().Info("Perdoruesi : " + Session["username"] + " dhe ip: " + Request.UserHostAddress + " ka dashur te krijoj nje appointment por pa sukses , Gabimi:" + ex);
            }

            return RedirectToAction("AppointmentList");
            ModelState.Clear();
        }

        public ActionResult DeleteAppointment(int id)
        {
            try
            {
                var user = User.Identity.GetUserId();
                var appointment = context.Appointments.Single(a => a.Id == id);
                context.Appointments.Remove(appointment);
                context.SaveChanges();
                TempData["AlertMessage"] = "Appointment deleted successfully !";
                LoggerService.GetInstance().Info("Useri : " + Session["username"] + " dhe ip: " + Request.UserHostAddress + " ka fshire nje appointment me id : " + user);
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occured !";
                LoggerService.GetInstance().Info("Perdoruesi : " + Session["username"] + " dhe ip: " + Request.UserHostAddress + " ka dashur te fshije nje appointment por pa sukses , Gabimi:" + ex);

            }
            return RedirectToAction("AppointmentList");
        }



        [HttpPost]
        public ActionResult ValidateDateEqualOrGreater(DateTime AppointmentDate)
        {
            // validate your date here and return True if validated
            if (AppointmentDate.Date >= DateTime.Now.Date)
            {
                return Json(true);
            }
            return Json(false);
        }

    }
}