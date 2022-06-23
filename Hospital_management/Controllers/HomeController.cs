using Hospital_management.CustomModels;
using Hospital_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hospital_management.Controllers
{
    [Authorize(Roles = "İstifadəçi")]
    public class HomeController : Controller
    {
        private readonly Hospital_DbContext _db;

        public HomeController(Hospital_DbContext _db)
        {
            this._db = _db;
        }

        [HttpGet]
        public IActionResult Index(string result)
        {
            if (result != null)
                ViewBag.Result = result;

            var userId = Convert.ToInt32(User.Claims.SingleOrDefault(x => x.Type == "Id").Value);

            var doctors = _db.Doctors
                .Include(x => x.Appointments)
                .Where(x => !x.Appointments.Any(x => x.AppointmentUserId == userId))
                .Include(x => x.DoctorUser)
                .OrderBy(x => x.DoctorUser.UsersActualName)
                .Select(x => new CustomDoctor
                {
                    Id = x.DoctorId,
                    Name = x.DoctorUser.UsersActualName,
                    Surname = x.DoctorUser.UsersSurname,
                    Position = x.DoctorPosition,
                    Experience = x.DoctorExperience
                })
                .ToList();

            return View(doctors);
        }

        public IActionResult MakeAppointment(int id, DateTime appointmentDateTime)
        {
            var userId = Convert.ToInt32(User.Claims.SingleOrDefault(x => x.Type == "Id").Value);

            if (appointmentDateTime < DateTime.Now)
            {
                return RedirectToAction("Index", new { result = "AppointmentDateFailed" });
            }

            Appointment appointment = new()
            {
                AppointmentDateTime = appointmentDateTime,
                AppointmentUserId = userId,
                AppointmentDoctorId = id
            };

            _db.Doctors.SingleOrDefault(x => x.DoctorId == id).DoctorAppointmentCount ++;
            _db.Appointments.Add(appointment);
            _db.SaveChanges();

            return RedirectToAction("Index", new { result = "AppointmentSuccess" });
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
