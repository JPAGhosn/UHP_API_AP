using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UHP.Application.Models;
using UHP.Persistence;

namespace UHP.Application.DrugByDrugInteraction.Queries.CheckPrescriptionValidity
{
    public class
        CheckPrescriptionValidityQueryHandler : IRequestHandler<CheckPrescriptionValidityQuery,
            CheckPrescriptionValidityViewModel>
    {
        private readonly UhpContext _uhpContext;

        public CheckPrescriptionValidityQueryHandler(UhpContext uhpContext)
        {
            _uhpContext = uhpContext;
        }

        public async Task<CheckPrescriptionValidityViewModel> Handle(CheckPrescriptionValidityQuery request,
            CancellationToken cancellationToken)
        {
            var drugByDrugInteractions = _uhpContext.DrugByDrugInteractions.Where(product =>
                request.DrugId.Contains(product.FirstDrugId)
                && request.DrugId.Contains(product.SecondDrugId)).ToList();
            
            return new CheckPrescriptionValidityViewModel
            {
                Description = drugByDrugInteractions.Select(interaction => interaction.Description).ToList(),
                Validity = !drugByDrugInteractions.Any()
            };
        }
    }
}