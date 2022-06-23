using System;
using System.Collections.Generic;

#nullable disable

namespace Hospital_management.Models
{
    public partial class User
    {
        public User()
        {
            Appointments = new HashSet<Appointment>();
            Doctors = new HashSet<Doctor>();
        }

        public int UsersId { get; set; }
        public string UsersActualName { get; set; }
        public string UsersSurname { get; set; }
        public string UsersPhone { get; set; }
        public string UsersName { get; set; }
        public int? UsersStatusId { get; set; }
        public string UsersPassword { get; set; }

        public virtual Status UsersStatus { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
