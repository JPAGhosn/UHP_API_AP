using System;
using System.Collections.Generic;

namespace UHP.Application.Models
{
    public class PrescriptionViewModel
    {
        public long Id { get; set; }
        public string DoctorFullName { get; set; }
        public string PatientFullName { get; set; }
        public long PatientId { get; set; }
        public string QrCode { get; set; }

        public DateTime? RedeemAt { get; set; }
        
        public DateTime TimeStamp { get; set; }
        public List<DrugProductViewModel> DrugProductViewModels { get; set; } = new List<DrugProductViewModel>();
    }
}