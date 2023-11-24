using UHP.Domain.Models.Drugs;

#nullable disable

namespace UHP.Domain.Models.Public
{
    public partial class PrescriptionByDrugProduct
    {
        public long Id { get; set; }
        public long PrescriptionId { get; set; }
        public long DrugProductId { get; set; }

        public virtual DrugProduct DrugProduct { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
