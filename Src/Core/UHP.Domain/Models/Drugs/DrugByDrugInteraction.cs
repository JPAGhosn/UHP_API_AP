#nullable disable

namespace UHP.Domain.Models.Drugs
{
    public partial class DrugByDrugInteraction
    {
        public long Id { get; set; }
        public long FirstDrugId { get; set; }
        public long SecondDrugId { get; set; }
        public string Description { get; set; }

        public virtual Drug FirstDrug { get; set; }
        public virtual Drug SecondDrug { get; set; }
    }
}
