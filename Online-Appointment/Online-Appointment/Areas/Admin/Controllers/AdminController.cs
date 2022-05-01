using Online_Appointment.CollectionOfModels;
using Online_Appointment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Online_Appointment.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public Online_Appointment.Models.HmsDbContext DbContext { get; set; }
        public AdminController()
        {
            DbContext = new Models.HmsDbContext();
        }

        public ActionResult Index()
        {
            var model = new DashboardCollection
            {
                Departments = DbContext.Departments.ToList()

            };
            return View(model);
        }

        //Medicine

        public ActionResult MedicineList()
        {
            var result = DbContext.Medicines.ToList();
            return View(result);
        }

        [HttpGet]
        public ActionResult AddMedicine()
        {
            return View();
        }

        [HttpPost]

        public ActionResult AddMedicine(Medicine model)
        {
            if (ModelState.IsValid)
            {
                Medicine med = new Medicine();
                med.Name = model.Name;
                med.Description = model.Description;
                med.Price = model.Price;
                med.Quantity = model.Quantity;
                med.TimeToTakeMedicine = model.TimeToTakeMedicine;
                DbContext.Medicines.Add(med);
                DbContext.SaveChanges();
            }
            return RedirectToAction("MedicineList");
            ModelState.Clear();
        }

        [HttpGet]
        public ActionResult EditMedicine(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var result = DbContext.Medicines.FirstOrDefault(m => m.Id == id);
            if (result == null)
                return HttpNotFound();
            return View(result);
        }

        [HttpPost]

        public ActionResult EditMedicine(int id, Medicine model)
        {
            var medicine = DbContext.Medicines.FirstOrDefault(m => m.Id == id);
            medicine.Name = model.Name;
            medicine.Description = model.Description;
            medicine.Price = model.Price;
            medicine.Quantity = model.Quantity;
            medicine.TimeToTakeMedicine = model.TimeToTakeMedicine;
            DbContext.SaveChanges();
            return RedirectToAction("MedicineList");

        }

        public ActionResult DeleteMedicine(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var medicine = DbContext.Medicines.FirstOrDefault(m => m.Id == id);
            if (medicine == null)
                return HttpNotFound();
            DbContext.Medicines.Remove(medicine);
            DbContext.SaveChanges();

            return RedirectToAction("MedicineList");
        }



        //Departament

        public ActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddDepartment([Bind(Include = "DepId, Name, Description, ImageURL, File")] Department department)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileName(department.File.FileName);
                string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                string path = Path.Combine(Server.MapPath("~/Images/Dep_Images/"), _filename);
                department.ImageURL = "~/Images/Dep_Images/" + _filename;
                DbContext.Departments.Add(department);
                if (department.File.ContentLength < 500000)
                {
                    if (DbContext.SaveChanges() > 0)
                    {
                        department.File.SaveAs(path);
                        TempData["AlertMessage"] = "Department added successfully !";
                    }
                    return RedirectToAction("DepartmentsList");
                }
                else
                {
                    ViewBag.msg = "File must be less or equal to 5 mb";
                }
            }

            return View(department);

        }

        [HttpGet]
        public ActionResult EditDepartment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = DbContext.Departments.FirstOrDefault(d => d.DepId == id);
            Session["imgPath"] = result.ImageURL;
            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDepartment([Bind(Include = "DepId, Name, Description, ImageURL, File")] Department department)
        {
            if (ModelState.IsValid)
            {
                if (department.File != null)
                {
                    string filename = Path.GetFileName(department.File.FileName);
                    string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                    string path = Path.Combine(Server.MapPath("~/Images/Dep_Images/"), _filename);
                    department.ImageURL = "~/Images/Dep_Images/" + _filename;

                    //if (department.File.ContentLength < 500000)
                    //{
                    DbContext.Entry(department).State = EntityState.Modified;
                    string oldImgPath = Request.MapPath(Session["imgPath"].ToString());
                    if (DbContext.SaveChanges() > 0)
                    {

                        department.File.SaveAs(path);
                        if (System.IO.File.Exists(oldImgPath))
                        {
                            System.IO.File.Delete(oldImgPath);
                        }

                    }
                    return RedirectToAction("DepartmentsList");
                    //}
                    //else
                    //{
                    //    ViewBag.msg = "File must be less or equal to 5 mb";
                    //}
                }
                else
                {
                    department.ImageURL = Session["imgPath"].ToString();
                    DbContext.Entry(department).State = EntityState.Modified;
                    DbContext.SaveChanges();
                    TempData["AlertMessage"] = "Department edited successfully !";
                    return RedirectToAction("DepartmentsList");
                }

            }
            return View(department);
        }



        public ActionResult DepartmentsList()
        {
            using (var context = new HmsDbContext())
            {
                var dep = context.Departments;
                List<Department> Deps = new List<Department>();
                foreach (var d in dep)
                {
                    Deps.Add(d);
                }
                return View(Deps);
            }
        }

        public ActionResult DepartmentDetails(int? id)
        {
            using (var context = new HmsDbContext())
            {
                return View(context.Departments.Where(x => x.DepId == id).FirstOrDefault());
            }

        }

        public ActionResult DeleteDepartment(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var department = DbContext.Departments.FirstOrDefault(m => m.DepId == id);
            if (department == null)
            {
                return HttpNotFound();
            }
            string removingPath = Request.MapPath(department.ImageURL);
            DbContext.Departments.Remove(department);
            if (DbContext.SaveChanges() > 0)
            {
                if (System.IO.File.Exists(removingPath))
                {
                    System.IO.File.Delete(removingPath);
                }
                TempData["AlertMessage"] = "Department deleted successfully !";
            }
            return RedirectToAction("DepartmentsList");
        }

        public JsonResult NameExists(string Name)
        {
            return Json(!DbContext.Departments.Any(x => x.Name == Name), JsonRequestBehavior.AllowGet);
        }


        //[HttpGet]

        //public ActionResult AddAppointment()
        //{

        //}

        //[HttpPost]

        //public ActionResult AddAppointment()
        //{

        //}

        //public ActionResult ActiveAppointment()
        //{
        //    var date = DateTime.Now.Date;
        //    var appointment = DbContext.Appointements.Where(d => d.Status == true)
        //        .Where(d => d.AppointmentDate >= date).ToList();
        //    return View(appointment);
        //}

        //public ActionResult PendingAppointment()
        //{
        //    var date = DateTime.Now.Date;
        //    var appointment = DbContext.Appointements.Where(d => d.Status == false)
        //        .Where(d => d.AppointmentDate >= date).ToList();
        //    return View(appointment);
        //}

        //public ActionResult DeleteAppointment(int? id)
        //{
        //    if (id == null)
        //        return HttpNotFound();
        //    var appointment = DbContext.Appointements.FirstOrDefault(m => m.Id == id);
        //    if (appointment == null)
        //        return HttpNotFound();
        //    DbContext.Appointements.Remove(appointment);
        //    DbContext.SaveChanges();

        //    return RedirectToAction("ActiveAppointment");
        //}

        //public ActionResult PatientsList()
        //{
        //    var result = DbContext.Patients.ToList();
        //    return View(result);
        //}

        //public ActionResult DeletePatient(int? id)
        //{
        //    if (id == null)
        //        return HttpNotFound();
        //    var patient = DbContext.Patients.FirstOrDefault(m => m.PatientId == id);
        //    if (patient == null)
        //        return HttpNotFound();
        //    DbContext.Patients.Remove(patient);
        //    DbContext.SaveChanges();

        //    return RedirectToAction("PatientsList");
        //}

    }
}
