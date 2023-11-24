using System.Collections.Generic;
using UHP.Domain.Models.Public;

#nullable disable

namespace UHP.Domain.Models.Drugs
{
    public partial class DrugProduct
    {
        public DrugProduct()
        {
            PrescriptionByDrugProducts = new HashSet<PrescriptionByDrugProduct>();
        }

        public long Id { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public string Unit { get; set; }
        public string Currency { get; set; }
        public long DrugId { get; set; }

        public virtual Drug Drug { get; set; }
        public virtual ICollection<PrescriptionByDrugProduct> PrescriptionByDrugProducts { get; set; }
    }
}
