namespace UHP.Application.Models
{
    public class DrugProductViewModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public string Unit { get; set; }
        public string Currency { get; set; }

        public long DrugId { get; set; }

        public string DrugName { get; set; }
        
    }
}