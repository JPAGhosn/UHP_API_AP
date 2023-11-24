using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UHP.Application.Exceptions;
using UHP.Application.Models;
using UHP.Application.User.Authentication.Login;
using UHP.Persistence;

namespace UHP.Application.User.Authentication.SignUp
{
    public class SignUpUserRequestHandler : IRequestHandler<SignUpUserRequestModel, TokenViewModel>
    {
        private readonly UhpContext _uhpContext;
        private readonly IMediator _mediator;

        public SignUpUserRequestHandler(IMediator mediator, UhpContext uhpContext)
        {
            _mediator = mediator;
            _uhpContext = uhpContext;
        }

        public async Task<TokenViewModel> Handle(SignUpUserRequestModel request, CancellationToken cancellationToken)
        {
            var userEmail =
                await _uhpContext.Users.SingleOrDefaultAsync(user =>
                    user.Email.ToLower().Trim() == request.Email.ToLower().Trim(), cancellationToken);

            if (userEmail != null)
            {
                throw new DuplicatedEmailException(request.Email.ToLower().Trim());
            }

            var user = new Domain.Models.Users.User()
            {
                Email = request.Email,
                Firstname = request.FirstName,
                Gender = request.Gender,
                Lastname = request.LastName,
                Password = request.Password,
                Title = request.Title,
                RoleId = request.RoleId
            };

            await _uhpContext.Users.AddAsync(user, cancellationToken);
            await _uhpContext.SaveChangesAsync(cancellationToken);
            return await _mediator.Send(new LoginUserRequestModel()
            {
                Email = request.Email,
                Password = request.Password
            }, cancellationToken);
        }
    }
}