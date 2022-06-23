using Hospital_management.CustomClasses;
using Hospital_management.CustomModels;
using Hospital_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hospital_management.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly Hospital_DbContext _db;

        public AdminController(Hospital_DbContext _db)
        {
            this._db = _db;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Index(string result)
        {
            if (result != null)
                ViewBag.Result = result;

            var doctors = _db.Doctors
                .Include(x => x.DoctorUser)
                .OrderBy(x => x.DoctorUser.UsersActualName)
                .Select(x => new CustomDoctor
                {
                    Id = x.DoctorId,
                    Name = x.DoctorUser.UsersActualName,
                    Surname = x.DoctorUser.UsersSurname,
                    Age = x.DoctorAge,
                    Position = x.DoctorPosition,
                    Experience = x.DoctorExperience,
                    AppointmentCount = x.DoctorAppointmentCount
                })
                .ToList();

            return View(doctors);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddDoctor()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddDoctor(CustomDoctor customDoctor)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (_db.Users.Any(x => x.UsersName.ToLower() == customDoctor.UserName.ToLower()))
            {
                ModelState.AddModelError("UserName", "Daxil etdiyiniz istifadəçi adı artıq mövcuddur");
                return View();
            }

            User user = new()
            {
                UsersActualName = customDoctor.Name,
                UsersSurname = customDoctor.Surname,
                UsersName = customDoctor.UserName,
                UsersPassword = Helper.CreateMD5(customDoctor.Password),
                UsersStatusId = 3
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            Doctor doctor = new()
            {
                DoctorAge = customDoctor.Age,
                DoctorPosition = customDoctor.Position,
                DoctorExperience = customDoctor.Experience,
                DoctorUserId = user.UsersId
            };

            _db.Doctors.Add(doctor);
            _db.SaveChanges();

            return RedirectToAction("Index", "Admin", new { result = "AddDoctorSuccess" });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditDoctor(int id)
        {
            var doctor = _db.Doctors
                .Include(x => x.DoctorUser)
                .Select(x => new CustomDoctor
                {
                    Id = x.DoctorId,
                    Name = x.DoctorUser.UsersActualName,
                    Surname = x.DoctorUser.UsersSurname,
                    Age = x.DoctorAge,
                    Position = x.DoctorPosition,
                    Experience = x.DoctorExperience
                })
                .SingleOrDefault(x => x.Id == id);

            if (doctor == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            return View(doctor);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditDoctor(int id, CustomDoctor customDoctor)
        {
            var oldDoctor = _db.Doctors.SingleOrDefault(x => x.DoctorId == id);

            if (oldDoctor == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            var user = _db.Users.SingleOrDefault(x => x.UsersId == oldDoctor.DoctorUserId);

            user.UsersActualName = customDoctor.Name;
            user.UsersSurname = customDoctor.Surname;

            oldDoctor.DoctorAge = customDoctor.Age;
            oldDoctor.DoctorPosition = customDoctor.Position;
            oldDoctor.DoctorExperience = customDoctor.Experience;
            _db.SaveChanges();

            return RedirectToAction("Index", "Admin", new { result = "EditDoctorSuccess" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteDoctor(int id)
        {
            var doctor = _db.Doctors.SingleOrDefault(x => x.DoctorId == id);

            if (doctor == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            _db.Appointments.RemoveRange(_db.Appointments.Where(x => x.AppointmentDoctorId == id));
            _db.Users.Remove(_db.Users.SingleOrDefault(x => x.UsersId == doctor.DoctorUserId));
            _db.SaveChanges();

            return RedirectToAction("Index", "Admin", new { result = "DeleteDoctorSuccess" });
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddUser(CustomUser customUser)
        {
            if (!ModelState.IsValid || User.Identity.IsAuthenticated)
            {
                return View("~/Views/User/Registration.cshtml");
            }

            if (_db.Users.Any(x => x.UsersName.ToLower() == customUser.UserName.ToLower()))
            {
                ModelState.AddModelError("UserName", "Daxil etdiyiniz istifadəçi adı artıq mövcuddur");
                return View("~/Views/User/Registration.cshtml");
            }

            User user = new()
            {
                UsersActualName = customUser.Name,
                UsersSurname = customUser.Surname,
                UsersPhone = customUser.Phone,
                UsersName = customUser.UserName,
                UsersPassword = Helper.CreateMD5(customUser.Password),
                UsersStatusId = 4
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            return RedirectToAction("Index", "User", new { result = "RegistrationSuccess" });
        }

    }
}
