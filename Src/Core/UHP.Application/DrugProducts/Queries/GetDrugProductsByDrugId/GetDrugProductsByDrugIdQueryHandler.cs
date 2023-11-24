using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UHP.Application.Models;
using UHP.Persistence;

namespace UHP.Application.DrugProducts.Queries.GetDrugProductsByDrugId
{
    public class
        GetDrugProductsByDrugIdQueryHandler : IRequestHandler<GetDrugProductsByDrugIdQuery, List<IdValueViewModel>>
    {
        private readonly UhpContext _uhpContext;

        public GetDrugProductsByDrugIdQueryHandler(UhpContext uhpContext)
        {
            _uhpContext = uhpContext;
        }

        public async Task<List<IdValueViewModel>> Handle(GetDrugProductsByDrugIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _uhpContext.DrugProducts.Where(product => product.DrugId == request.DrugId).Select(
                product => new IdValueViewModel()
                {
                    Id = product.Id,
                    Value = product.Description
                }).ToListAsync(cancellationToken);
        }
    }
}