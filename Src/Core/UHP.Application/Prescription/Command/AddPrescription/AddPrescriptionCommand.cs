using System.Collections.Generic;
using MediatR;
using UHP.Application.Models;

namespace UHP.Application.Prescription.Command.AddPrescription
{
    public class AddPrescriptionCommand : IRequest<PrescriptionViewModel>
    {
        public long DoctorId { get; set; }
        public long PatientId { get; set; }
        public List<long> DrugProductsId { get; set; } = new();
    }

    public class AddPrescriptionCommandModel
    {
        public long PatientId { get; set; }
        public List<long> DrugProductsId { get; set; } = new();
    }
}