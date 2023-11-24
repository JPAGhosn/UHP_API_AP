using MediatR;
using UHP.Application.Models;

namespace UHP.Application.Prescription.Queries.GetPrescriptionById
{
    public class GetPrescriptionByIdQuery : IRequest<PrescriptionViewModel>
    {
        public long Id { get; set; }
    }
}