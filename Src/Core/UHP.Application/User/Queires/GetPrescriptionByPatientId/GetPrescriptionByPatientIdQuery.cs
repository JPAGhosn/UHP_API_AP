using System.Collections.Generic;
using MediatR;
using UHP.Application.Models;

namespace UHP.Application.User.Queires.GetPrescriptionByPatientId
{
    public class GetPrescriptionByPatientIdQuery : IRequest<PrescriptionListViewModel>
    {
        public long PatientId { get; set; }
    }
}