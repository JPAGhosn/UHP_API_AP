using System.Collections.Generic;

namespace UHP.Application.Models
{
    public class PrescriptionListViewModel
    {
        public List<PrescriptionViewModel> RedeemedPrescriptionViewModels { get; set; } =
            new();

        public List<PrescriptionViewModel> UnredeemedPrescriptionViewModels { get; set; } =
            new();
    }
}