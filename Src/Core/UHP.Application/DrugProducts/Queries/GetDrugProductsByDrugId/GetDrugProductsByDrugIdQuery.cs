using System.Collections.Generic;
using MediatR;
using UHP.Application.Models;

namespace UHP.Application.DrugProducts.Queries.GetDrugProductsByDrugId
{
    public class GetDrugProductsByDrugIdQuery : IRequest<List<IdValueViewModel>>
    {
        public long DrugId { get; set; }
    }
}