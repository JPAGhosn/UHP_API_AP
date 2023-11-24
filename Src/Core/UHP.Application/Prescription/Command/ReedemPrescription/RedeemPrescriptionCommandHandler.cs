using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UHP.Application.Exceptions;
using UHP.Application.Models;
using UHP.Application.Prescription.Queries.GetPrescriptionById;
using UHP.Persistence;

namespace UHP.Application.Prescription.Command.ReedemPrescription
{
    public class RedeemPrescriptionCommandHandler : IRequestHandler<RedeemPrescriptionCommand,PrescriptionViewModel>
    {
        private readonly UhpContext _uhpContext;
        private readonly IMediator _mediator;

        public RedeemPrescriptionCommandHandler(UhpContext uhpContext, IMediator mediator)
        {
            _uhpContext = uhpContext;
            _mediator = mediator;
        }

        public async Task<PrescriptionViewModel> Handle(RedeemPrescriptionCommand request, CancellationToken cancellationToken)
        {
            
            var prescription =
                await _uhpContext.Prescriptions.SingleOrDefaultAsync(prescription1 => prescription1.Id == request.Id,
                    cancellationToken);
            
            if (prescription == null)
            {
                throw new NotFoundException("Prescription", request.Id);
            }
            
            prescription.RedeemedAt = DateTime.Now;
            
            _uhpContext.Update(prescription);
            await _uhpContext.SaveChangesAsync(cancellationToken);
            
            return await _mediator.Send(new GetPrescriptionByIdQuery
            {
                Id = prescription.Id
            }, cancellationToken);
        }
    }
}