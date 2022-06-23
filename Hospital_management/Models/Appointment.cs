using System;
using System.Collections.Generic;

#nullable disable

namespace Hospital_management.Models
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime? AppointmentDateTime { get; set; }
        public int? AppointmentUserId { get; set; }
        public int? AppointmentDoctorId { get; set; }
        public int? AppointmentState { get; set; }

        public virtual Doctor AppointmentDoctor { get; set; }
        public virtual User AppointmentUser { get; set; }
    }
}
