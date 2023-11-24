using MediatR;
using UHP.Application.Models;

namespace UHP.Application.User.Authentication.SignUp
{
    public class SignUpUserRequestModel : IRequest<TokenViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public long RoleId { get; set; }
    }
}