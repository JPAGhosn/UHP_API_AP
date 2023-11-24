using System.Collections.Generic;
using MediatR;
using UHP.Application.Models;

namespace UHP.Application.Prescription.Queries.GetPrescriptionByDoctorId
{
    public class GetPrescriptionByDoctorIdQuery : IRequest<PrescriptionListViewModel>
    {
        public long DoctorId { get; set; }
    }
}