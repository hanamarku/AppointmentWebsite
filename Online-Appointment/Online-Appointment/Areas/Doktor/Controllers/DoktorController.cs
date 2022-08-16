using Microsoft.AspNet.Identity;
using Online_Appointment.Models;
using Online_Appointment.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;

namespace Online_Appointment.Areas.Doktor.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoktorController : Controller
    {
        public ApplicationDbContext context = new ApplicationDbContext();
        // GET: Doktor/Doktor
        public ActionResult Index()
        {
            string user = User.Identity.GetUserId();
            var doctorUser = context.Users.Single(u => u.Id == user);
            
            return View(doctorUser);
        }

        public ActionResult AppointmentList()
        {
            //var roleId = context.Roles.Where(m => m.Name == "Patient").Select(m => m.Id).SingleOrDefault();
            //var patientUser = context.Users.Where(m => m.Roles.Any(r => r.RoleId == roleId)).ToList();
            //var result = context.Appointements.Include(d => d.Doctor).Where(p => p.PatientId == patientUser.Id).ToList();
            string user = User.Identity.GetUserId();
            var doctorUser = context.Users.Single(u => u.Id == user);
            var result = context.Appointments.Where(p => p.DoctorId == doctorUser.Id).ToList();
            return View(result);
        }
        //var roleId = context.Roles.Where(m => m.Name == "Patient").Select(m => m.Id).SingleOrDefault();
        //var patientUser = context.Users.Where(m => m.Roles.Any(r => r.RoleId == roleId)).ToList();
        private readonly UserManager<ApplicationUser> _userManager;
        public ActionResult ApprovedAppointments()
        {
            string user = User.Identity.GetUserId();
            var doctorUser = context.Doctors.Single(u => u.Id == user);

            //_userManager.GetUsersInRoleAsync("Patient").Result;
            var date = System.DateTime.Now.Date;
            var result = context.Appointments.Where(p => p.DoctorId == doctorUser.Id).Where(s => s.Status == true).Where(x => x.AppointmentDate >= date).ToList();
            return View(result);
        }

        public ActionResult PendingAppointments()
        {
            string user = User.Identity.GetUserId();
            var doctorUser = context.Doctors.Single(u => u.Id == user);
            var date = DateTime.Now.Date;
            var result = context.Appointments.Where(p => p.DoctorId == doctorUser.Id).Where(s => s.Status == false).Where(x => x.AppointmentDate >= date).ToList();

            return View(result);

        }



        public void SendEmail(string from, string to, string subject, string body)
        {

            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                MailMessage mailMessage = new MailMessage(from, to, subject, body);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);
                TempData["AlertMessage"] = "Email sent successfully !";
                LoggerService.GetInstance().Info("Useri : " + senderEmail + " ka derguar email tek : " + to);

            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occured !";
                LoggerService.GetInstance().Info("Useri : " + from + " dhe ip: " + Request.UserHostAddress + " ka dashur te dergoje nje email per tek useri " + to + " por pa sukses , Gabimi:" + ex);
            }
        }


        [HttpGet]
        public ActionResult EditAppointment(int id)
        {
            if (id == null)
                return HttpNotFound();
            var result = context.Appointments.FirstOrDefault(m => m.Id == id);
            if (result == null)
                return HttpNotFound();
            return View(result);
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public ActionResult EditAppointment(int id, Appointment model)
        //{

        //    var appointement = context.Appointments.Single(a => a.Id == id);
        //    appointement.Status = model.Status;
        //    var emailSentTo = appointement.User.Email;
        //    var patientName = appointement.User.Firstname;
        //    var doctorName = appointement.Doctor.ApplicationUser.Firstname;
        //    var doctorLastname = appointement.Doctor.ApplicationUser.Lastname;
        //    var appDate = appointement.AppointmentDate;

        //    context.SaveChanges();
        //    if (model.Status == true)
        //    {
        //        SendEmail("testhhospital@gmail.com", emailSentTo, "Appoitnment Status", " Dear " + patientName + "  <br /> Your appointment with " + doctorName + " " + doctorLastname + "  scheduled for " + appDate + " has been confirmed " + "   Thank you for choosing our hospital ");
        //        return RedirectToAction("ApprovedAppointments");
        //    }
        //    else
        //    {
        //        SendEmail("testhhospital@gmail.com", emailSentTo, "Appoitnment Status", " Dear " + patientName + "  <br /> Your appointment with " + doctorName + " " + doctorLastname + "  scheduled for " + appDate + " has been cancelled " + "   Thank you for choosing our hospital ");
        //        return RedirectToAction("PendingAppointments");

        //    }

        //}


        bool Declined = false;

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        public ActionResult AcceptAppointment(int? id)
        {
            string user = User.Identity.GetUserId();
            if (id == null)
                return HttpNotFound();
            try
            {

                var appointement = context.Appointments.Single(a => a.Id == id);
                appointement.Status = true;
                var emailSentTo = appointement.User.Email;
                var patientName = appointement.User.Firstname;
                var patientLastname = appointement.User.Lastname;
                var doctorName = appointement.Doctor.ApplicationUser.Firstname;
                var doctorLastname = appointement.Doctor.ApplicationUser.Lastname;
                var appDate = appointement.AppointmentDate;
                SendEmail("testhhospital@gmail.com", emailSentTo, "Appointment Status", " Dear " + patientName + "  <br /> Your appointment with dr" + doctorName + " " + doctorLastname + "  scheduled for " + appDate + " has been confirmed " + "   Thank you for choosing our hospital ");

                context.SaveChanges();
                TempData["AlertMessage"] = "Appointment with " + patientName + " " + patientLastname + " has been successfully confirmed !";
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occured !";
                LoggerService.GetInstance().Info("Useri me id: " + user + " dhe ip: " + Request.UserHostAddress + " nuk ka pranuar me sukses appoitnment me id " + id + " Gabimi:" + ex);
            }
            return RedirectToAction("PendingAppointments");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeclineAppointment(int? id)
        {
            string user = User.Identity.GetUserId();
            if (id == null)
                return HttpNotFound();
            try
            {


                var appointement = context.Appointments.Single(a => a.Id == id);
                appointement.Status = false;
                Declined = true;
                var emailSentTo = appointement.User.Email;
                var patientName = appointement.User.Firstname;
                var patientLastname = appointement.User.Lastname;
                var doctorName = appointement.Doctor.ApplicationUser.Firstname;
                var doctorLastname = appointement.Doctor.ApplicationUser.Lastname;
                var appDate = appointement.AppointmentDate;
                SendEmail("testhhospital@gmail.com", emailSentTo, "Appointment Status", " Dear " + patientName + "  <br /> Your appointment with dr" + doctorName + " " + doctorLastname + "  scheduled for " + appDate + " has been cancelled " + " You will get notified for further information. Thank you for choosing our hospital ");

                context.SaveChanges();
                TempData["AlertMessage"] = "Appointment with " + patientName + " " + patientLastname + " has been successfully declined!";
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occured !";
                LoggerService.GetInstance().Info("Useri me id: " + user + " dhe ip: " + Request.UserHostAddress + " nuk ka fshire me sukses appoitnment me id " + id + " Gabimi:" + ex);
            }

            return RedirectToAction("ApprovedAppointments");
        }

        public ActionResult GetUsers()
        {
            List<test> doctors = new List<test>();
            doctors.Add(new test() { name = "hana", lastname = "marku" });
            doctors.Add(new test() { name = "mira", lastname = "mira" });
            return View(doctors);
        }



        [HttpGet]
        public ActionResult AddPrescription(int id)
        {
            //if (id == null)
            //    return HttpNotFound();
            ////var result = context.Medicines.FirstOrDefault(m => m.Id == id);
            var appointement = context.Appointments.Single(a => a.Id == id);
            var patientId = appointement.PatientId;
            var doctorId = appointement.DoctorId;
            var doctors = context.Doctors.Where(x => x.Id == doctorId).ToList();
            ViewBag.userDoctor = User.Identity.GetUserId();
            ViewBag.userPatient = appointement.PatientId;
            ViewBag.appDate = appointement.AppointmentDate;



            // //var doctorUser = context.Users.Single(u => u.Id == user);

            // ViewBag.doctor = doctors;

            // ViewBag.patient = context.Users.Where(x => x.Id == patientId).Select(x => x.UserName);
            // ViewBag.users = doctors.Select(x =>
            //new
            //{
            //    x.Id,
            //    UserName = x.ApplicationUser.Firstname + " " + x.ApplicationUser.Lastname
            //});
            //var patientId = appointement.PatientId;
            //var patientfull = context.Users.Where(a => a.Id == patientId).ToList();


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddPrescription(Prescription model, int id)
        {


            if (ModelState.IsValid)
            {
                string user = User.Identity.GetUserId();
                Prescription prescription = new Prescription();
                prescription.AppointmentId = id;
                prescription.PrescriptionDate = DateTime.Now;
                prescription.Description = model.Description;

                context.Prescriptions.Add(prescription);
                context.SaveChanges();


                return new Rotativa.ActionAsPdf("AddPrescription");
                ModelState.Clear();
            }
            return View(model);

        }

        public ActionResult PresriptionList(int? id)
        {
            var result = context.Prescriptions.Where(x => x.AppointmentId == id).FirstOrDefault();
            return View(result);
        }

        public ActionResult GeneratePDF(int? id)
        {
            return new Rotativa.ActionAsPdf("PresriptionList");
        }


        [HttpGet]
        public ActionResult AddMedicine()
        {
            return View();
        }





        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public ActionResult AddMedicine(Medicine model, int id)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //        Prescription prescription = new Prescription();
        //        prescription.AppointmentId = id;


        //        prescription.PrescriptionDate = DateTime.Now;



        //        context.SaveChanges();

        //        var med = new Medicine()
        //        {
        //            Name = model.Name,
        //            Description = model.Description,
        //            Price = model.Price,
        //            Quantity = model.Quantity,
        //            TimeToTakeMedicine = model.TimeToTakeMedicine
        //        };
        //        var presId = context.Prescriptions.Where(x => x.AppointmentId == prescription.AppointmentId).Select(x => x.Id);


        //        var medicine = context.Prescriptions.FirstOrDefault(a => a.Id == presId);
        //        medicine.Medicines.Add(med);
        //        context.SaveChanges();
        //        return RedirectToAction("ApprovedAppointment");
        //        ModelState.Clear();
        //    //}
        //    return View(model);

        //}
    }
}