using Hospital_management.CustomClasses;
using Hospital_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital_management.Controllers
{
    [Authorize(Roles = "Həkim")]
    public class DoctorController : Controller
    {
        private readonly Hospital_DbContext _db;

        public DoctorController(Hospital_DbContext _db)
        {
            this._db = _db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = Convert.ToInt32(User.Claims.SingleOrDefault(x => x.Type == "Id").Value);
            var doctor = _db.Doctors.SingleOrDefault(x => x.DoctorUserId == userId);

            List<AppointmentInfo> appointments = _db.Appointments
                .Where(x => x.AppointmentDoctorId == doctor.DoctorId)
                .Include(x => x.AppointmentUser)
                .OrderBy(x => x.AppointmentDateTime)
                .Select(x => new AppointmentInfo
                {
                    PatientName = x.AppointmentUser.UsersActualName,
                    PatientSurname = x.AppointmentUser.UsersSurname,
                    PatientPhone = x.AppointmentUser.UsersPhone,
                    Date = x.AppointmentDateTime.Value.ToString("dd/MM/yyyy"),
                    Time = x.AppointmentDateTime.Value.ToString("HH:mm")
                })
                .ToList();

            return View(appointments);
        }
    }
}
