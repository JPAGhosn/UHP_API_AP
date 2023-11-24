using System.Collections.Generic;

namespace UHP.Application.Models
{
    public class DrugViewModel
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CasNumber { get; set; }
        public string Unii { get; set; }
        public double? AverageMass { get; set; }
        public double? MonoisotopicMass { get; set; }
        public string State { get; set; }
        public string Toxicity { get; set; }
        public string DrugBankId { get; set; }

        public List<DrugProductViewModel> DrugProductViewModels { get; set; }
    }
}