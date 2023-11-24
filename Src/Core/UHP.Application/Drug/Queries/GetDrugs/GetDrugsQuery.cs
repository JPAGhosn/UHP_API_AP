using System.Collections.Generic;
using MediatR;
using UHP.Application.Models;

namespace UHP.Application.Drug.Queries.GetDrugs
{
    public class GetDrugsQuery : IRequest<DrugsListViewModel>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public Dictionary<string, string> Sort { get; set; }
        public Dictionary<string, string> Filters { get; set; }
    }
}