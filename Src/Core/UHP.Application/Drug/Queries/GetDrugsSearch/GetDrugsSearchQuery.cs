using System.Collections.Generic;
using MediatR;
using UHP.Application.Models;

namespace UHP.Application.Drug.Queries.GetDrugsSearch
{
    public class GetDrugsSearchQuery : IRequest<List<IdValueViewModel>>
    {
        public string Value { get; set; } = "";
    }
}