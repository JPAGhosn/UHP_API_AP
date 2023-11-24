using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UHP.Persistence;

namespace UHP.Application.Prescription.Command.DeletePrescriptionById
{
    public class DeletePrescriptionByIdCommandHandler : IRequestHandler<DeletePrescriptionByIdCommand>
    {
        private readonly UhpContext _uhpContext;

        public DeletePrescriptionByIdCommandHandler(UhpContext uhpContext)
        {
            _uhpContext = uhpContext;
        }

        public async Task<Unit> Handle(DeletePrescriptionByIdCommand request, CancellationToken cancellationToken)
        {
            var prescription =
                await _uhpContext.Prescriptions.SingleOrDefaultAsync(prescription1 => prescription1.Id == request.Id,
                    cancellationToken);

            _uhpContext.Prescriptions.Remove(prescription);
            await _uhpContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}