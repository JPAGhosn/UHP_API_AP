using MediatR;
using UHP.Application.Models;

namespace UHP.Application.Prescription.Command.ReedemPrescription
{
    public class RedeemPrescriptionCommand : IRequest<PrescriptionViewModel>
    {
        public long Id { get; set; }
    }
}