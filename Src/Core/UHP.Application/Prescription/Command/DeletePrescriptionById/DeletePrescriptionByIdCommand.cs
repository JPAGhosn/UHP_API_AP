using MediatR;

namespace UHP.Application.Prescription.Command.DeletePrescriptionById
{
    public class DeletePrescriptionByIdCommand : IRequest
    {
        public long Id { get; set; }
    }
}