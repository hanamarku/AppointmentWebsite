using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Online_Appointment.Models;
using Online_Appointment.Models.Requests;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Online_Appointment.Areas.Admin.Controllers
{
    public class User_DoctorController : Controller
    {

        private ApplicationDbContext context = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public User_DoctorController()
        {

        }
        public User_DoctorController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        // GET: Admin/User_Doctor_
        // public ActionResult Index()
        // {
        //     return View();
        // }

        [HttpGet]
        public ActionResult Add_Doctor()
        {
            ViewBag.departments = context.Departments.Select(a => a.DepId).ToList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add_Doctor([Bind(Exclude = "UserPhoto")] DoctorRegisterRequest model)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                //try catch veprimet me skedare / db
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase poImgFile = Request.Files["UserPhoto"];

                    using (var binary = new BinaryReader(poImgFile.InputStream))
                    {
                        imageData = binary.ReadBytes(poImgFile.ContentLength);
                    }
                }

                var user = new ApplicationUser
                {
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    UserName = model.UserName,
                    Email = model.Email,
                    Birthday = model.Birthday,
                    CreatedOn = System.DateTime.Now,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {


                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    await this.UserManager.AddToRoleAsync(user.Id, "Doctor");

                    //string filename = Path.GetFileName(doctor.File.FileName);
                    //string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                    //string path = Path.Combine(Server.MapPath("~/Images/Dep_Images/"), _filename);
                    //Doctor doctor = new Doctor()
                    //{

                    Doctor doctor = new Doctor()
                    {
                        Id = user.Id,
                        Specialization = model.Specialization,
                        StartTime = model.StartTime,
                        EndTime = model.EndTime,
                        Status = model.Status,
                        DepId = model.Departament,
                        UserPhoto = imageData

                    };
                    //string filename = Path.GetFileName(doctor.File.FileName);
                    //string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                    //string path = Path.Combine(Server.MapPath("~/Images/Doctor_Images/"), _filename);
                    //doctor.ImageURL = "~/Images/Doctor_Images/" + _filename;
                    context.Doctors.Add(doctor);
                    context.SaveChanges();

                    //if (context.SaveChanges() > 0)
                    //{
                    //    doctor.File.SaveAs(path);
                    //    TempData["AlertMessage"] = "Doctor added successfully !";
                    //}
                    return RedirectToAction("Add_Doctor", "User_Doctor");
                }

            }
            ViewBag.departments = context.Departments.Select(a => a.DepId).ToList();
            return View(model);
        }



        // GET: Admin/User_Doctor_/Details/5
        public ActionResult DoctorsList()
        {
            var roleId = context.Roles.Where(m => m.Name == "Doctor").Select(m => m.Id).SingleOrDefault();

            ViewBag.doctor = context.Doctors.ToList();
            return View();
        }

        // GET: Admin/User_Doctor_/Create
        // public ActionResult Create()
        // {
        //     return View();
        // }

        // POST: Admin/User_Doctor_/Create
        // [HttpPost]
        // public ActionResult Create(FormCollection collection)
        // {
        //     try
        //     {
        //         // TODO: Add insert logic here
        //
        //         return RedirectToAction("Index");
        //     }
        //     catch
        //     {
        //         return View();
        //     }
        // }


        public FileContentResult UserPhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();

                if (userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");

                }
                // to get the user details to load user Image
                var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userImage = bdUsers.Doctors.Where(x => x.Id == userId).FirstOrDefault();

                return new FileContentResult(userImage.UserPhoto, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");

            }
        }


        // GET: Admin/User_Doctor_/Edit/5
        // public ActionResult Edit(int id)
        // {
        //     return View();
        // }
        //
        // // POST: Admin/User_Doctor_/Edit/5
        // [HttpPost]
        // public ActionResult Edit(int id, FormCollection collection)
        // {
        //     try
        //     {
        //         // TODO: Add update logic here
        //
        //         return RedirectToAction("Index");
        //     }
        //     catch
        //     {
        //         return View();
        //     }
        // }

        // GET: Admin/User_Doctor_/Delete/5
        // public ActionResult Delete(int id)
        // {
        //     return View();
        // }

        // POST: Admin/User_Doctor_/Delete/5
        // [HttpPost]
        // public ActionResult Delete(int id, FormCollection collection)
        // {
        //     try
        //     {
        //         // TODO: Add delete logic here
        //
        //         return RedirectToAction("Index");
        //     }
        //     catch
        //     {
        //         return View();
        //     }
        // }
    }
}
