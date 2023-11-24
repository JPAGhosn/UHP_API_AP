using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UHP.Application.Models;
using UHP.Persistence;

namespace UHP.Application.Drug.Queries.GetDrugsSearch
{
    public class GetDrugsSearchQueryHandler : IRequestHandler<GetDrugsSearchQuery, List<IdValueViewModel>>
    {
        private readonly UhpContext _uhpContext;

        public GetDrugsSearchQueryHandler(UhpContext uhpContext)
        {
            _uhpContext = uhpContext;
        }

        public async Task<List<IdValueViewModel>> Handle(GetDrugsSearchQuery request,
            CancellationToken cancellationToken)
        {
            var value = $"%{request.Value}%";

            var drugs = _uhpContext.Drugs.Include(drug => drug.DrugProducts)
                .Where(drug => EF.Functions.ILike(drug.Name, value) && drug.DrugProducts.Count != 0).Take(50);
            return await drugs.Select(drug => new IdValueViewModel
            {
                Id = drug.Id,
                Value = drug.Name
            }).ToListAsync(cancellationToken);
        }
    }
}