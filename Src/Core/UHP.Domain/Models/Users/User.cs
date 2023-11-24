using System.Collections.Generic;
using UHP.Domain.Models.Public;

#nullable disable

namespace UHP.Domain.Models.Users
{
    public partial class User
    {
        public User()
        {
            PrescriptionDoctors = new HashSet<Prescription>();
            PrescriptionPatients = new HashSet<Prescription>();
        }

        public long Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Prescription> PrescriptionDoctors { get; set; }
        public virtual ICollection<Prescription> PrescriptionPatients { get; set; }
    }
}
