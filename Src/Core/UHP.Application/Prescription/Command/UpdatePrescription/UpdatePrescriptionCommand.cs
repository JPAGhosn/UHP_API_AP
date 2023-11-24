using System.Collections.Generic;
using MediatR;
using UHP.Application.Models;

namespace UHP.Application.Prescription.Command.UpdatePrescription
{
    public class UpdatePrescriptionCommand : IRequest<PrescriptionViewModel>
    {
        public long Id { get; set; }
        public List<long> DrugProductsId { get; set; } = new();
        
    }
}