using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UHP.Application.Exceptions;
using UHP.Application.Models;
using UHP.Application.Prescription.Queries.GetPrescriptionById;
using UHP.Domain.Models.Public;
using UHP.Persistence;

namespace UHP.Application.Prescription.Command.UpdatePrescription
{
    public class UpdatePrescriptionCommandHandler : IRequestHandler<UpdatePrescriptionCommand, PrescriptionViewModel>
    {
        private readonly UhpContext _uhpContext;
        private readonly IMediator _mediator;


        public UpdatePrescriptionCommandHandler(UhpContext uhpContext, IMediator mediator)
        {
            _uhpContext = uhpContext;
            _mediator = mediator;
        }

        public async Task<PrescriptionViewModel> Handle(UpdatePrescriptionCommand request,
            CancellationToken cancellationToken)
        {
            var prescription =
                await _uhpContext.Prescriptions.Include(prescription1 => prescription1.PrescriptionByDrugProducts)
                    .SingleOrDefaultAsync(prescription1 => prescription1.Id == request.Id,
                        cancellationToken);
            
            if (prescription == null)
            {
                throw new NotFoundException("Prescription", request.Id);
            }
            
            if (prescription.RedeemedAt.HasValue)
            {
                throw new LockedEntityException("Prescription", request.Id.ToString(), "already Redeemed");
            }


            prescription.PrescriptionByDrugProducts = request.DrugProductsId.Select(l => new PrescriptionByDrugProduct()
            {
                DrugProductId = l
            }).ToList();

            _uhpContext.Update(prescription);
            await _uhpContext.SaveChangesAsync(cancellationToken);

            return await _mediator.Send(new GetPrescriptionByIdQuery
            {
                Id = prescription.Id
            }, cancellationToken);
        }
    }
}