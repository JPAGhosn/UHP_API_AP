using System;
using System.Collections.Generic;

#nullable disable

namespace UHP.Domain.Models.Drugs
{
    public partial class Drug
    {
        public Drug()
        {
            DrugByDrugInteractionFirstDrugs = new HashSet<DrugByDrugInteraction>();
            DrugByDrugInteractionSecondDrugs = new HashSet<DrugByDrugInteraction>();
            DrugProducts = new HashSet<DrugProduct>();
        }

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
        public DateTime? CreatedAt { get; set; }
        public string DrugBankId { get; set; }

        public virtual ICollection<DrugByDrugInteraction> DrugByDrugInteractionFirstDrugs { get; set; }
        public virtual ICollection<DrugByDrugInteraction> DrugByDrugInteractionSecondDrugs { get; set; }
        public virtual ICollection<DrugProduct> DrugProducts { get; set; }
    }
}
