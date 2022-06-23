using System;
using System.Collections.Generic;

#nullable disable

namespace Hospital_management.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int DoctorId { get; set; }
        public int? DoctorAge { get; set; }
        public string DoctorPosition { get; set; }
        public string DoctorExperience { get; set; }
        public int? DoctorAppointmentCount { get; set; }
        public int? DoctorUserId { get; set; }

        public virtual User DoctorUser { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
