using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UHP.Application.Models;
using UHP.Persistence;

namespace UHP.Application.DrugProducts.Queries.GetDrugProductById
{
    public class GetDrugProductByIdQueryHandler : IRequestHandler<GetDrugProductByIdQuery, DrugProductViewModel>
    {
        private readonly UhpContext _uhpContext;

        public GetDrugProductByIdQueryHandler(UhpContext uhpContext)
        {
            _uhpContext = uhpContext;
        }

        public async Task<DrugProductViewModel> Handle(GetDrugProductByIdQuery request,
            CancellationToken cancellationToken)
        {
            var drugProducts =
                await _uhpContext.DrugProducts.Include(product => product.Drug).SingleOrDefaultAsync(
                    product => product.Id == request.Id,
                    cancellationToken);

            return new DrugProductViewModel
            {
                Id = drugProducts.Id,
                Cost = drugProducts.Cost,
                Currency = drugProducts.Currency,
                Description = drugProducts.Description,
                Unit = drugProducts.Unit,
                DrugId = drugProducts.DrugId,
                DrugName = drugProducts.Drug.Name
            };
        }
    }
}