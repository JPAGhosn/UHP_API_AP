using MediatR;
using UHP.Application.Models;

namespace UHP.Application.Drug.Queries.GetDrugById
{
    public class GetDrugByIdQuery : IRequest<DrugViewModel>
    {
        public long Id { get; set; }
    }
}