using MediatR;
using UHP.Application.Models;

namespace UHP.Application.DrugProducts.Queries.GetDrugProductById
{
    public class GetDrugProductByIdQuery : IRequest<DrugProductViewModel>
    {
        public long Id { get; set; }
    }
}