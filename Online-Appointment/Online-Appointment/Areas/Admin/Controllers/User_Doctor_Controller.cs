using Microsoft.AspNet.Identity.Owin;
using Online_Appointment.Models;
using Online_Appointment.Models.Requests;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Online_Appointment.Areas.Admin.Controllers
{
    public class User_Doctor_Controller : Controller
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
        public User_Doctor_Controller()
        {

        }
        public User_Doctor_Controller(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        // GET: Admin/User_Doctor_
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add_Doctor()
        {
            ViewBag.departments = context.Departments.Select(a => a.Name).ToList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add_Doctor(DoctorRegisterRequest model)
        {
            if (ModelState.IsValid)
            {

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

                    return RedirectToAction("Login", "Account");
                }
            }
            return View();
        }

        // GET: Admin/User_Doctor_/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/User_Doctor_/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/User_Doctor_/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/User_Doctor_/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/User_Doctor_/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/User_Doctor_/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/User_Doctor_/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
