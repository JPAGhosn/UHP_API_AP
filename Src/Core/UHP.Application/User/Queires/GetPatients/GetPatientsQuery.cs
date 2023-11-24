using System.Collections.Generic;
using MediatR;
using UHP.Application.Models;

namespace UHP.Application.User.Queires.GetPatients
{
    public class GetPatientsQuery : IRequest<List<IdValueViewModel>>
    {
        public string Patient { get; set; }
    }
}