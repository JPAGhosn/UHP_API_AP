using System;
using System.Collections.Generic;
using UHP.Domain.Models.Users;

#nullable disable

namespace UHP.Domain.Models.Public
{
    public partial class Prescription
    {
        public Prescription()
        {
            PrescriptionByDrugProducts = new HashSet<PrescriptionByDrugProduct>();
        }

        public long Id { get; set; }
        public long DoctorId { get; set; }
        public long PatientId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? RedeemedAt { get; set; }
        public string QrCodePath { get; set; }

        public virtual User Doctor { get; set; }
        public virtual User Patient { get; set; }
        public virtual ICollection<PrescriptionByDrugProduct> PrescriptionByDrugProducts { get; set; }
    }
}
