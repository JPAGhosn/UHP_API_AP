using MediatR;
using UHP.Application.Models;

namespace UHP.Application.User.Authentication.Login
{
    public class LoginUserRequestModel : IRequest<TokenViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}