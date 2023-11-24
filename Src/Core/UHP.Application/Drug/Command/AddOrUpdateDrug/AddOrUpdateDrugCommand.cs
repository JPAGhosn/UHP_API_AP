using System.Collections.Generic;
using MediatR;
using UHP.Application.Models;

namespace UHP.Application.Drug.Command.AddOrUpdateDrug
{
    public class AddOrUpdateDrugCommand : IRequest<DrugViewModel>
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

        public List<AddDrugProductsCommand> AddDrugProductsCommands { get; set; } = new List<AddDrugProductsCommand>();
    }

    public class AddDrugProductsCommand
    {
        public long? Id { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public string Unit { get; set; }
        public string Currency { get; set; }
    }
}