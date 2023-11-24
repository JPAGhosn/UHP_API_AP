using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UHP.Application.Configurations.Settings;
using UHP.Application.Exceptions;
using UHP.Application.Models;
using UHP.Persistence;

namespace UHP.Application.User.Authentication.Login
{
    public class LoginUserRequestHandler : IRequestHandler<LoginUserRequestModel, TokenViewModel>
    {
        private readonly UhpContext _uhpContext;
        private readonly AppSettings _appSettings;

        public LoginUserRequestHandler(UhpContext uhpContext, AppSettings appSettings)
        {
            _uhpContext = uhpContext;
            _appSettings = appSettings;
        }

        public async Task<TokenViewModel> Handle(LoginUserRequestModel request, CancellationToken cancellationToken)
        {
            var user = await _uhpContext.Users.Include(role => role.Role)
                .SingleOrDefaultAsync(
                    x => x.Email.ToLower().Trim() == request.Email.ToLower().Trim() && x.Password == request.Password,
                    cancellationToken);
            if (user == null)
            {
                throw new UnauthorizedException("Invalid Credentials");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("Firstname", user.Firstname),
                    new Claim("Lastname", user.Lastname),
                    new Claim("Title", user.Title),
                    new Claim("Gender", user.Gender),
                    new Claim("Roles", user.Role.Name),
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Audience = _appSettings.ValidAudience,
                Issuer = _appSettings.ValidIssuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenViewModel()
            {
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}