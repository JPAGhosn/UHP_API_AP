using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UHP.Application.Models;
using UHP.Persistence;

namespace UHP.Application.User.Queires.GetPatients
{
    public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, List<IdValueViewModel>>
    {
        private readonly UhpContext _uhpContext;

        public GetPatientsQueryHandler(UhpContext uhpContext)
        {
            _uhpContext = uhpContext;
        }

        public async Task<List<IdValueViewModel>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            var regex = "%" + request.Patient + "%";
            return await _uhpContext.Users
                .Include(user => user.Role).Where(m => m.RoleId == 1 &&
                                                       (EF.Functions.ILike(m.Firstname + m.Lastname, regex)
                                                        || EF.Functions.ILike(m.Lastname + m.Firstname, regex))).Select(
                    user =>
                        new IdValueViewModel
                        {
                            Id = user.Id,
                            Value = user.Firstname + ' ' + user.Lastname
                        }).ToListAsync(cancellationToken);
        }
    }
}