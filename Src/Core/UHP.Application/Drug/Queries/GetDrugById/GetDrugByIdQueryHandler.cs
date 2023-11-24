using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UHP.Application.Exceptions;
using UHP.Application.Models;
using UHP.Persistence;

namespace UHP.Application.Drug.Queries.GetDrugById
{
    public class GetDrugByIdQueryHandler : IRequestHandler<GetDrugByIdQuery, DrugViewModel>
    {
        private readonly UhpContext _uhpContext;

        public GetDrugByIdQueryHandler(UhpContext uhpContext)
        {
            _uhpContext = uhpContext;
        }

        public async Task<DrugViewModel> Handle(GetDrugByIdQuery request, CancellationToken cancellationToken)
        {
            var drug = await _uhpContext.Drugs.Include(drug1 => drug1.DrugProducts)
                .SingleOrDefaultAsync(drug => drug.Id == request.Id, cancellationToken);

            if (drug == null)
            {
                throw new NotFoundException("Drug", request.Id);
            }

            return new DrugViewModel()
            {
                Description = drug.Description,
                Id = drug.Id,
                Name = drug.Name,
                State = drug.State,
                Toxicity = drug.Toxicity,
                Type = drug.Type,
                Unii = drug.Unii,
                AverageMass = drug.AverageMass,
                CasNumber = drug.CasNumber,
                MonoisotopicMass = drug.MonoisotopicMass,
                DrugBankId = drug.DrugBankId,
                DrugProductViewModels = drug.DrugProducts.Select(product => new DrugProductViewModel()
                {
                    Cost = product.Cost,
                    Currency = product.Currency,
                    Description = product.Description,
                    Id = product.Id,
                    Unit = product.Unit
                }).ToList()
            };
        }
    }
}