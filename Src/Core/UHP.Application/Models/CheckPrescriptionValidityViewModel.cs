using System.Collections.Generic;

namespace UHP.Application.Models
{
    public class CheckPrescriptionValidityViewModel
    {
        public bool Validity { get; set; }
        public List<string> Description { get; set; } = new List<string>();
    }
}